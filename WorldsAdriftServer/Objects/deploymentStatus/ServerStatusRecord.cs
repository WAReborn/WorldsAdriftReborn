using Newtonsoft.Json;

namespace WorldsAdriftServer.Objects.DeploymentStatus
{
    internal class ServerStatus
    {
        public static readonly string UP = "up";
        public static readonly string DOWN = "down";
        public static readonly string MAINTENANCE = "maintenance";
    }
    internal class ServerStatusRecord
    {
        [JsonProperty("name")]
        public string DisplayName { get; set; }
        [JsonProperty("server")]
        public string ServerIdentifier { get; set; }
        public string Status { get; set; }
        public string Population { get; set; }

        public ServerStatusRecord(string name, string identifier, string status, string population )
        {
            DisplayName = name;
            ServerIdentifier = identifier;
            Status = status;
            Population = population;
        }
    }
}
