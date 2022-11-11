namespace WorldsAdriftServer.Objects.DataObjects
{
    [Serializable]
    internal class InviteData : Data
    {
        public InviteData() { }
        internal InviteData( string inviterGuid, string invitedGuid, string targetGuid, string targetName, string targetType, string status, string message )
        {
            InviterGuid = inviterGuid;
            InvitedGuid = invitedGuid;
            TargetGuid = targetGuid;
            TargetName = targetName;
            TargetType = targetType;
            Status = status;
            Message = message;
            Created = DateTime.Now.Ticks;
            LastUpdated = Created;
        }
        public string InviterGuid { get; set; } = string.Empty;
        public string InvitedGuid { get; set; } = string.Empty;
        public string TargetGuid { get; set; } = string.Empty;
        public string TargetName { get; set; } = string.Empty;
        public string TargetType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public long Created { get; set; }
        public long LastUpdated { get; set; }
    }
}
