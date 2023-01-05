using NetCoreServer;
using WorldsAdriftServer.Helper.Token;
using WorldsAdriftServer.Helper;
using WorldsAdriftServer.Objects.DataObjects;
using WorldsAdriftServer.Helper.Data;

namespace WorldsAdriftServer.Handlers.CharacterScreen
{
    internal class CharacterCreateHandler : Handler
    {
        internal override string Method { get; } = "POST";
        internal override string[] URLs { get; } = { $"/reserveCharacterSlot/{Config.GameVersion}/steam/1234" };
        internal override bool CheckSteamToken { get; } = true;
        internal override bool CheckCharacterToken { get; } = false;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            if (!HttpParsers.HeaderByName("Security", httpRequest, out string steamToken) || !GetGuidFromAuthToken.Steam(steamToken, out string playerGuid))
            { return false; }

            PlayerData playerData = DataStore.Instance.PlayerDataDictionary[playerGuid];
            CharacterData characterData = new(string.Empty);

            if (!playerData.HasMainCharacter() && DataStore.Instance.PlayerCharacterNameData.TryGetValue(playerData.Name, out NameData? nameData))
            {
                nameData.Guid = characterData.Guid;
                characterData.Name = playerData.Name;
                characterData.characterCreationData.Name = playerData.Name;
            }

            playerData.CharacterGUIDs.Add(characterData.Guid);
            DataStore.Instance.CharacterDataDictionary.Add(characterData.Guid, characterData);

            return new CharacterListHandler().Handle(httpSession, httpRequest);
        }
    }
}
