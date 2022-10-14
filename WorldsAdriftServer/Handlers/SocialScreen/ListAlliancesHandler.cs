using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.SocialObjects;

namespace WorldsAdriftServer.Handlers.SocialScreen
{
    internal class ListAlliancesHandler
    {
        internal static bool HandleListAlliances( HttpSession session, HttpRequest request )
        {
            return SendData.SendJObject((JObject)JToken.FromObject(new ResponseSchema(JArray.FromObject(new List<AllianceDataModel>()))), session);
        }
    }
}
