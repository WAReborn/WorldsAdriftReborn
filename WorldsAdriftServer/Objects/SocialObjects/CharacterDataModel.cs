using Newtonsoft.Json;
using WorldsAdriftServer.Helpers;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class CharacterDataModel : ResponseSchema
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("displayName")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonProperty("bossaId")]
        public long BossaId { get; set; } = 999;

        [JsonProperty("characterSlot")]
        public int CharacterSlot { get; set; } = 0;

        [JsonProperty("lastUpdated")]
        public long LastUpdated { get; set; } = DateTime.Now.Ticks;

        [JsonProperty("characterUid")]
        public string CharacterUid { get; set; } = string.Empty;

        [JsonProperty("validated")]
        public bool Validated { get; set; } = true;
    }
}
