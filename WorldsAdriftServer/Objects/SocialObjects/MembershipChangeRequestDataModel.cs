using System;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class MembershipChangeRequestDataModel
    {
        public string id { get; set; }
        public string targetId { get; set; }
        public string targetName { get; set; }
        public NameServerDataModel character { get; set; }
        public string targetType { get; set; }
        public NameServerDataModel inviter { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public long created { get; set; }
        public long lastUpdated { get; set; }
    }
}
