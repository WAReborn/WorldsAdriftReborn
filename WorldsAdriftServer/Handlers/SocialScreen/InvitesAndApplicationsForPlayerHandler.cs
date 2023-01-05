using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.SocialObjects;

namespace WorldsAdriftServer.Handlers.SocialScreen
{
    internal class InvitesAndApplicationsForPlayerHandler : Handler
    {
        internal override string Method { get; } = "GET";
        internal override string[] URLs { get; } = { "/memberships/invites/character/" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = true;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            //TODO return real data
            JObject response = JObject.FromObject(new ResponseSchema(JArray.FromObject(new List<MembershipChangeRequestDataModel>())));
            return SendData.JObject(response, httpSession );
        }
    }
}
