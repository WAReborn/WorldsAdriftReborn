using Newtonsoft.Json;

namespace WorldsAdriftServer.Objects.SteamObjects
{
    internal class SteamAuthResponseToken
    {
        public SteamAuthResponseToken() { }
        internal SteamAuthResponseToken( string token, bool success )
        {
            Token = token;
            Success = success;
        }

        [JsonProperty("token")]
        public string Token { get; set; } = string.Empty;

        [JsonProperty("playerId")]
        public string PlayerId { get; set; } = Config.PlayerId;

        [JsonProperty("bossaId")]
        public string BossaId { get; set; } = Config.BossaId;

        [JsonProperty("screenName")]
        public string ScreenName { get; set; } = string.Empty;

        [JsonProperty("desc")]
        public string Desc { get; set; } = Config.SteamAuthError;

        [JsonProperty("success")]
        public bool Success { get; set; } = false;

    }
}
