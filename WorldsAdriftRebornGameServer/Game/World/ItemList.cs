// #define DEBUG_INVALID_ITEMS

using System.Reflection;
using System.Text.Json;
using WorldsAdriftRebornGameServer.Game.World.Config;

namespace WorldsAdriftRebornGameServer.Game.World;

public sealed class ItemList : CsvLoader<ItemDefinition>
{
    protected override ItemDefinition ParseRow(string[] headers, string[] values)
    {
        var item = new ItemDefinition();

        for (int i = 0; i < headers.Length && i < values.Length; i++)
        {
            var h = headers[i];
            var v = values[i];
            var vempty = string.IsNullOrEmpty(v);

            try
            {
                switch (h)
                {
                    case "itemTypeID":
                        item.itemTypeID = v;
                        break;

                    case "name":
                        item.name = v;
                        break;

                    case "height":
                        item.height = vempty ? 1 : int.Parse(v);
                        break;

                    case "width":
                        item.width = vempty? 1 : int.Parse(v);
                        break;

                    case "stacksize":
                        item.stacksize = vempty ? 999 : int.Parse(v);
                        break;

                    case "iconName":
                        item.iconName = v;
                        break;

                    case "equippable" when !vempty:
                        item.equippable = bool.Parse(v);
                        break;

                    case "characterSlot":
                        item.characterSlot = v;
                        break;

                    case "category":
                        item.category = v;
                        break;

                    case "description":
                        item.description = v;
                        break;

                    case "rarity" when !vempty:
                        item.rarity = int.Parse(v);
                        break;

                    case "metadata":
                        try
                        {
                            item.metadata = (string.IsNullOrWhiteSpace(v)
                                                ? new Dictionary<string, string>()
                                                : JsonSerializer.Deserialize<Dictionary<string, string>>(v)) ??
                                            new Dictionary<string, string>();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(
                                $"ERROR - Failed to parse metadata on {values[0]}: {ex.Message}\nStack: {ex.StackTrace}\nBody: {v}");
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR - Failed to parse {h}: {ex.Message}");
            }
        }

        return item;
    }
    
    
    private static readonly string itemPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "Game", "World", "Config", "itemData.csv");

    public static Dictionary<string, ItemDefinition> LoadItemList()
    {
        var dict = new Dictionary<string, ItemDefinition>();
        var contents = Load<ItemList>(itemPath);

        foreach (var item in contents)
        {
            if (string.IsNullOrWhiteSpace(item.itemTypeID))
            {
#if DEBUG_INVALID_ITEMS
                Console.WriteLine("Invalid Item: {item.name} {item.description} {item.iconName}")
#endif
                continue;
            }

            dict[item.itemTypeID] = item;
        }

        Console.WriteLine($"Loaded {dict.Count}/{contents.Count} items");
        return dict;
    }
}
