using System.ComponentModel;
using Bossa.Travellers.Inventory;
using Bossa.Travellers.Player;
using Improbable.Worker.Internal;
using WorldsAdriftRebornGameServer.DLLCommunication;
using WorldsAdriftRebornGameServer.Game.Items;
using WorldsAdriftRebornGameServer.Networking.Wrapper;

namespace WorldsAdriftRebornGameServer.Game.Components.Update.Handlers
{
    [RegisterComponentUpdateHandler]
    internal class InventoryModificationState_Handler : IComponentUpdateHandler<InventoryModificationState, InventoryModificationState.Update, InventoryModificationState.Data>
    {
        public InventoryModificationState_Handler() { Init(1082); }
        protected override void Init( uint ComponentId )
        {
            this.ComponentId = ComponentId;
        }
        public override void HandleUpdate( ENetPeerHandle player, long entityId, InventoryModificationState.Update clientComponentUpdate, InventoryModificationState.Data serverComponentData)
        {
            clientComponentUpdate.ApplyTo(serverComponentData);

            InventoryModificationState.Update serverComponentUpdate = (InventoryModificationState.Update)serverComponentData.ToUpdate();

            for (int j = 0; j < clientComponentUpdate.equipWearable.Count; j++)
            {
                Console.WriteLine("[info] game wants to equip a wearable");
                Console.WriteLine("[info] id: " + clientComponentUpdate.equipWearable[j].itemId);
                Console.WriteLine("[info] slot: " + clientComponentUpdate.equipWearable[j].slotId);
                Console.WriteLine("[info] lockbox: " + clientComponentUpdate.equipWearable[j].isLockboxItem);

                // send updates to equip the wearables
                WearableUtilsState.Update storedWearableUtilsState = (WearableUtilsState.Update)((WearableUtilsState.Data)ClientObjects.Instance.Dereference(GameState.Instance.ComponentMap[player][entityId][1280])).ToUpdate();
                PlayerPropertiesState.Update storedPlayerPropertiesState = (PlayerPropertiesState.Update)((PlayerPropertiesState.Data)ClientObjects.Instance.Dereference(GameState.Instance.ComponentMap[player][entityId][1088])).ToUpdate();
                InventoryState.Update storedInventoryState = (InventoryState.Update)((InventoryState.Data)ClientObjects.Instance.Dereference(GameState.Instance.ComponentMap[player][entityId][1081])).ToUpdate();

                storedWearableUtilsState.SetItemIds(new Improbable.Collections.List<int> { clientComponentUpdate.equipWearable[j].itemId }).SetHealths(new Improbable.Collections.List<float> { 100f }).SetActive(new Improbable.Collections.List<bool> { true });
                for (int k = 0; k < storedInventoryState.inventoryList.Value.Count; k++)
                {
                    if (storedInventoryState.inventoryList.Value[k].itemId == clientComponentUpdate.equipWearable[j].itemId)
                    {
                        ScalaSlottedInventoryItem modifiedItem = storedInventoryState.inventoryList.Value[k];
                        modifiedItem.slotType = ItemHelper.GetItem(storedInventoryState.inventoryList.Value[k].itemTypeId).characterSlot;

                        storedInventoryState.inventoryList.Value[k] = modifiedItem;
                    }
                }

                // NOTE: its absolutely crucial to send 1081 before 1088, this is because 1081 sets the item slotType to something meaningfull while 1088 expects some meaningful value if it should be equipped
                SendOPHelper.SendComponentUpdateOp(player, entityId, new List<uint> { 1280, 1081, 1088 }, new List<object> { storedWearableUtilsState, storedInventoryState, storedPlayerPropertiesState });
            }
            for (int j = 0; j < clientComponentUpdate.equipTool.Count; j++)
            {
                Console.WriteLine("[info] game wants to equip a tool");
                Console.WriteLine("[info] id: " + clientComponentUpdate.equipTool[j].itemId);
            }
            for (int j = 0; j < clientComponentUpdate.craftItem.Count; j++)
            {
                Console.WriteLine("[info] game wants to craft an item");
                Console.WriteLine("[info] inventoryEntityId: " + clientComponentUpdate.craftItem[j].inventoryEntityId);
                Console.WriteLine("[info] itemTypeId: " + clientComponentUpdate.craftItem[j].itemTypeId);
                Console.WriteLine("[info] amount: " + clientComponentUpdate.craftItem[j].amount);
            }
            for (int j = 0; j < clientComponentUpdate.crossInventoryMoveItem.Count; j++)
            {
                Console.WriteLine("[info] game wants to cross inventory move item");
                Console.WriteLine("[info] srcItemId: " + clientComponentUpdate.crossInventoryMoveItem[j].srcItemId);
                Console.WriteLine("[info] xPos: " + clientComponentUpdate.crossInventoryMoveItem[j].xPos);
                Console.WriteLine("[info] yPos: " + clientComponentUpdate.crossInventoryMoveItem[j].yPos);
                Console.WriteLine("[info] rotate: " + clientComponentUpdate.crossInventoryMoveItem[j].rotate);
                Console.WriteLine("[info] srcInventoryEntityId: " + clientComponentUpdate.crossInventoryMoveItem[j].srcInventoryEntityId);
                Console.WriteLine("[info] destInventoryItemId: " + clientComponentUpdate.crossInventoryMoveItem[j].destInventoryEntityId);
                Console.WriteLine("[info] isLockBoxItem: " + clientComponentUpdate.crossInventoryMoveItem[j].isLockboxItem);
            }
            for (int j = 0; j < clientComponentUpdate.moveItem.Count; j++)
            {
                Console.WriteLine("[info] game wants to move an inventory item");
                Console.WriteLine("[info] inventoryEntityId: " + clientComponentUpdate.moveItem[j].inventoryEntityId);
                Console.WriteLine("[info] itemId: " + clientComponentUpdate.moveItem[j].itemId);
                Console.WriteLine("[info] xPos: " + clientComponentUpdate.moveItem[j].xPos);
                Console.WriteLine("[info] yPos: " + clientComponentUpdate.moveItem[j].yPos);
                Console.WriteLine("[info] rotate: " + clientComponentUpdate.moveItem[j].rotate);
                Console.WriteLine("[info] isLockboxItem: " + clientComponentUpdate.moveItem[j].isLockboxItem);
            }
            for (int j = 0; j < clientComponentUpdate.removeFromHotBar.Count; j++)
            {
                Console.WriteLine("[info] game wants to remove from hotbar");
                Console.WriteLine("[info] slotIndex: " + clientComponentUpdate.removeFromHotBar[j].slotIndex);
                Console.WriteLine("[info] isLockboxItem: " + clientComponentUpdate.removeFromHotBar[j].isLockboxItem);
            }
            for (int j = 0; j < clientComponentUpdate.assignToHotBar.Count; j++)
            {
                Console.WriteLine("[info] game wants to assign to hotbar");
                Console.WriteLine("[info] itemId: " + clientComponentUpdate.assignToHotBar[j].itemId);
                Console.WriteLine("[info] slotIndex: " + clientComponentUpdate.assignToHotBar[j].slotIndex);
                Console.WriteLine("[info] isLockboxItem: " + clientComponentUpdate.assignToHotBar[j].isLockboxItem);
            }

            SendOPHelper.SendComponentUpdateOp(player, entityId, new List<uint> { ComponentId }, new List<object> { serverComponentUpdate });
        }
    }
}
