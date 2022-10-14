using System;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class CrewMembershipDataModel
    {
        public string memberId { get; set; } = string.Empty;
        public string targetId { get; set; } = string.Empty;
        public long lastUpdated { get; set; } = 0;
        public long created { get; set; } = 0;
        public NameServerDataModel member { get; set; } = new NameServerDataModel();
    }
}
