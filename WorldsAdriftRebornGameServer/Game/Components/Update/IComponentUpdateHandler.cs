using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Improbable.Entity.Component;
using Improbable.Worker;
using WorldsAdriftRebornGameServer.DLLCommunication;

namespace WorldsAdriftRebornGameServer.Game.Components.Update
{
    internal abstract class IComponentUpdateHandler<T, H, C>
    {
        public uint ComponentId { get; protected set; }
        protected abstract void Init( uint ComponentId );
        public abstract void HandleUpdate(ENetPeerHandle player, long entityId, H clientComponentUpdate, C serverComponentData);
    }
}
