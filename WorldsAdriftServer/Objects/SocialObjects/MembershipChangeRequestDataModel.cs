using System;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class MembershipChangeRequestDataModel
    {
        public string id { get; set; } = string.Empty;
        public string targetId { get; set; } = string.Empty;
        public string targetName { get; set; } = string.Empty;
        public NameServerDataModel character { get; set; } = new NameServerDataModel();
        public string targetType { get; set; } = string.Empty;
        public NameServerDataModel inviter { get; set; } = new NameServerDataModel();
        public string message { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public long created { get; set; } = 0;
        public long lastUpdated { get; set; } = 0;
    }
}
