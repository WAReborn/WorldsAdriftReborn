using Newtonsoft.Json;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class RankDataModel
    {
        [JsonProperty("target")]
        public string Target { get; set; } = string.Empty;

        [JsonProperty("uid")]
        public string Guid { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("editable")]
        public bool Editable { get; set; } = false;

        [JsonProperty("rankType")]
        public string RankType { get; set; } = string.Empty;

        [JsonProperty("membershipType")]
        public string MembershipType { get; set; } = string.Empty;

        [JsonProperty("permissions")]
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
