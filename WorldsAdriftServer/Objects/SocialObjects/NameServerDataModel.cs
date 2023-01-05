using System;
using Newtonsoft.Json;
using WorldsAdriftServer.Objects.DataObjects;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class NameServerDataModel
    {
        public NameServerDataModel() { }
        internal NameServerDataModel( Data data )
        {
            Guid = data.Guid;
            Name = data.Name;
        }

        [JsonProperty("uid")]
        public string Guid { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}
