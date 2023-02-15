using System.Runtime.InteropServices;
using static WorldsAdriftRebornGameServer.Structs.Structs;

namespace WorldsAdriftRebornGameServer.DLLCommunication
{
    internal class EnetLayer
    {
        public enum ENetChannel
        {
            ASSET_LOAD_REQUEST_OP = 0,
            ADD_ENTITY_OP = 1,
            SEND_COMPONENT_INTEREST = 2,
            AUTHORITY_CHANGE_OP = 3
        }
        public enum ENetPacketFlag
        {
            RELIABLE = 0,
            UNRELIABLE = 1,
            UNRELIABLE_UNSEQUENCED = 2
        }
        public struct ENetPacket_Wrapper
        {
            public unsafe byte* Data;
            public long DataLength;
            public unsafe byte* UserData;
            public int Channel;
        }

        [DllImport("CoreSdkDll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ENet_EXP_Initialize")]
        public static extern int ENet_Initialize();

        [DllImport("CoreSdkDll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ENet_EXP_Create_Host")]
        public static extern ENetHostHandle ENet_Create_Host( int port, int maxConnections, int maxChannels, int inBandwidth, int outBandwidth );

        [DllImport("CoreSdkDll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ENet_EXP_Poll")]
        public static unsafe extern ENetPacket_Wrapper* ENet_Poll( ENetHostHandle client, int waitTime, IntPtr callbackC, IntPtr callbackD );

        [DllImport("CoreSdkDll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ENet_EXP_Destroy_Packet")]
        public static extern void ENet_Destroy_Packet( IntPtr packet );

        [DllImport("CoreSdkDll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ENet_EXP_Send")]
        public static unsafe extern void ENet_Send( ENetPeerHandle peer, int channel, void* data, long len, int flag );

        [DllImport("CoreSdkDll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ENet_EXP_Disconnect")]
        public static extern void ENet_Disconnect( IntPtr peer, ENetHostHandle client );

        [DllImport("CoreSdkDll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ENet_EXP_Deinitialize")]
        public static extern void ENet_Deinitialize( IntPtr client );

        [DllImport("CoreSdkDll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ENet_EXP_Flush")]
        public static extern void ENet_Flush( ENetHostHandle client );

        [DllImport("CoreSdkDll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "PB_EXP_AssetLoadRequestOp_Serialize")]
        public static unsafe extern void* PB_AssetLoadRequestOp_Serialize( AssetLoadRequestOp* op, int* len );

        [DllImport("CoreSdkDll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "PB_EXP_AddEntityOp_Serialize")]
        public static unsafe extern void* PB_AddEntityOp_Serialize( AddEntityOp* op, int* len, long entityId );

        [DllImport("CoreSdkDll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "PB_EXP_SendComponentInterest_Deserialize")]
        public static unsafe extern bool PB_EXP_SendComponentInterest_Deserialize(void* data, int len, long* entityId, InterestOverride** interest_override, uint* interest_override_count);

        [DllImport("CoreSdkDll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "PB_EXP_AddComponentOp_Serialize")]
        public static unsafe extern void* PB_EXP_AddComponentOp_Serialize( long entityId, AddComponentOp* addComponentOp, uint addComponentOp_count, int* len );

        [DllImport("CoreSdkDll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "PB_EXP_AuthorityChangeOp_Serialize")]
        public static unsafe extern void* PB_EXP_AuthorityChangeOp_Serialize( long entityId, AuthorityChangeOp* authorityChangeOp, uint authorityChangeOp_count, int* len );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void ENet_Poll_Callback( IntPtr peer );
    }
}
