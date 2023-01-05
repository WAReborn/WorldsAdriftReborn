using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class ResponseSchema
    {
        public ResponseSchema() { }
        internal ResponseSchema( JArray data ) => Data = JObject.FromObject(new ItemArray(data));
        internal ResponseSchema( JToken data ) => Data = data;

        [JsonProperty("success")]
        public bool Success { get; set; } = true;

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("messages")]
        public string[] Messages { get; set; } = new string[0];

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; } = 200;

        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; } = string.Empty;

        [JsonProperty("data")]
        public JToken Data { get; set; } = new JObject();

        [JsonProperty("originalResponseData")]
        public JToken OriginalResponseData { get; set; } = string.Empty;
    }
    [Serializable]
    public class ItemArray
    {
        public ItemArray() { }
        internal ItemArray( JArray items ) => Items = items;

        [JsonProperty("items")]
        public JArray Items { get; set; } = new JArray();
    }
}
