using System;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class CrewMembershipDataModel
    {
        public string memberId { get; set; }
        public string targetId { get; set; }
        public long lastUpdated { get; set; }
        public long created { get; set; }
        public NameServerDataModel member { get; set; }
    }
}
