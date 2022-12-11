using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.SocialObjects;

namespace WorldsAdriftServer.Handlers.SocialScreen.Alliance
{
    internal class ListAlliancesHandler : Handler
    {
        internal override string Method { get; } = "GET";
        internal override string[] URLs { get; } = { $"/alliances/" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = true;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            //TODO return real data
            JObject responce = JObject.FromObject(new ResponseSchema(JArray.FromObject(new List<AllianceDataModel>())));
            return SendData.JObject(responce, httpSession);
        }
    }
}
