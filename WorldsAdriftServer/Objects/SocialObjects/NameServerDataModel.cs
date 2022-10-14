using System;

namespace WorldsAdriftServer.Objects.SocialObjects
{
    [Serializable]
    public class NameServerDataModel
    {
        public string uid { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
    }
}
