using Newtonsoft.Json;
using WorldsAdriftServer.Objects.DataObjects;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class PlayerMembershipModel
    {
        public PlayerMembershipModel() { }
        internal PlayerMembershipModel( CharacterData characterData )
        {
            Character = characterData.Name;
            Member = new NameServerDataModel(characterData);
            if (!string.IsNullOrEmpty(characterData.AllianceGuid))
            {
                Alliance = new AllianceMembershipDataModel(characterData);
            }
            if (!string.IsNullOrEmpty(characterData.CrewGuid))
            {
                Crew = new CrewMembershipDataModel(characterData);
            }
        }

        [JsonProperty("character")]
        public string Character { get; set; } = string.Empty;

        [JsonProperty("member")]
        public NameServerDataModel Member { get; set; } = new NameServerDataModel();

        [JsonProperty("alliance")]
        public AllianceMembershipDataModel? Alliance { get; set; }

        [JsonProperty("crew")]
        public CrewMembershipDataModel? Crew { get; set; }
    }
}
