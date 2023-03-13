using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WorldsAdriftRebornGameServer.DLLCommunication;
using static WorldsAdriftRebornGameServer.Structs.Structs;

namespace WorldsAdriftRebornGameServer.Structs
{
    internal class Structs
    {
        public struct AssetLoadRequestOp
        {
            public unsafe byte* AssetType;
            public unsafe byte* Name;
            public unsafe byte* Context;
            public unsafe byte* Url;
        }
        public struct AddEntityOp
        {
            public unsafe byte* PrefabName;
            public unsafe byte* PrefabContext;
        }
        public struct InterestOverride
        {
            public InterestOverride(uint ComponentId, byte IsInterested) { this.ComponentId = ComponentId; this.IsInterested = IsInterested; }
            public uint ComponentId;
            public byte IsInterested;
        }
        public struct AddComponentOp
        {
            public uint ComponentId;
            public unsafe byte* ComponentData;
            public int DataLength;
        }
        public struct AuthorityChangeOp
        {
            public AuthorityChangeOp(uint ComponentId, bool HasAuthority ) { this.ComponentId = ComponentId; this.HasAuthority = (byte)(HasAuthority ? 0x01 : 0x00); }
            public uint ComponentId;
            public byte HasAuthority;
        }
    }
}
