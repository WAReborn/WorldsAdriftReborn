using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.SocialObjects;

namespace WorldsAdriftServer.Handlers.SocialScreen
{
    internal class InvitesAndApplicationsForPlayerHandler
    {
        internal static bool HandleInvitesAndApplicationsForPlayer( HttpSession session, HttpRequest request )
        {
            return SendData.SendJObject((JObject)JToken.FromObject(new ResponseSchema(JArray.FromObject(new List<MembershipChangeRequestDataModel>()))), session);
        }
    }
}
