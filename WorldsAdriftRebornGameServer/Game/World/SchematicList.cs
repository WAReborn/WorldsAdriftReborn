using System.Reflection;
using System.Text.Json;
using Improbable.Collections;
using WorldsAdriftRebornGameServer.Game.World.Config;

namespace WorldsAdriftRebornGameServer.Game.World;

public sealed class SchematicList : CsvLoader<SchematicData>
{
    protected override SchematicData ParseRow( string[] headers, string[] values )
    {
        var s = new SchematicData();

        for (int i = 0; i < headers.Length && i < values.Length; i++)
        {
            var h = headers[i];
            var v = values[i];

            if (string.IsNullOrEmpty(v))
                continue;

            switch (h)
            {
                case "SchematicType":
                    s.SchematicType = Enum.Parse<SchematicType>(v);
                    break;

                case "uUID":
                    s.uUID = v;
                    break;

                case "schematicId":
                    s.schematicId = v;
                    break;

                case "referenceData":
                    s.referenceData = v;
                    break;

                case "category":
                    s.category = v;
                    break;

                case "title":
                    s.title = v;
                    break;

                case "iconId":
                    s.iconId = v;
                    break;

                case "description":
                    s.description = v;
                    break;

                case "timeToCraft":
                    s.timeToCraft = int.Parse(v);
                    break;

                case "amountToCraft":
                    s.amountToCraft = int.Parse(v);
                    break;

                case "itemType":
                    s.itemType = v;
                    break;

                case "craftingRequirements":
                    s.craftingRequirements =
                        JsonSerializer.Deserialize<CraftingItemData[]>(v);
                    break;

                case "baseHp":
                    s.baseHp = float.Parse(v, System.Globalization.CultureInfo.InvariantCulture);
                    break;

                case "baseStats":
                    s.baseStats =
                        JsonSerializer.Deserialize<Dictionary<string, float>>(v);
                    break;

                case "rarity":
                    s.rarity = int.Parse(v);
                    break;

                case "cipherSlots":
                    s.cipherSlots =
                        JsonSerializer.Deserialize<System.Collections.Generic.List<Improbable.Collections.Map<string, string>>>(v);
                    break;

                case "unlearnable":
                    s.unlearnable = bool.Parse(v);
                    break;

                case "modules":
                    s.modules =
                        JsonSerializer.Deserialize<Improbable.Collections.Map<string, string>>(v);
                    break;

                case "hullData":
                    s.hullData = v;
                    break;
            }
        }

        s.baseStats ??= new Dictionary<string, float>();
        s.cipherSlots ??= new System.Collections.Generic.List<Improbable.Collections.Map<string, string>>();
        s.modules ??= new Map<string, string>();

        return s;
    }


    private static readonly string itemPath =
        Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "Game", "World",
            "Config", "schematicData.csv");

    private static Dictionary<string, SchematicData>? schematicDatas = null;
    public static Dictionary<string, SchematicData> SchematicsById
    {
        get
        {
            if (schematicDatas != null) return schematicDatas;
            
            schematicDatas = new Dictionary<string, SchematicData>();
            var contents = Load<SchematicList>(itemPath);

            foreach (var item in contents)
            {
                if (string.IsNullOrWhiteSpace(item.uUID) && string.IsNullOrEmpty(item.schematicId))
                {
                    continue;
                }

                schematicDatas[string.IsNullOrEmpty(item.schematicId) ? item.uUID : item.schematicId] = item;
            }

            Console.WriteLine($"Loaded {schematicDatas.Count}/{contents.Count} schematics");

            return schematicDatas;
        }
    }
}
