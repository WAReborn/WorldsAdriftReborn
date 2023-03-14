using System.Collections.Generic;
using Improbable;
using WorldsAdriftRebornGameServer.DLLCommunication;
using WorldsAdriftRebornGameServer.Game.Components;
using static WorldsAdriftRebornGameServer.DLLCommunication.EnetLayer;

namespace WorldsAdriftRebornGameServer.Networking.Wrapper
{
    internal class SendOPHelper
    {
        public static unsafe bool SendAddEntityOP(ENetPeerHandle destination, long entityId, string prefabName, string prefabContext)
        {
            Structs.Structs.AddEntityOp addEntityOp;

            fixed (byte* pn = Translator.ToUtf8Cstr(prefabName))
            {
                fixed (byte* pc = Translator.ToUtf8Cstr(prefabContext))
                {
                    addEntityOp.PrefabName = pn;
                    addEntityOp.PrefabContext = pc;

                    int len = 0;

                    void* ptr = EnetLayer.PB_AddEntityOp_Serialize(&addEntityOp, &len, entityId);

                    if (ptr != null && len != 0)
                    {
                        EnetLayer.ENet_Send(destination, (int)EnetLayer.ENetChannel.ADD_ENTITY_OP, ptr, len, (int)ENetPacketFlag.RELIABLE);
                        return true;
                    }
                    return false;
                }
            }
        }

        public static unsafe bool SendAssetLoadRequestOP(ENetPeerHandle destination, string assetType, string assetName, string assetContext)
        {
            Structs.Structs.AssetLoadRequestOp assetLoadRequestOp;

            fixed (byte* at = Translator.ToUtf8Cstr(assetType))
            {
                fixed (byte* name = Translator.ToUtf8Cstr(assetName))
                {
                    fixed (byte* context = Translator.ToUtf8Cstr(assetContext))
                    {
                        assetLoadRequestOp.AssetType = at;
                        assetLoadRequestOp.Name = name;
                        assetLoadRequestOp.Context = context;

                        int len = 0;

                        void* ptr = EnetLayer.PB_AssetLoadRequestOp_Serialize(&assetLoadRequestOp, &len);

                        if (ptr != null && len != 0)
                        {
                            EnetLayer.ENet_Send(destination, (int)EnetLayer.ENetChannel.ASSET_LOAD_REQUEST_OP, ptr, len, (int)ENetPacketFlag.RELIABLE);
                            return true;
                        }
                        return false;
                    }
                }
            }
        }

        public static unsafe bool SendAddComponentOp( ENetPeerHandle destination, long entityId, List<Structs.Structs.InterestOverride> interests, bool failOnComponentInitError = false )
        {
            fixed(Structs.Structs.InterestOverride* interestsArray = interests.ToArray())
            {
                return SendAddComponentOp(destination, entityId, interestsArray, (uint)interests.Count);
            }
        }

        public static unsafe bool SendAddComponentOp(ENetPeerHandle destination, long entityId, Structs.Structs.InterestOverride* interests, uint interestCount, bool failOnComponentInitError = false )
        {
            List<Structs.Structs.AddComponentOp> serializedComponents = new List<Structs.Structs.AddComponentOp>();

            for (int i = 0; i < interestCount; i++)
            {
                uint len = 0;
                byte* buffer;
                ComponentsSerializer.InitAndSerialize(destination, entityId, interests[i].ComponentId, &buffer, &len);

                if (len <= 0)
                {
                    Console.WriteLine("[error] failed to initialize component " + interests[i].ComponentId);
                    if (failOnComponentInitError)
                    {
                        Console.WriteLine("[info] aborting send of components.");
                        return false;
                    }
                    continue;
                }

                Console.WriteLine("[success] initialized and serialized componentId " + interests[i].ComponentId);
                Structs.Structs.AddComponentOp component;

                component.ComponentId = interests[i].ComponentId;
                component.ComponentData = buffer;
                component.DataLength = (int)len;

                serializedComponents.Add(component);
            }

            fixed (Structs.Structs.AddComponentOp* comps = serializedComponents.ToArray())
            {
                int len = 0;
                void* ptr = EnetLayer.PB_EXP_AddComponentOp_Serialize(entityId, comps, (uint)serializedComponents.Count, &len);

                if (ptr != null && len > 0)
                {
                    Console.WriteLine("[success] serialized all requested components, sending them to the game now...");

                    EnetLayer.ENet_Send(destination, (int)EnetLayer.ENetChannel.SEND_COMPONENT_INTEREST, ptr, len, (int)ENetPacketFlag.RELIABLE);

                    return true;
                }
            }

            return false;
        }

        public static unsafe bool SendComponentUpdateOp(ENetPeerHandle destination, long entityId, List<Structs.Structs.ComponentUpdateOp> updates )
        {
            fixed(Structs.Structs.ComponentUpdateOp* u = updates.ToArray())
            {
                int len = 0;
                void* ptr = EnetLayer.PB_EXP_ComponentUpdateOp_Serialize(entityId, u, (uint)updates.Count, &len);

                if(ptr != null && len > 0)
                {
                    Console.WriteLine("[success] serialized ComponentUpdateOp message for client.");

                    EnetLayer.ENet_Send(destination, (int)EnetLayer.ENetChannel.COMPONENT_UPDATE_OP, ptr, len, (int)ENetPacketFlag.RELIABLE);

                    return true;
                }
            }

            return false;
        }

        public static unsafe bool SendAuthorityChangeOp(ENetPeerHandle destination, long entityId, List<uint> components)
        {
            fixed (Structs.Structs.AuthorityChangeOp* authChangeOps = components.Select(p => new Structs.Structs.AuthorityChangeOp(p, true)).ToArray())
            {
                int len = 0;
                void* ptr = EnetLayer.PB_EXP_AuthorityChangeOp_Serialize(entityId, authChangeOps, (uint)components.Count, &len);

                if (ptr == null || len <= 0)
                {
                    Console.WriteLine("[error] failed to serialize AuthorityChangeOp for component");
                    return false;
                }

                Console.WriteLine("[info] serialized all AuthorityChangeOp instructions for authoritative components.");
                EnetLayer.ENet_Send(destination, (int)EnetLayer.ENetChannel.AUTHORITY_CHANGE_OP, ptr, len, (int)ENetPacketFlag.RELIABLE);

                return true;
            }
        }
    }
}
