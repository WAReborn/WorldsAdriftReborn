using Newtonsoft.Json;
using WorldsAdriftServer.Objects.UnityObjects;

namespace WorldsAdriftServer.Objects.CharacterSelection
{
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

    public struct ColorProperties
    {
        public ColorProperties( UnityColor primary, UnityColor secondary )
        {
            PrimaryColor = primary;
            SecondaryColor = secondary;
            TertiaryColor = new UnityColor();
            SpecColor = new UnityColor(1f, 1f, 1f, 1f);
        }

        [JsonConverter(typeof(ColorConverter))]
        public UnityColor PrimaryColor { get; set; }

        [JsonConverter(typeof(ColorConverter))]
        public UnityColor SecondaryColor { get; set; }

        [JsonConverter(typeof(ColorConverter))]
        public UnityColor TertiaryColor { get; set; }

        [JsonConverter(typeof(ColorConverter))]
        public UnityColor SpecColor { get; set; }
    }

    public struct CharacterUniversalColors
    {
        [JsonConverter(typeof(ColorConverter))]
        public UnityColor HairColor { get; set; }

        [JsonConverter(typeof(ColorConverter))]
        public UnityColor SkinColor { get; set; }

        [JsonConverter(typeof(ColorConverter))]
        public UnityColor LipColor { get; set; }
    }

    public class ItemData
    {
        public override string ToString()
        {
            return string.Format("{0}:{1}", Id, Prefab);
        }

        public string Id { get; set; }

        public string Prefab { get; set; }

        public ColorProperties ColorProps { get; set; }

        public float Health { get; set; }

        public ItemData( string id, string prefab, ColorProperties colorProps, float health )
        {
            Id = id;
            Prefab = prefab;
            ColorProps = colorProps;
            Health = health;
        }
    }

    public class CharacterCreationData
    {
        public int Id { get; set; }
        public string characterUid { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string serverIdentifier { get; set; }
        public Dictionary<CharacterSlotType, ItemData>? Cosmetics { get; set; }
        public CharacterUniversalColors UniversalColors { get; set; }
        public bool isMale { get; set; }
        public bool seenIntro { get; set; }
        public bool skippedTutorial { get; set; }

        public CharacterCreationData( int id, string characterUid, string name, string server, string serverIdentifier, Dictionary<CharacterSlotType, ItemData>? cosmetics, CharacterUniversalColors universalColors, bool isMale, bool seenIntro, bool skippedTutorial )
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
