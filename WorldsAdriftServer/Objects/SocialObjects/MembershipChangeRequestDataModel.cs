using Newtonsoft.Json;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.DataObjects;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class MembershipChangeRequestDataModel
    {
        public MembershipChangeRequestDataModel() { }
        internal MembershipChangeRequestDataModel( InviteData inviteData )
        {
            Guid = inviteData.Guid;
            TargetGuid = inviteData.TargetGuid;
            TargetName = inviteData.TargetName;
            TargetType = inviteData.TargetType;
            Character.Guid = inviteData.InvitedGuid;
            Character.Name = DataStore.Instance.PlayerDataDictionary[inviteData.InvitedGuid].Name;
            Inviter.Guid = inviteData.InviterGuid;
            Inviter.Name = DataStore.Instance.PlayerDataDictionary[inviteData.InviterGuid].Name;
            Message = inviteData.Message;
            Status = inviteData.Status;
        }

        [JsonProperty("id")]
        public string Guid { get; set; } = string.Empty;

        [JsonProperty("targetId")]
        public string TargetGuid { get; set; } = string.Empty;

        [JsonProperty("targetName")]
        public string TargetName { get; set; } = string.Empty;

        [JsonProperty("character")]
        public NameServerDataModel Character { get; set; } = new NameServerDataModel();

        [JsonProperty("targetType")]
        public string TargetType { get; set; } = string.Empty;

        [JsonProperty("inviter")]
        public NameServerDataModel Inviter { get; set; } = new NameServerDataModel();

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;

        [JsonProperty("created")]
        public long created { get; set; } = DateTime.Now.Ticks;

        [JsonProperty("lastUpdated")]
        public long lastUpdated { get; set; } = DateTime.Now.Ticks;
    }
}
