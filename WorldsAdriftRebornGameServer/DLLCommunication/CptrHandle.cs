

using System.Runtime.InteropServices;

namespace WorldsAdriftRebornGameServer.DLLCommunication
{
    internal abstract class CptrHandle : SafeHandle
    {
        public CptrHandle() : base((IntPtr)0, true)
        {

        }

        public override bool IsInvalid
        {
            get => handle == IntPtr.Zero;
        }
    }
}
