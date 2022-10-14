using System;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class PlayerMembershipModel
    {
        public string character { get; set; }
        public NameServerDataModel member { get; set; }
        public AllianceMembershipDataModel alliance { get; set; }
        public CrewMembershipDataModel crew { get; set; }
    }
}
