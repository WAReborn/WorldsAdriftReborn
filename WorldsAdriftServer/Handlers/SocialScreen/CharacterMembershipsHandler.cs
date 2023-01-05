using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.DataObjects;
using WorldsAdriftServer.Objects.SocialObjects;

namespace WorldsAdriftServer.Handlers.SocialScreen
{
    internal class CharacterMembershipsHandler : Handler
    {
        internal override string Method { get; } = "GET";
        internal override string[] URLs { get; } = { "/memberships/character/" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = true;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            if (!HttpParsers.GUIDsFromURL(httpRequest.Url, out List<string> guids))
            { return false; }

            CharacterData characterData = DataStore.Instance.CharacterDataDictionary[guids[0]];
            PlayerMembershipModel playerMembershipModel = new(characterData);

            JObject response = JObject.FromObject(new ResponseSchema(JObject.FromObject(playerMembershipModel)));
            return SendData.JObject(response, httpSession);
        }
    }
}
