using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.SocialObjects;

namespace WorldsAdriftServer.Handlers.SocialScreen.Crew
{
    internal class CreateCrewHandler : Handler
    {
        internal override string Method { get; } = "POST";
        internal override string[] URLs { get; } = { "/crews/" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = true;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            //TODO return real data
            JObject response = JObject.FromObject(new ResponseSchema(JToken.FromObject(new CrewDataModel())));
            return SendData.JObject(response, httpSession);
        }
    }
}
