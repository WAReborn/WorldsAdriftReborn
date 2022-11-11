namespace WorldsAdriftServer.Objects.DataObjects
{
    [Serializable]
    internal class NameData : Data
    {
        public NameData() { }
        internal NameData( string name, string guid, string playerGuid )
        {
            Guid = guid;
            Name = name;
            PlayerGuid = playerGuid;
        }
        public string PlayerGuid { get; set; } = string.Empty;
    }
}
