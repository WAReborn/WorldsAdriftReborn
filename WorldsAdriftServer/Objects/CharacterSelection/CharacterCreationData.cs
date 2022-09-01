using System.Drawing;
using System.Globalization;
using Newtonsoft.Json;

namespace WorldsAdriftServer.Objects.CharacterSelection
{
    public class ColorConverter : JsonConverter
    {
        public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer )
        {
            UnityColor color = (UnityColor)value;
            writer.WriteStartObject();
            writer.WritePropertyName("r");
            serializer.Serialize(writer, color.r);
            writer.WritePropertyName("g");
            serializer.Serialize(writer, color.g);
            writer.WritePropertyName("b");
            serializer.Serialize(writer, color.b);
            writer.WritePropertyName("a");
            serializer.Serialize(writer, color.a);
            writer.WriteEndObject();
        }

        public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer )
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override bool CanConvert( Type objectType )
        {
            return objectType == typeof(UnityColor);
        }
    }

    public enum CharacterSlotType
    {
        None,
        Head,
        Body,
        Feet,
        UtilityHead,
        Utility,
        UtilityFeet,
        Face,
        FacialHair,
        Tool,
        UtilityHand,
        Pet
    }

    public struct UnityColor
    {
        public float a { get; set; }
        public float b { get; set; }
        public float g { get; set; }
        public float r { get; set; }

        public UnityColor(float r, float g, float b, float a )
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
        public static int HexToInt(char hexValue )
        {
            return int.Parse(hexValue.ToString(), NumberStyles.HexNumber);
        }
        public static UnityColor FromHex(string color )
        {
            if(color.IndexOf("#") == 0)
            {
                color = color.Substring(1);
            }
            float r = ((float)HexToInt(color[1]) + (float)HexToInt(color[0]) * 16f) / 255f;
            float g = ((float)HexToInt(color[3]) + (float)HexToInt(color[2]) * 16f) / 255f;
            float b = ((float)HexToInt(color[5]) + (float)HexToInt(color[4]) * 16f) / 255f;
            float a = (color.Length <= 6) ? 1f : (((float)HexToInt(color[7]) + (float)HexToInt(color[6]) * 16f) / 255f);
            return new UnityColor
            {
                r = r,
                g = g,
                b = b,
                a = a
            };
        }
    }

    public struct ColorProperties
    {
        public ColorProperties( UnityColor primary, UnityColor secondary )
        {
            PrimaryColor = primary;
            SecondaryColor = secondary;
            TertiaryColor = new UnityColor();
            SpecColor = new UnityColor(1f, 1f, 1f, 1f); // white
        }

        [JsonConverter(typeof(ColorConverter))]
        public UnityColor PrimaryColor;

        [JsonConverter(typeof(ColorConverter))]
        public UnityColor SecondaryColor;

        [JsonConverter(typeof(ColorConverter))]
        public UnityColor TertiaryColor;

        [JsonConverter(typeof(ColorConverter))]
        public UnityColor SpecColor;
    }

    public struct CharacterUniversalColors
    {
        [JsonConverter(typeof(ColorConverter))]
        public UnityColor HairColor;

        [JsonConverter(typeof(ColorConverter))]
        public UnityColor SkinColor;

        [JsonConverter(typeof(ColorConverter))]
        public UnityColor LipColor;
    }

    public class ItemData
    {
        public override string ToString()
        {
            return string.Format("{0}:{1}", Id, Prefab);
        }

        public string Id;

        public string Prefab;

        public ColorProperties ColorProps;

        public float Health;

        public ItemData(string id, string prefab, ColorProperties colorProps, float health )
        {
            Id = id;
            Prefab = prefab;
            ColorProps = colorProps;
            Health = health;
        }
    }

    internal class CharacterCreationData
    {
        public int Id { get; set; }
        public string characterUid { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string serverIdentifier { get; set; }
        public Dictionary<CharacterSlotType, ItemData> Cosmetics { get; set; }
        public CharacterUniversalColors UniversalColors { get; set; }
        public bool isMale { get; set; }
        public bool seenIntro { get; set; }
        public bool skippedTutorial { get; set; }

        public CharacterCreationData(int id, string characterUid, string name, string server, string serverIdentifier, Dictionary<CharacterSlotType, ItemData> cosmetics, CharacterUniversalColors universalColors, bool isMale, bool seenIntro, bool skippedTutorial )
        {
            Id = id;
            this.characterUid = characterUid;
            Name = name;
            Server = server;
            this.serverIdentifier = serverIdentifier;
            Cosmetics = cosmetics;
            UniversalColors = universalColors;
            this.isMale = isMale;
            this.seenIntro = seenIntro;
            this.skippedTutorial = skippedTutorial;
        }
    }
}
