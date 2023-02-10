using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Helper.Token;
using WorldsAdriftServer.Helper;
using WorldsAdriftServer.Objects.CharacterSelection;
using WorldsAdriftServer.Objects.DataObjects;
using WorldsAdriftServer.Objects.DeploymentStatus;

namespace WorldsAdriftServer.Handlers.CharacterScreen
{
    internal class CharacterSaveHandler : Handler
    {
        internal override string Method { get; } = "POST";
        internal override string[] URLs { get; } = { $"/character/{Config.GameVersion}/steam/1234" };
        internal override bool CheckSteamToken { get; } = true;
        internal override bool CheckCharacterToken { get; } = false;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            CharacterCreationData? requestCharacterData = JObject.Parse(httpRequest.Body).ToObject<CharacterCreationData>();
            if (requestCharacterData == null)
            { return false; }

            if (!HttpParsers.HeaderByName("Security", httpRequest, out string steamToken) || !GetGuidFromAuthToken.Steam(steamToken, out string playerGuid))
            { return false; }

            ServerStatusRecord serverStatusRecord = Config.ServerStatusDictionary[requestCharacterData.serverIdentifier];
            requestCharacterData.Server = serverStatusRecord.DisplayName;

            CharacterData characterData = DataStore.Instance.CharacterDataDictionary[requestCharacterData.characterUid];
            characterData.characterCreationData = requestCharacterData;
            characterData.Name = requestCharacterData.Name;

            if (!DataStore.Instance.PlayerCharacterNameData.ContainsKey(requestCharacterData.Name))
            {
                NameData nameData = new(requestCharacterData.Name, requestCharacterData.characterUid, playerGuid);
                DataStore.Instance.PlayerCharacterNameData.Add(nameData.Name, nameData);
            }

            return new CharacterListHandler().Handle(httpSession, httpRequest);
        }
    }
}
