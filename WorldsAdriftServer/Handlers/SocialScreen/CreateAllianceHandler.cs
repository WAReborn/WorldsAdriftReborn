using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.SocialObjects;

namespace WorldsAdriftServer.Handlers.SocialScreen
{
    internal class CreateAllianceHandler
    {
        internal static bool HandleCreateAlliance( HttpSession session, HttpRequest request )
        {
            return SendData.SendJObject((JObject)JToken.FromObject(new ResponseSchema(JToken.FromObject(new AllianceDataModel()))), session);
        }
    }
}
