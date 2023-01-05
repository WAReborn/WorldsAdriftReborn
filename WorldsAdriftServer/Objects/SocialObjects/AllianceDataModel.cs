using System;
using Newtonsoft.Json;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.DataObjects;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class AllianceDataModel
    {
        public AllianceDataModel() { }
        internal AllianceDataModel( AllianceData allianceData )
        {
            Guid = allianceData.Guid;
            Region = allianceData.Region;
            Name = allianceData.Name;
            Description = allianceData.Description;
            MessageOfTheDay = allianceData.MessageOfTheDay;
            LeaderCharacterGuid = allianceData.LeaderGuid;
            LeaderCharacter.Guid = allianceData.LeaderGuid;
            LeaderCharacter.Name = DataStore.Instance.PlayerDataDictionary[allianceData.LeaderGuid].Name;
            Created = allianceData.Created;
            LastUpdated = allianceData.LastUpdated;
            MemberCount = allianceData.MemberGuids.Count;
        }

        [JsonProperty("uid")]
        public string Guid { get; set; } = string.Empty;

        [JsonProperty("region")]
        public string Region { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("messageOfTheDay")]
        public string MessageOfTheDay { get; set; } = string.Empty;

        [JsonProperty("leaderCharacterUid")]
        public string LeaderCharacterGuid { get; set; } = string.Empty;

        [JsonProperty("leaderCharacter")]
        public NameServerDataModel LeaderCharacter { get; set; } = new NameServerDataModel();

        [JsonProperty("created")]
        public long Created { get; set; } = DateTime.Now.Ticks;

        [JsonProperty("lastUpdated")]
        public long LastUpdated { get; set; } = DateTime.Now.Ticks;

        [JsonProperty("emblemUrl")]
        public string EmblemUrl { get; set; } = string.Empty;

        [JsonProperty("memberCount")]
        public int MemberCount { get; set; } = 0;
    }
}
