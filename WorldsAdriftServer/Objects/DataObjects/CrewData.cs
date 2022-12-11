namespace WorldsAdriftServer.Objects.DataObjects
{
    [Serializable]
    internal class CrewData : Data
    {
        public CrewData() { }
        internal CrewData( string name, string description, string region, CharacterData Leader )
        {
            Name = name;
            Region = region;
            Description = description;
            LeaderGuid = Leader.Guid;
            MemberGuids.Add(Leader.Guid);
            Created = DateTime.Now.Ticks;
            LastUpdated = Created;
        }
        public string LeaderGuid { get; set; } = string.Empty;
        public List<string> MemberGuids { get; set; } = new List<string>();
        public string Region { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long Created { get; set; }
        public long LastUpdated { get; set; }
    }
}
