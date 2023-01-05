using Newtonsoft.Json;

namespace WorldsAdriftServer.Objects.CharacterSelection
{
    internal class CharacterAuthResponse
    {
        public CharacterAuthResponse() { }
        internal CharacterAuthResponse( string token, bool success )
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

        [JsonProperty("tokenExpiryTime")]
        public string TokenExpiryTime { get; set; } = "12.12.12";

        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [JsonProperty("messages")]
        public string[] Messages { get; set; } = { };

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }
}
