namespace WorldsAdriftServer.Objects
{
    internal class SteamAuthResponseToken
    {
        public string token { get; set; }
        public string playerId { get; set; }
        public string bossaId { get; set; }
        public string screenName { get; set; }
        public string desc { get; set; }

        public SteamAuthResponseToken(  string token,
                                        string playerId,
                                        string bossaId )
        {
            this.token = token;
            this.playerId = playerId;
            this.bossaId = bossaId;
            screenName = string.Empty;
            desc = string.Empty;
        }
    }
}
