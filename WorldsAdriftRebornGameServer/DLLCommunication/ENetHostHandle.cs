

namespace WorldsAdriftRebornGameServer.DLLCommunication
{
    internal class ENetHostHandle : CptrHandle
    {
        protected override bool ReleaseHandle()
        {
            EnetLayer.ENet_Deinitialize(handle);
            return true;
        }
    }
}
