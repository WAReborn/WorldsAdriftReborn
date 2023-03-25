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
    internal abstract class IComponentUpdateHandler<T, H>
    {
        public uint ComponentId { get; protected set; }
        public Type DataType { get; protected set; }
        public Type UpdateType { get; protected set; }
        protected abstract void Init(uint ComponentId, Type UpdateType, Type DataType);
        public abstract void HandleUpdate(ENetPeerHandle player, long entityId, T clientComponentUpdate, H serverComponentData);
    }
}
