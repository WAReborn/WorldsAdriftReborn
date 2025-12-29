using System.Text.Json;
using Bossa.Travellers.Inventory;
using Improbable.Collections;

namespace WorldsAdriftRebornGameServer.Game.World
{
    public static class ItemHelper
    {
        private static Dictionary<string, ItemDefinition> allItems = new Dictionary<string, ItemDefinition>();
        // private static readonly string itemPath = Path.Combine(
        //                                                     Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
        //                                                     "Game/Items/Config/itemData.json"
        //                                                     );

        public static Dictionary<string, ItemDefinition> AllItems
        {
            get
            {
                if (allItems.Count > 0)
                    return allItems;

                allItems = ItemList.LoadItemList();
                return allItems;
            }
        }

        public static ItemDefinition GetItem( string itemTypeId ) => AllItems[itemTypeId];

        public static ScalaSlottedInventoryItem MakeItem( int itemId, string itemTypeId, int x = 0, int y = 0,
            int amount = 1, int quality = 0, bool stashItem = false, int hotBarSlot = -1,
            Dictionary<string, string> metaOverrides = null, bool slotted = false)
        {
            var item = GetItem(itemTypeId);
            return new ScalaSlottedInventoryItem(itemId, itemTypeId, amount,  !slotted ? "None" : item.characterSlot, -1, x, y, false,
                hotBarSlot, 0, quality, stashItem, item.Meta(metaOverrides), item.GetRarity());
        }
        
                public static string GetReferenceItems()
        {
            System.Collections.Generic.List<object> o = new();
            foreach (ItemDefinition v in AllItems.Values)
                o.Add(new
                {
                    itemTypeId = v.itemTypeID,
                    v.name,
                    v.category,
                    v.iconName,
                    stackingMax = v.stacksize,
                    numOfSlotsWidth = v.width,
                    numOfSlotsHeight = v.height,
                    v.equippable,
                    wearable = v.characterSlot
                });
            return JsonSerializer.Serialize(o);
        }

        public static Map<string, string> GetDescriptions(bool resources = false)
        {
            Map<string, string> map = new();
            foreach (ItemDefinition item in AllItems.Values)
            {
                bool isResource = item.category == "Fuel" || item.category == "Metal" || item.category == "Wood";
                if ((resources && !isResource) || (!resources && isResource))
                {
                    continue;
                }

                map.Add(item.itemTypeID, item.description);
            }

            return map;
        }

        public static Map<string, string> BundleDescriptions() => new() { { "steamInvBundle-xmas_present", AllItems["steamInvBundle-xmas_present"].description } };

        public static Improbable.Collections.List<ScalaSlottedInventoryItem> GetStashItems( bool steam = false,
            bool pioneer = false, bool founders = false, bool dev = false )
        {
            var i = new Improbable.Collections.List<ScalaSlottedInventoryItem>();

            if (dev)
                i.AddRange(DevItems());
            if (founders)
                i.AddRange(FoundersItems());
            if (pioneer)
                i.AddRange(PioneerItems());
            if (steam)
                i.AddRange(SteamItems());

            return i;
        }

        // First 100 itemIds are reserved for client logic
        public static Improbable.Collections.List<ScalaSlottedInventoryItem> GetDefaultItems()
        {
            return new Improbable.Collections.List<ScalaSlottedInventoryItem>
            {
                MakeItem(1, "gauntlet_salvage", -1, -1, hotBarSlot: 0),
                MakeItem(2, "gauntlet_repair", -1, -1, hotBarSlot: 1),
                MakeItem(3, "gauntlet_build", -1, -1, hotBarSlot: 2),
                MakeItem(4, "gauntlet_scanner", -1, -1, hotBarSlot: 3),
                //MakeItem(1100, "gold", 2, 3, 40, 9),
                MakeItem(1101, "glider"),
                MakeItem(1102, "torso_poncho", 0, 4),
                MakeItem(1103, "head_devhat", 3, 0)
            };
        }

        private static System.Collections.Generic.List<ScalaSlottedInventoryItem> DevItems()
        {
            return new System.Collections.Generic.List<ScalaSlottedInventoryItem>
            {
                MakeItem(6, "head_olk", stashItem: true),
                MakeItem(7, "head_devhat", stashItem: true),
                MakeItem(8, "torso_devjacket", stashItem: true)
            };
        }

        private static System.Collections.Generic.List<ScalaSlottedInventoryItem> PioneerItems()
        {
            return new System.Collections.Generic.List<ScalaSlottedInventoryItem>
            {
                MakeItem(9, "head_pioneer", stashItem: true)
            };
        }

        private static System.Collections.Generic.List<ScalaSlottedInventoryItem> FoundersItems()
        {
            return new System.Collections.Generic.List<ScalaSlottedInventoryItem>
            {
                MakeItem(10, "head_skullmask", stashItem: true),
                MakeItem(11, "torso_tribal_skeleton", stashItem: true),
                MakeItem(12, "legs_tribal_skeleton", stashItem: true),
                MakeItem(13, "head_christmas", stashItem: true),
                MakeItem(14, "head_hoodVariantA", stashItem: true)
            };
        }

        private static System.Collections.Generic.List<ScalaSlottedInventoryItem> SteamItems()
        {
            return new System.Collections.Generic.List<ScalaSlottedInventoryItem>
            {
                MakeItem(20, "head_bargu_mask", stashItem: true),
                MakeItem(21, "head_intucki_mask", stashItem: true),
                MakeItem(22, "head_tamoe_mask", stashItem: true),
                MakeItem(23, "torso_tribal_tamoe", stashItem: true),
                MakeItem(24, "legs_tribal_tamoe", stashItem: true),
                MakeItem(25, "head_yharma_mask", stashItem: true),
                MakeItem(26, "torso_summer_male", stashItem: true),
                MakeItem(27, "legs_summer", stashItem: true),
                MakeItem(28, "head_christmas_2018", stashItem: true),
                MakeItem(29, "torso_christmas_2018", stashItem: true),
                MakeItem(30, "legs_christmas_2018", stashItem: true),
            };
        }
    }
}
