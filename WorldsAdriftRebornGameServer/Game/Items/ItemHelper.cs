using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Bossa.Travellers.Inventory;
using Improbable.Collections;

namespace WorldsAdriftRebornGameServer.Game.Items
{
    public static class ItemHelper
    {
        private static Dictionary<string, ValidItem> _allItems = new Dictionary<string, ValidItem>();
        private static readonly string itemPath = Path.Combine(
                                                            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                            "Game/Items/Config/itemData.json"
                                                            );

        public class ValidItem
        {
            public string itemTypeID { get; set; }
            public string name { get; set; }
            public int height { get; set; }
            public int width { get; set; }
            public int stacksize { get; set; } = -1;
            public string iconName { get; set; }
            public bool equippable { get; set; }
            public string characterslot { get; set; } = "None";
            public string category { get; set; } = "";
            public string description { get; set; } = "";
            public int rarity { get; set; } = 0;
            public Dictionary<string, string> metadata { get; set; }

            public Option<int> GetRarity()
            {
                return new Option<int>(rarity);
            }

            public Map<string, string> Meta( Dictionary<string, string> overrides = null )
            {
                var m = new Map<string, string>(metadata);
                if (overrides == null)
                    return m;
                foreach (var i in overrides)
                    m[i.Key] = i.Value;
                return m;
            }
        }

        public static Dictionary<string, ValidItem> AllItems
        {
            get
            {
                if (_allItems.Count > 0)
                    return _allItems;

                _allItems = new Dictionary<string, ValidItem>();
                var itemList = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.List<ValidItem>>(File.ReadAllText(itemPath));

                foreach (ValidItem item in itemList)
                {
                    if (string.IsNullOrEmpty(item.itemTypeID))
                        continue;
                    _allItems[item.itemTypeID] = item;
                    // if (!string.IsNullOrEmpty(item.description)) ItemDescriptions.Add(item.itemTypeID, item.description);
                }

                Console.WriteLine($"{_allItems.Count}/{itemList.Count} listed items are valid");
                return _allItems;
            }
        }

        public static ValidItem GetItem( string itemTypeId ) => AllItems[itemTypeId];


        public static ScalaSlottedInventoryItem MakeItem( int itemId, string itemTypeId, int x = 0, int y = 0,
            int amount = 1, int quality = 0, bool stashItem = false, int hotBarSlot = -1,
            Dictionary<string, string> metaOverrides = null )
        {
            var item = GetItem(itemTypeId);
            return new ScalaSlottedInventoryItem(itemId, itemTypeId, amount, item.characterslot, -1, x, y, false,
                hotBarSlot, 0, quality, stashItem, item.Meta(metaOverrides), item.GetRarity());
        }

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
                MakeItem(1100, "gold", 2, 3, 40, 9),
                MakeItem(1101, "glider", 2, 5)
            };
        }

        private static System.Collections.Generic.List<ScalaSlottedInventoryItem> DevItems()
        {
            return new System.Collections.Generic.List<ScalaSlottedInventoryItem>
            {
                MakeItem(6, "head_olk", stashItem: true),
                MakeItem(7, "head_devcap", stashItem: true),
                MakeItem(8, "torso_devjacket", stashItem: true)
            };
        }

        private static System.Collections.Generic.List<ScalaSlottedInventoryItem> PioneerItems()
        {
            return new System.Collections.Generic.List<ScalaSlottedInventoryItem>
            {
                MakeItem(9, "head_drissiancowl", stashItem: true)
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
                MakeItem(23, "torso_tamoe", stashItem: true),
                MakeItem(24, "legs_tamoe", stashItem: true),
                MakeItem(25, "head_yharma_mask", stashItem: true),
                MakeItem(26, "torso_vinicoti", stashItem: true),
                MakeItem(27, "legs_vinicoti", stashItem: true),
                MakeItem(28, "head_mask_natiq", stashItem: true),
                MakeItem(29, "torso_kahi", stashItem: true),
                MakeItem(30, "legs_kahi", stashItem: true),
            };
        }
    }
}
