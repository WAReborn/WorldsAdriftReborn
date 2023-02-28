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

        [JsonConverter(typeof(UnityObjects.ColorConverter))]
        public UnityColor PrimaryColor;

        [JsonConverter(typeof(UnityObjects.ColorConverter))]
        public UnityColor SecondaryColor;

        [JsonConverter(typeof(UnityObjects.ColorConverter))]
        public UnityColor TertiaryColor;

        [JsonConverter(typeof(UnityObjects.ColorConverter))]
        public UnityColor SpecColor;
    }

    public struct CharacterUniversalColors
    {
        [JsonConverter(typeof(UnityObjects.ColorConverter))]
        public UnityColor HairColor;

        [JsonConverter(typeof(UnityObjects.ColorConverter))]
        public UnityColor SkinColor;

        [JsonConverter(typeof(UnityObjects.ColorConverter))]
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


    /// <summary>
    /// Data object used by the game
    /// </summary>
    public class CharacterCreationData
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

        public CharacterCreationData(int id, string characterId, string name, string server, string serverIdentifier, Dictionary<CharacterSlotType, ItemData> cosmetics, CharacterUniversalColors universalColors, bool isMale, bool seenIntro, bool skippedTutorial )
        {
            Id = id;
            characterUid = characterId;
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


    /// <summary>
    /// Character Data for storing inside a database.
    /// </summary>
    public class CharacterDataDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string serverIdentifier { get; set; }
        public string Cosmetics { get; set; }
        public string UniversalColors { get; set; }
        public bool IsMale { get; set; }
        public bool SeenIntro { get; set; }
        public bool SkippedTutorial { get; set; }

        public CharacterDataDTO( Guid id, string name, string server, string serverIdentifier, string cosmetics, string universalColors, bool isMale, bool seenIntro, bool skippedTutorial )
        {
            Id = id;
            Name = name;
            Server = server;
            this.serverIdentifier = serverIdentifier;
            Cosmetics = cosmetics;
            UniversalColors = universalColors;
            IsMale = isMale;
            SeenIntro = seenIntro;
            SkippedTutorial = skippedTutorial;
        }
    }
}
