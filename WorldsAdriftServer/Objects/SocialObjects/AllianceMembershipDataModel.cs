using System;
using Newtonsoft.Json;
using WorldsAdriftServer.Objects.DataObjects;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class AllianceMembershipDataModel
    {
        public AllianceMembershipDataModel() { }
        internal AllianceMembershipDataModel( CharacterData characterData )
        {
            MemberGuid = characterData.Guid;
            TargetGuid = characterData.AllianceGuid;
            RankGuid = characterData.AllianceRankGuid;
            Member.Guid = characterData.Guid;
            Member.Name = characterData.Name;
            OfficerNote = characterData.AlliancePublicNotes;
            PrivateOfficerNote = characterData.AlliancePrivateNotes;
        }


        [JsonProperty("memberId")]
        public string MemberGuid { get; set; } = string.Empty;

        [JsonProperty("targetId")]
        public string TargetGuid { get; set; } = string.Empty;

        [JsonProperty("rankId")]
        public string RankGuid { get; set; } = string.Empty;

        [JsonProperty("lastUpdated")]
        public long LastUpdated { get; set; } = DateTime.Now.Ticks;

        [JsonProperty("created")]
        public long Created { get; set; } = DateTime.Now.Ticks;

        [JsonProperty("member")]
        public NameServerDataModel Member { get; set; } = new NameServerDataModel();

        [JsonProperty("officerNote")]
        public string OfficerNote { get; set; } = string.Empty;

        [JsonProperty("privateOfficerNote")]
        public string PrivateOfficerNote { get; set; } = string.Empty;
    }
}
