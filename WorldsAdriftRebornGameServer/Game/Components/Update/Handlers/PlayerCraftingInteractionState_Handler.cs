using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bossa.Travellers.Craftingstation;
using WorldsAdriftRebornGameServer.DLLCommunication;
using WorldsAdriftRebornGameServer.Networking.Wrapper;

namespace WorldsAdriftRebornGameServer.Game.Components.Update.Handlers
{
    [RegisterComponentUpdateHandler]
    internal class PlayerCraftingInteractionState_Handler : IComponentUpdateHandler<PlayerCraftingInteractionState, PlayerCraftingInteractionState.Update, PlayerCraftingInteractionState.Data>
    {
        public PlayerCraftingInteractionState_Handler() { Init(1003); }
        protected override void Init( uint ComponentId )
        {
            this.ComponentId = ComponentId;
        }
        public override void HandleUpdate( ENetPeerHandle player, long entityId, PlayerCraftingInteractionState.Update clientComponentUpdate, PlayerCraftingInteractionState.Data serverComponentData)
        {
            clientComponentUpdate.ApplyTo(serverComponentData);
            PlayerCraftingInteractionState.Update serverComponentUpdate = (PlayerCraftingInteractionState.Update)serverComponentData.ToUpdate();

            PlayerCraftingInteractionState.Update test = new PlayerCraftingInteractionState.Update();
            object o = test;

            SendOPHelper.SendComponentUpdateOp(player, entityId, new List<uint> { ComponentId }, new List<object> { serverComponentUpdate });
        }
    }
}
