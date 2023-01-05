using Newtonsoft.Json;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.DataObjects;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class CrewDataModel
    {
        public CrewDataModel() { }
        internal CrewDataModel( CrewData crewData )
        {
            Guid = crewData.Guid;
            Region = crewData.Region;
            Name = crewData.Name;
            Description = crewData.Description;
            LeaderCharacterUid = crewData.LeaderGuid;
            LeaderCharacter.Guid = crewData.LeaderGuid;
            LeaderCharacter.Name = DataStore.Instance.PlayerDataDictionary[crewData.LeaderGuid].Name;
        }

        [JsonProperty("uid")]
        public string Guid { get; set; } = string.Empty;

        [JsonProperty("region")]
        public string Region { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("leaderCharacterUid")]
        public string LeaderCharacterUid { get; set; } = string.Empty;

        [JsonProperty("leaderCharacter")]
        public NameServerDataModel LeaderCharacter { get; set; } = new NameServerDataModel();

        [JsonProperty("created")]
        public long Created { get; set; } = DateTime.Now.Ticks;

        [JsonProperty("lastUpdated")]
        public long LastUpdated { get; set; } = DateTime.Now.Ticks;
    }
}
