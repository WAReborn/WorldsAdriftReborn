using System;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class AllianceMembershipDataModel
    {
        public string memberId { get; set; } = string.Empty;
        public string targetId { get; set; } = string.Empty;
        public string rankId { get; set; } = string.Empty;
        public long lastUpdated { get; set; } = 0;
        public long created { get; set; } = 0;
        public NameServerDataModel member { get; set; } = new NameServerDataModel();
        public string officerNote { get; set; } = string.Empty;
        public string privateOfficerNote { get; set; } = string.Empty;
    }
}
