using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;

namespace WorldsAdriftServer.Handlers.ServerStatus
{
    internal class VersionCheckHandler : Handler
    {
        internal override string Method { get; } = "GET";
        internal override string[] URLs { get; } = { $"/{Config.GameVersion}" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = false;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            return SendData.JObject(JObject.Parse("{ versionCheck: true }"), httpSession);
        }
    }
}
