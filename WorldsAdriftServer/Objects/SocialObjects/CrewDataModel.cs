using System;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class CrewDataModel
    {
        public string uid { get; set; } = string.Empty;
        public string region { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string leaderCharacterUid { get; set; } = string.Empty;
        public NameServerDataModel leaderCharacter { get; set; } = new NameServerDataModel();
        public long created { get; set; } = 0;
        public long lastUpdated { get; set; } = 0;
    }
}
