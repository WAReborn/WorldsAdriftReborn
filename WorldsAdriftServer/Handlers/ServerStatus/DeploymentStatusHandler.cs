using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.DeploymentStatus;

namespace WorldsAdriftServer.Handlers.ServerStatus
{
    internal static class DeploymentStatusHandler
    {
        /*
         * URL: /deploymentStatus
         * 
         * the game requests this endpoint after requesting the character list and then continues to do so in a specific interval.
         * the response contains a list of available servers along with their status.
         */
        internal static bool HandleDeploymentStatusRequest(HttpSession session, HttpRequest request, string serverName, string serverIdentifier, int population )
        {
            Dictionary<string, ServerStatusRecord> serverStatus = new Dictionary<string, ServerStatusRecord>();
            serverStatus.Add(serverIdentifier, new ServerStatusRecord(serverName, serverIdentifier, Objects.DeploymentStatus.ServerStatus.UP, population.ToString()));
            return SendData.SendJObject((JObject)JToken.FromObject(serverStatus), session);
        }
    }
}
