using NetCoreServer;
using WorldsAdriftServer.Helper.Token;
using WorldsAdriftServer.Helper;
using WorldsAdriftServer.Objects.DataObjects;
using WorldsAdriftServer.Objects.CharacterSelection;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;

namespace WorldsAdriftServer.Handlers.CharacterScreen
{
    internal class CharacterDeleteHandler : Handler
    {
        internal override string Method { get; } = "DELETE";
        internal override string[] URLs { get; } = { $"/character/{Config.GameVersion}/steam/1234/" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = true;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            if (!HttpParsers.HeaderByName("Security", httpRequest, out string chracterToken) || !GetGuidFromAuthToken.Character(chracterToken, out string characterGuid))
            { return false; }

            CharacterData characterData = DataStore.Instance.CharacterDataDictionary[characterGuid];
            NameData nameData = DataStore.Instance.PlayerCharacterNameData[characterData.Name];
            PlayerData playerData = DataStore.Instance.PlayerDataDictionary[nameData.PlayerGuid];

            if (characterData.Name != playerData.Name)
            { 
                DataStore.Instance.PlayerCharacterNameData.Remove(nameData.Name); 
            }
            else
            { 
                nameData.Guid = string.Empty;
            }

            playerData.CharacterGUIDs.Remove(characterGuid);
            DataStore.Instance.CharacterDataDictionary.Remove(characterGuid);

            CharacterListResponse characterListResponse = new(playerData);

            return SendData.JObject(JObject.FromObject(characterListResponse), httpSession);
        }
    }
}
