using System;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class AllianceMembershipDataModel
    {
        public string memberId { get; set; }
        public string targetId { get; set; }
        public string rankId { get; set; }
        public long lastUpdated { get; set; }
        public long created { get; set; }
        public NameServerDataModel member { get; set; }
        public string officerNote { get; set; }
        public string privateOfficerNote { get; set; }
    }
}
