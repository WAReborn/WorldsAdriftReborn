using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.SocialObjects;

namespace WorldsAdriftServer.Handlers.SocialScreen
{
    internal class CreateCrewHandler
    {
        internal static bool HandleCreateCrew( HttpSession session, HttpRequest request )
        {
            return SendData.SendJObject((JObject)JToken.FromObject(new ResponseSchema(JToken.FromObject(new CrewDataModel()))), session);
        }
    }
}
