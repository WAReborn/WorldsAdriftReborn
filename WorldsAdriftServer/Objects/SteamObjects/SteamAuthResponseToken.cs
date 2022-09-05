namespace WorldsAdriftServer.Objects.SteamObjects
{
    internal class SteamAuthResponseToken
    {
        public string token { get; set; }
        public string playerId { get; set; }
        public string bossaId { get; set; }
        public string screenName { get; set; }
        public string desc { get; set; }
        public bool success { get; set; }

        public SteamAuthResponseToken( string token,
                                        string playerId,
                                        string bossaId,
                                        bool success )
        {
            this.token = token;
            this.playerId = playerId;
            this.bossaId = bossaId;
            this.success = success;
            screenName = string.Empty;
            desc = string.Empty;
        }
    }
}
