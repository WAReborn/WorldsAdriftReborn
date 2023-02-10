namespace WorldsAdriftServer.Objects.DataObjects
{
    [Serializable]
    internal class AllianceData : CrewData
    {
        public AllianceData() { }
        internal AllianceData( string name, string description, string region, CharacterData Leader, string messageOfTheDay )
        {
            Name = name;
            Description = description;
            Region = region;
            LeaderGuid = Leader.Guid;
            MemberGuids.Add(Leader.Guid);
            Created = DateTime.Now.Ticks;
            LastUpdated = Created;
            MessageOfTheDay = messageOfTheDay;
        }
        public string MessageOfTheDay { get; set; } = string.Empty;
        public string EmblemURL { get; set; } = string.Empty;
    }
}
