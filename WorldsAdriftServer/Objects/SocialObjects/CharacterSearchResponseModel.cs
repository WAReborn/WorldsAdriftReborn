using Newtonsoft.Json;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class CharacterSearchResponseModel : ResponseSchema
    {
        [JsonProperty("screenname")]
        public CharacterDataModel ScreenName { get; set; } = new CharacterDataModel();

        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;

        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;

        [JsonProperty("desc")]
        public string Desc { get; set; } = string.Empty;
    }
}
