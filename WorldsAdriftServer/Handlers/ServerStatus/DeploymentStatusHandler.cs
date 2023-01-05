using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;

namespace WorldsAdriftServer.Handlers.ServerStatus
{
    internal class DeploymentStatusHandler : Handler
    {
        /*
         * URL: /deploymentStatus
         * 
         * the game requests this endpoint after requesting the character list and then continues to do so in a specific interval.
         * the response contains a list of available servers along with their status.
         */
        internal override string Method { get; } = "GET";
        internal override string[] URLs { get; } = { "/status" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = false;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            return SendData.JObject((JObject)JToken.FromObject(Config.ServerStatusDictionary), httpSession);
        }
    }
}
