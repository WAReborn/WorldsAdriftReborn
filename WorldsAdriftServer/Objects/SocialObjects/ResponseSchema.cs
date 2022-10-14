using System;
using Newtonsoft.Json.Linq;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class ResponseSchema
    {
        public ResponseSchema( JArray data ) => this.data = JToken.FromObject(new ItemArray(data));
        public ResponseSchema( JToken data ) => this.data = data;

        public bool success { get; set; } = true;
        public string message { get; set; } = string.Empty;
        public string[] messages { get; set; } = new string[0];
        public int statusCode { get; set; } = 200;
        public string errorCode { get; set; } = string.Empty;
        public JToken data { get; set; }
        public JToken originalResponseData { get; set; } = string.Empty;
    }
    [Serializable]
    public class ItemArray
    {
        public ItemArray( JArray items ) => this.items = items;
        public JArray items { get; set; }
    }
}
