using System;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class PlayerMembershipModel
    {
        public string character { get; set; } = string.Empty;
        public NameServerDataModel member { get; set; } = new NameServerDataModel();
        public AllianceMembershipDataModel alliance { get; set; } = new AllianceMembershipDataModel();
        public CrewMembershipDataModel crew { get; set; } = new CrewMembershipDataModel(); 
    }
}
