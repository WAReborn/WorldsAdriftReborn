using System;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class AllianceDataModel
    {
        public string uid { get; set; }
        public string region { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string messageOfTheDay { get; set; }
        public string leaderCharacterUid { get; set; }
        public NameServerDataModel leaderCharacter { get; set; }
        public long created { get; set; }
        public long lastUpdated { get; set; }
        public string emblemUrl { get; set; }
        public int memberCount { get; set; }
    }
}
