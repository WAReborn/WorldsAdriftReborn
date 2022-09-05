namespace WorldsAdriftServer.Objects.SteamObjects
{
    internal class SteamCredential
    {
        public string platformId { get; set; }
        public string secret { get; set; }
        public string userKey { get; set; }
    }
    internal class SteamAuthRequestToken
    {
        public string appId { get; set; }
        public SteamCredential steamCredential { get; set; }
    }
}
