using Newtonsoft.Json;

namespace WorldsAdriftServer.Objects.deploymentStatus
{
    internal class ServerStatus
    {
        public static readonly string up = "up";
        public static readonly string down = "down";
        public static readonly string maintenance = "maintenance";
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
