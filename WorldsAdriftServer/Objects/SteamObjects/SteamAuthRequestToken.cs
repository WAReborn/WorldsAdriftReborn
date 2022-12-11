using Newtonsoft.Json;

namespace WorldsAdriftServer.Objects.SteamObjects
{
    internal class Credential
    {
        [JsonProperty("platformId")]
        public string PlatformId { get; set; } = string.Empty;

        [JsonProperty("secret")]
        public string Secret { get; set; } = string.Empty;

        [JsonProperty("userKey")]
        public string UserKey { get; set; } = string.Empty;
    }
    internal class SteamAuthRequestToken
    {
        [JsonProperty("appId")]
        public string AppId { get; set; } = string.Empty;

        [JsonProperty("steamCredential")]
        public Credential SteamCredential { get; set; } = new Credential();

        [JsonProperty("bossaCredential")]
        public Credential BossaCredential { get; set; } = new Credential();
    }
}
