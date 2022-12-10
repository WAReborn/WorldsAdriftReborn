using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Improbable.Worker;
using WorldsAdriftRebornGameServer.DLLCommunication;
using WorldsAdriftRebornGameServer.Game;
using WorldsAdriftRebornGameServer.Game.Components;
using WorldsAdriftRebornGameServer.Networking.Singleton;

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
                PeerManager.Instance.playerState.Add(ePeer, Game.GameState.State.NEWLY_CONNECTED);
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
            ENetHostHandle server = EnetLayer.ENet_Create_Host(7777, 1, 3, 0, 0);

            if (server.IsInvalid)
            {
                Console.WriteLine("[error] failed to create host and listen on network interface.");

                EnetLayer.ENet_Deinitialize(new IntPtr(0));
                return;
            }

            Console.WriteLine("[info] successfully initialized networking, now waiting for connections and data.");
            PeerManager.Instance.SetENetHostHandle(server);

            while (keepRunning)
            {
                EnetLayer.ENetPacket_Wrapper* packet = EnetLayer.ENet_Poll(server, 50, Marshal.GetFunctionPointerForDelegate(callbackC), Marshal.GetFunctionPointerForDelegate(callbackD));
                if(packet != null)
                {
                    foreach (KeyValuePair<ENetPeerHandle, GameState.State> keyValuePair in PeerManager.Instance.playerState)
                    {
                        if(packet->channel == (int)EnetLayer.ENetChannel.AssetLoadRequestOp && keyValuePair.Value == GameState.State.NEWLY_CONNECTED_PENDING)
                        {
                            // for now set it for every client, but we need to distinguish them by their userData field
                            PeerManager.Instance.playerState[keyValuePair.Key] = GameState.State.PLAYER_ASSET_LOADED;
                        }
                        else if (packet->channel == (int)EnetLayer.ENetChannel.AssetLoadRequestOp && keyValuePair.Value == GameState.State.ISLAND_LOAD_PENDING)
                        {
                            // for now set it for every client, but we need to distinguish them by their userData field
                            PeerManager.Instance.playerState[keyValuePair.Key] = GameState.State.ISLAND_LOADED;
                        }
                        else if(packet->channel == (int)EnetLayer.ENetChannel.AddEntityOp && keyValuePair.Value == GameState.State.ISLAND_SPAWN_PENDING)
                        {
                            PeerManager.Instance.playerState[keyValuePair.Key] = GameState.State.ISLAND_SPAWNED;
                        }
                        else if (packet->channel == (int)EnetLayer.ENetChannel.AddEntityOp && keyValuePair.Value == GameState.State.PLAYER_SPAWN_PENDING)
                        {
                            PeerManager.Instance.playerState[keyValuePair.Key] = GameState.State.PLAYER_SPAWNED;
                        }

                        if(packet->channel == (int)EnetLayer.ENetChannel.SendComponentInterest)
                        {
                            long entityId = 0;
                            uint interestCount = 0;
                            Structs.Structs.InterestOverride* interests = (Structs.Structs.InterestOverride * )new IntPtr(0);

                            if (EnetLayer.PB_EXP_SendComponentInterest_Deserialize(packet->data, (int)packet->dataLength, &entityId, &interests, &interestCount))
                            {
                                Console.WriteLine("[info] game requests components for entity id: " + entityId);
                                List<Structs.Structs.AddComponentOp> serializedComponents = new List<Structs.Structs.AddComponentOp>();

                                for(int i = 0; i < interestCount; i++)
                                {
                                    uint len = 0;
                                    byte* buffer;
                                    ComponentsSerializer.InitAndSerialize(interests[i].ComponentId, &buffer, &len);

                                    if(len > 0)
                                    {
                                        Console.WriteLine("[success] initialized and serialized componentId " + interests[i].ComponentId);
                                        Structs.Structs.AddComponentOp component;

                                        component.ComponentId = interests[i].ComponentId;
                                        component.ComponentData = buffer;
                                        component.DataLength = (int)len;

                                        serializedComponents.Add(component);
                                    }
                                }

                                fixed (Structs.Structs.AddComponentOp* comps = serializedComponents.ToArray())
                                {
                                    int len = 0;
                                    void* ptr = EnetLayer.PB_EXP_AddComponentOp_Serialize(entityId, comps, (uint)serializedComponents.Count, &len);

                                    if(ptr != null && len > 0)
                                    {
                                        Console.WriteLine("[success] serialized all requested components, sending them to the game now...");

                                        EnetLayer.ENet_Send(keyValuePair.Key, (int)EnetLayer.ENetChannel.SendComponentInterest, ptr, len, 1);
                                        EnetLayer.ENet_Flush(server); // for now just send out without waiting for enet's internal sending in ENet_Poll()
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("[error] failed to parse component interest request from game.");
                            }
                        }
                    }
                    EnetLayer.ENet_Destroy_Packet(new IntPtr(packet));
                }

                // dont wait for GetOplist and then for the Dispatch call as we are the ones who would dispatch the work anyways.
                foreach (KeyValuePair<ENetPeerHandle, GameState.State> keyValuePair in PeerManager.Instance.playerState)
                {
                    if(keyValuePair.Value == GameState.State.NEWLY_CONNECTED)
                    {
                        Console.WriteLine("[info] requesting the game to load the player asset...");
                        Structs.Structs.AssetLoadRequestOp assetLoadRequestOp;

                        fixed(byte* assetType = Translator.ToUtf8Cstr("notNeeded?"))
                        {
                            fixed(byte* name = Translator.ToUtf8Cstr("Traveller"))
                            {
                                fixed(byte* context = Translator.ToUtf8Cstr("Player"))
                                {
                                    assetLoadRequestOp.AssetType = assetType;
                                    assetLoadRequestOp.Name = name;
                                    assetLoadRequestOp.Context = context;

                                    int len = 0;

                                    void* ptr = EnetLayer.PB_AssetLoadRequestOp_Serialize(&assetLoadRequestOp, &len);

                                    if(ptr != null && len != 0)
                                    {
                                        Console.WriteLine("[info] successfully serialized AssetLoadRequestOp.");

                                        EnetLayer.ENet_Send(keyValuePair.Key, (int)EnetLayer.ENetChannel.AssetLoadRequestOp, ptr, len, 1);
                                        EnetLayer.ENet_Flush(server); // for now just send out without waiting for enet's internal sending in ENet_Poll()
                                    }
                                }
                            }
                        }

                        PeerManager.Instance.playerState[keyValuePair.Key] = GameState.State.NEWLY_CONNECTED_PENDING;
                    }
                    else if(keyValuePair.Value == GameState.State.PLAYER_ASSET_LOADED)
                    {
                        Console.WriteLine("[info] requesting the game to load the island from its asset bundles...");
                        Structs.Structs.AssetLoadRequestOp assetLoadRequestOp;

                        fixed (byte* assetType = Translator.ToUtf8Cstr("notNeeded?"))
                        {
                            fixed (byte* name = Translator.ToUtf8Cstr("949069116@Island"))
                            {
                                fixed (byte* context = Translator.ToUtf8Cstr("notNeeded?"))
                                {
                                    assetLoadRequestOp.AssetType = assetType;
                                    assetLoadRequestOp.Name = name;
                                    assetLoadRequestOp.Context = context;

                                    int len = 0;

                                    void* ptr = EnetLayer.PB_AssetLoadRequestOp_Serialize(&assetLoadRequestOp, &len);

                                    if (ptr != null && len != 0)
                                    {
                                        Console.WriteLine("[info] successfully serialized AssetLoadRequestOp.");

                                        EnetLayer.ENet_Send(keyValuePair.Key, (int)EnetLayer.ENetChannel.AssetLoadRequestOp, ptr, len, 1);
                                        EnetLayer.ENet_Flush(server); // for now just send out without waiting for enet's internal sending in ENet_Poll()
                                    }
                                }
                            }
                        }

                        PeerManager.Instance.playerState[keyValuePair.Key] = GameState.State.ISLAND_LOAD_PENDING;
                    }
                    else if(keyValuePair.Value == GameState.State.ISLAND_LOADED)
                    {
                        Console.WriteLine("[success] island asset loaded. requesting loading of island...");
                        Structs.Structs.AddEntityOp addEntityOp;

                        fixed(byte* prefabName = Translator.ToUtf8Cstr("949069116@Island"))
                        {
                            fixed(byte* prefabContext = Translator.ToUtf8Cstr("notNeeded?"))
                            {
                                addEntityOp.PrefabName = prefabName;
                                addEntityOp.PrefabContext = prefabContext;

                                int len = 0;

                                void* ptr = EnetLayer.PB_AddEntityOp_Serialize(&addEntityOp, &len, 2);

                                if(ptr != null && len != 0)
                                {
                                    Console.WriteLine("[info] successfully serialized AddEntityOp.");

                                    EnetLayer.ENet_Send(keyValuePair.Key, (int)EnetLayer.ENetChannel.AddEntityOp, ptr, len, 1);
                                    EnetLayer.ENet_Flush(server);
                                }
                            }
                        }

                        PeerManager.Instance.playerState[keyValuePair.Key] = GameState.State.ISLAND_SPAWN_PENDING;
                    }
                    else if(keyValuePair.Value == GameState.State.ISLAND_SPAWNED)
                    {
                        Console.WriteLine("[info] client ack'ed island spawning instruction (info by sdk, does not mean it truly spawned). requesting to spawn player...");
                        Structs.Structs.AddEntityOp addEntityOp;

                        fixed (byte* prefabName = Translator.ToUtf8Cstr("Traveller"))
                        {
                            fixed (byte* prefabContext = Translator.ToUtf8Cstr("Player"))
                            {
                                addEntityOp.PrefabName = prefabName;
                                addEntityOp.PrefabContext = prefabContext;

                                int len = 0;

                                void* ptr = EnetLayer.PB_AddEntityOp_Serialize(&addEntityOp, &len, 1);

                                if (ptr != null && len != 0)
                                {
                                    Console.WriteLine("[info] successfully serialized AddEntityOp.");

                                    EnetLayer.ENet_Send(keyValuePair.Key, (int)EnetLayer.ENetChannel.AddEntityOp, ptr, len, 1);
                                    EnetLayer.ENet_Flush(server);
                                }
                            }
                        }

                        PeerManager.Instance.playerState[keyValuePair.Key] = GameState.State.PLAYER_SPAWN_PENDING;
                    }
                    else if(keyValuePair.Value == GameState.State.PLAYER_SPAWNED)
                    {
                        Console.WriteLine("[info] client ack'ed player spawning instruction (info by sdk, does not mean it truly spawned).");
                        PeerManager.Instance.playerState[keyValuePair.Key] = GameState.State.DONE;
                    }
                }
            }

            server.Dispose();

            Console.WriteLine("[info] shutting down.");
        }
    }
}
