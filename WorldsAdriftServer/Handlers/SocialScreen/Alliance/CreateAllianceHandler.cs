using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helpers;
using WorldsAdriftServer.Objects.SocialObjects;

namespace WorldsAdriftServer.Handlers.SocialScreen.Alliance
{
    internal class CreateAllianceHandler : Handler
    {
        internal override string Method { get; } = "POST";
        internal override string[] URLs { get; } = { "/alliance" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = true;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            //TODO return real data
            JObject responce = JObject.FromObject(new ResponseSchema(JToken.FromObject(new AllianceDataModel())));
            return SendData.JObject(responce, httpSession);
        }
    }
}
