namespace WorldsAdriftServer.Objects.CharacterSelection
{
    internal class CharacterAuthResponse
    {
        public string token { get; set; }
        public string playerId { get; set; }
        public long bossaId { get; set; }
        public string tokenExpiryTime { get; set; }
        public bool success { get; set; }

        public CharacterAuthResponse(string token, string playerId, long bossaId, string tokenExpiryTime, bool success )
        {
            this.token = token;
            this.playerId = playerId;
            this.bossaId = bossaId;
            this.tokenExpiryTime = tokenExpiryTime;
            this.success = success;
        }
    }
}
