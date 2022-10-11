using System.Runtime.InteropServices;

namespace WorldsAdriftRebornGameServer.DLLCommunication
{
    internal class EnetLayer
    {
        public struct ENetPacket_Wrapper
        {
            public unsafe byte* data;
            public long dataLength;
            public unsafe byte* userData;
            public int channel;
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

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void ENet_Poll_Callback( IntPtr peer );
    }
}
