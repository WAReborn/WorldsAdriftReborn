

namespace WorldsAdriftRebornGameServer.DLLCommunication
{
    internal class ENetPeerHandle : CptrHandle
    {
        ENetHostHandle client;
        public ENetPeerHandle(IntPtr peer, ENetHostHandle client )
        {
            handle = peer;
            this.client = client;
        }
        public void SetHostHandle(ENetHostHandle client )
        {
            this.client = client;
        }
        protected override bool ReleaseHandle()
        {
            EnetLayer.ENet_Disconnect(handle, client);
            return true;
        }
    }
}
