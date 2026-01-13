using Improbable.Collections;

namespace WorldsAdriftRebornGameServer.Game.World
{
    // Instead of reusing a game type here we need to store additional data like stack size and lore
    // So a custom definition is better
    public class ItemDefinition
    {
        public string itemTypeID { get; set; }
        public string name { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int stacksize { get; set; } = -1;
        public string iconName { get; set; }
        public bool equippable { get; set; }
        public string characterSlot { get; set; } = "None";
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
}
