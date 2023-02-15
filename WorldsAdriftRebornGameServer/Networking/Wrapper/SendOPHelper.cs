using System.Collections.Generic;
using WorldsAdriftRebornGameServer.DLLCommunication;
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
    }
}
