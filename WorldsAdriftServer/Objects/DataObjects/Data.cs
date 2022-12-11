namespace WorldsAdriftServer.Objects.DataObjects
{
    [Serializable]
    internal class Data
    {
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
    }
}
