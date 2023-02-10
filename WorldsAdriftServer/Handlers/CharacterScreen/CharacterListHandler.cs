using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Helper.Token;
using WorldsAdriftServer.Helper;
using WorldsAdriftServer.Objects.CharacterSelection;
using WorldsAdriftServer.Objects.DataObjects;

namespace WorldsAdriftServer.Handlers.CharacterScreen
{
    internal class CharacterListHandler : Handler
    {
        /*
         * URL: /characterList/{buildNumber}/steam/1234
         * 
         * once the user clicks on the play button the game requests a list of characters.
         * the response also decides whether there is an option to create a new character using the unlockedSlots field
         */
        internal override string Method { get; } = "GET";
        internal override string[] URLs { get; } = { $"/characterList/{Config.GameVersion}/steam/1234" };
        internal override bool CheckSteamToken { get; } = true;
        internal override bool CheckCharacterToken { get; } = false;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            if (!HttpParsers.HeaderByName("Security", httpRequest, out string steamToken) || !GetGuidFromAuthToken.Steam(steamToken, out string playerGuid))
            { return false; }

            PlayerData playerData = DataStore.Instance.PlayerDataDictionary[playerGuid];
            CharacterListResponse characterListResponse = new(playerData);

            return SendData.JObject(JObject.FromObject(characterListResponse), httpSession);
        }
    }
}
