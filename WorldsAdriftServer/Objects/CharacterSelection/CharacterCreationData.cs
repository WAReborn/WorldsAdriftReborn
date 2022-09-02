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

    internal struct ColorProperties
    {
        public ColorProperties( UnityColor primary, UnityColor secondary )
        {
            PrimaryColor = primary;
            SecondaryColor = secondary;
            TertiaryColor = new UnityColor();
            SpecColor = new UnityColor(1f, 1f, 1f, 1f);
        }

        [JsonConverter(typeof(UnityObjects.ColorConverter))]
        public UnityColor PrimaryColor;

        [JsonConverter(typeof(UnityObjects.ColorConverter))]
        public UnityColor SecondaryColor;

        [JsonConverter(typeof(UnityObjects.ColorConverter))]
        public UnityColor TertiaryColor;

        [JsonConverter(typeof(UnityObjects.ColorConverter))]
        public UnityColor SpecColor;
    }

    internal struct CharacterUniversalColors
    {
        [JsonConverter(typeof(UnityObjects.ColorConverter))]
        public UnityColor HairColor;

        [JsonConverter(typeof(UnityObjects.ColorConverter))]
        public UnityColor SkinColor;

        [JsonConverter(typeof(UnityObjects.ColorConverter))]
        public UnityColor LipColor;
    }

    internal class ItemData
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
