

namespace WorldsAdriftRebornGameServer.DLLCommunication
{
    internal class ENetPacket_WrapperHandle : CptrHandle
    {
        protected override bool ReleaseHandle()
        {
            EnetLayer.ENet_Destroy_Packet(handle);
            return true;
        }
    }
}
