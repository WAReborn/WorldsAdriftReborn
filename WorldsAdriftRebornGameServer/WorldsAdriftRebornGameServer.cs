using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Improbable.Worker;
using WorldsAdriftRebornGameServer.DLLCommunication;
using WorldsAdriftRebornGameServer.Game;
using WorldsAdriftRebornGameServer.Game.Components;
using WorldsAdriftRebornGameServer.Networking.Singleton;
using WorldsAdriftRebornGameServer.Networking.Wrapper;
using static WorldsAdriftRebornGameServer.DLLCommunication.EnetLayer;

namespace WorldsAdriftRebornGameServer
{
    internal class WorldsAdriftRebornGameServer
    {
        private static bool keepRunning = true;
        [PInvoke(typeof(EnetLayer.ENet_Poll_Callback))]
        private unsafe static void OnNewClientConnected(IntPtr peer )
        {
            ENetPeerHandle ePeer = new ENetPeerHandle(peer, new ENetHostHandle());
            if (!ePeer.IsInvalid)
            {
                Console.WriteLine("[info] got a connection.");
                PeerManager.Instance.playerState.Add(ePeer, new Dictionary<int, PlayerSyncStatus> { { 0, new PlayerSyncStatus() } });
            }
        }
        [PInvoke(typeof(EnetLayer.ENet_Poll_Callback))]
        private unsafe static void OnClientDisconnected(IntPtr peer )
        {
            ENetPeerHandle ePeer = new ENetPeerHandle(peer, new ENetHostHandle());
            if (!ePeer.IsInvalid)
            {
                Console.WriteLine("[info] a client disconnected.");
            }
        }

        private static readonly EnetLayer.ENet_Poll_Callback callbackC = new EnetLayer.ENet_Poll_Callback(OnNewClientConnected);
        private static readonly EnetLayer.ENet_Poll_Callback callbackD = new EnetLayer.ENet_Poll_Callback(OnClientDisconnected);
        private static readonly uint[] authoritativeComponents = { 8050, 8051, 6908, 1260, 1097, 1003, 1241};

        private static long nextEntityId = 0;
        public static long NextEntityId
        {
            get
            {
                return nextEntityId++;
            }
        }
        
        static unsafe void Main( string[] args )
        {
            Console.CancelKeyPress += delegate ( object? sender, ConsoleCancelEventArgs e )
            {
                keepRunning = false;
            };

            if (EnetLayer.ENet_Initialize() < 0)
            {
                Console.WriteLine("[error] failed to initialize ENet.");
                return;
            }

            Console.WriteLine("[info] successfully initialized ENet.");
            ENetHostHandle server = EnetLayer.ENet_Create_Host(7777, 1, 4, 0, 0);

            if (server.IsInvalid)
            {
                Console.WriteLine("[error] failed to create host and listen on network interface.");

                EnetLayer.ENet_Deinitialize(new IntPtr(0));
                return;
            }

            Console.WriteLine("[info] successfully initialized networking, now waiting for connections and data.");
            PeerManager.Instance.SetENetHostHandle(server);

            // define initial world state
            GameState.Instance.WorldState[0] = new List<SyncStep>()
            {
                new SyncStep(GameState.NextStateRequirement.ASSET_LOADED_RESPONSE, new Action<object>((object o) =>
                {
                    Console.WriteLine("[info] requesting the game to load the player asset...");

                    if (SendOPHelper.SendAssetLoadRequestOP((ENetPeerHandle)o, "notNeeded?", "Traveller", "Player"))
                    {
                        Console.WriteLine("[info] successfully serialized and queued AssetLoadRequestOp.");
                    }
                    else
                    {
                        Console.WriteLine("[info] failed to serialize and queue AssetLoadRequestOp.");
                    }
                })),
                new SyncStep(GameState.NextStateRequirement.ASSET_LOADED_RESPONSE, new Action<object>((object o) =>
                {
                    Console.WriteLine("[info] requesting the game to load the island from its asset bundles...");

                    if (SendOPHelper.SendAssetLoadRequestOP((ENetPeerHandle)o, "notNeeded?", "949069116@Island", "notNeeded?"))
                    {
                        Console.WriteLine("[info] successfully serialized and queued AssetLoadRequestOp.");
                    }
                    else
                    {
                        Console.WriteLine("[info] failed to serialize and queue AssetLoadRequestOp.");
                    }
                })),
                new SyncStep(GameState.NextStateRequirement.ADDED_ENTITY_RESPONSE, new Action<object>((object o) =>
                {
                    Console.WriteLine("[success] island asset loaded. requesting loading of island...");

                    if (SendOPHelper.SendAddEntityOP((ENetPeerHandle)o, NextEntityId, "949069116@Island", "notNeeded?"))
                    {
                        Console.WriteLine("[info] successfully serialized and queued AddEntityOp.");
                    }
                    else
                    {
                        Console.WriteLine("[info] failed to serialize and queue AddEntityOp.");
                    }
                })),
                new SyncStep(GameState.NextStateRequirement.ADDED_ENTITY_RESPONSE, new Action<object>((object o) =>
                {
                    Console.WriteLine("[info] client ack'ed island spawning instruction (info by sdk, does not mean it truly spawned). requesting to spawn player...");

                    if(SendOPHelper.SendAddEntityOP((ENetPeerHandle)o, NextEntityId, "Traveller", "Player"))
                    {
                        Console.WriteLine("[info] successfully serialized and queued AddEntityOp.");
                    }
                    else
                    {
                        Console.WriteLine("[info] failed to serialize and queue AddEntityOp.");
                    }
                }))
            };

            while (keepRunning)
            {
                EnetLayer.ENetPacket_Wrapper* packet = EnetLayer.ENet_Poll(server, 50, Marshal.GetFunctionPointerForDelegate(callbackC), Marshal.GetFunctionPointerForDelegate(callbackD));
                if(packet != null)
                {
                    // work on packets that are relevant to progress in sync state
                    foreach (KeyValuePair<ENetPeerHandle, Dictionary<int, PlayerSyncStatus>> keyValuePair in PeerManager.Instance.playerState)
                    {
                        int currentChunkIndex = 0;
                        int currentPlayerSyncIndex = PeerManager.Instance.playerState[keyValuePair.Key][currentChunkIndex].SyncStepPointer;

                        if (currentPlayerSyncIndex == GameState.Instance.WorldState[currentChunkIndex].Count - 1)
                        {
                            // this player is synced
                            continue;
                        }

                        GameState.NextStateRequirement nextStateRequirement = GameState.Instance.WorldState[currentChunkIndex][currentPlayerSyncIndex].NextStateRequirement;

                        if(packet->Channel == (int)EnetLayer.ENetChannel.ASSET_LOAD_REQUEST_OP && nextStateRequirement == GameState.NextStateRequirement.ASSET_LOADED_RESPONSE)
                        {
                            // for now set it for every client, but we need to distinguish them by their userData field
                            PeerManager.Instance.playerState[keyValuePair.Key][currentChunkIndex].SyncStepPointer++;
                        }
                        else if(packet->Channel == (int)EnetLayer.ENetChannel.ADD_ENTITY_OP && nextStateRequirement == GameState.NextStateRequirement.ADDED_ENTITY_RESPONSE)
                        {
                            PeerManager.Instance.playerState[keyValuePair.Key][currentChunkIndex].SyncStepPointer++;
                        }
                    }

                    // work on packets that are not relevant for progress of sync state but need processing for any player
                    foreach (KeyValuePair<ENetPeerHandle, Dictionary<int, PlayerSyncStatus>> keyValuePair in PeerManager.Instance.playerState)
                    {
                        if (packet->Channel != (int)EnetLayer.ENetChannel.SEND_COMPONENT_INTEREST)
                            continue;

                        long entityId = 0;
                        uint interestCount = 0;
                        bool sendAuthority = false;
                        Structs.Structs.InterestOverride* interests = (Structs.Structs.InterestOverride*)new IntPtr(0);

                        if (EnetLayer.PB_EXP_SendComponentInterest_Deserialize(packet->Data, (int)packet->DataLength, &entityId, &interests, &interestCount))
                        {
                            Console.WriteLine("[info] game requests components for entity id: " + entityId);
                            List<Structs.Structs.AddComponentOp> serializedComponents = new List<Structs.Structs.AddComponentOp>();

                            for (int i = 0; i < interestCount; i++)
                            {
                                uint len = 0;
                                byte* buffer;
                                ComponentsSerializer.InitAndSerialize(interests[i].ComponentId, &buffer, &len);

                                if (len <= 0)
                                    continue;

                                Console.WriteLine("[success] initialized and serialized componentId " + interests[i].ComponentId);
                                Structs.Structs.AddComponentOp component;

                                component.ComponentId = interests[i].ComponentId;
                                component.ComponentData = buffer;
                                component.DataLength = (int)len;

                                serializedComponents.Add(component);
                            }

                            if (entityId == 1 && !PeerManager.Instance.clientSetupState.Contains(keyValuePair.Key))
                            {
                                for (var i = 0; i < authoritativeComponents.Length; i++)
                                {
                                    uint len = 0;
                                    byte* buffer;
                                    ComponentsSerializer.InitAndSerialize(authoritativeComponents[i], &buffer, &len);

                                    if (len <= 0)
                                        continue;

                                    Console.WriteLine("[success] initialized and serialized authoritative componentId " + authoritativeComponents[i]);
                                    Structs.Structs.AddComponentOp component;

                                    component.ComponentId = authoritativeComponents[i];
                                    component.ComponentData = buffer;
                                    component.DataLength = (int)len;

                                    serializedComponents.Add(component);
                                }

                                sendAuthority = true;
                                PeerManager.Instance.clientSetupState.Add(keyValuePair.Key);
                            }

                            fixed (Structs.Structs.AddComponentOp* comps = serializedComponents.ToArray())
                            {
                                int len = 0;
                                void* ptr = EnetLayer.PB_EXP_AddComponentOp_Serialize(entityId, comps, (uint)serializedComponents.Count, &len);

                                if (ptr != null && len > 0)
                                {
                                    Console.WriteLine("[success] serialized all requested components, sending them to the game now...");

                                    EnetLayer.ENet_Send(keyValuePair.Key, (int)EnetLayer.ENetChannel.SEND_COMPONENT_INTEREST, ptr, len, (int)ENetPacketFlag.RELIABLE);
                                }
                            }

                            if (sendAuthority)
                            {
                                fixed (Structs.Structs.AuthorityChangeOp* authChangeOps = authoritativeComponents.Select(p => new Structs.Structs.AuthorityChangeOp(p, true)).ToArray())
                                {
                                    int len = 0;
                                    void* ptr = EnetLayer.PB_EXP_AuthorityChangeOp_Serialize(entityId, authChangeOps, (uint)authoritativeComponents.Length, &len);

                                    if (ptr == null || len <= 0)
                                        continue;

                                    Console.WriteLine("[info] serialized all AuthorityRequestOp instructions for authoritative components.");

                                    EnetLayer.ENet_Send(keyValuePair.Key, (int)EnetLayer.ENetChannel.AUTHORITY_CHANGE_OP, ptr, len, (int)ENetPacketFlag.RELIABLE);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("[error] failed to parse component interest request from game.");
                        }
                    }

                    EnetLayer.ENet_Destroy_Packet(new IntPtr(packet));
                }

                // dont wait for GetOplist and then for the Dispatch call as we are the ones who would dispatch the work anyways.
                // sync up players
                foreach (KeyValuePair<ENetPeerHandle, Dictionary<int, PlayerSyncStatus>> keyValuePair in PeerManager.Instance.playerState)
                {
                    int currentChunkIndex = 0;
                    PlayerSyncStatus pStatus = keyValuePair.Value[currentChunkIndex];
                    SyncStep step = GameState.Instance.WorldState[currentChunkIndex][pStatus.SyncStepPointer];

                    if (!pStatus.Performed)
                    {
                        step.Step(keyValuePair.Key);
                        pStatus.Performed = true;
                    }
                }
            }

            server.Dispose();

            Console.WriteLine("[info] shutting down.");
        }
    }
}
