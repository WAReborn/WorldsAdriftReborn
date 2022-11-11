using System;
using Newtonsoft.Json;
using WorldsAdriftServer.Objects.DataObjects;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class CrewMembershipDataModel
    {
        public CrewMembershipDataModel() { }
        internal CrewMembershipDataModel( CharacterData characterData )
        {
            MemberGuid = characterData.Guid;
            TargetGuid = characterData.CrewGuid;
            Member.Guid = characterData.Guid;
            Member.Name = characterData.Name;
        }

        [JsonProperty("memberId")]
        public string MemberGuid { get; set; } = string.Empty;

        [JsonProperty("targetId")]
        public string TargetGuid { get; set; } = string.Empty;

        [JsonProperty("lastUpdated")]
        public long LastUpdated { get; set; } = DateTime.Now.Ticks;

        [JsonProperty("created")]
        public long Created { get; set; } = DateTime.Now.Ticks;

        [JsonProperty("member")]
        public NameServerDataModel Member { get; set; } = new NameServerDataModel();
    }
}
