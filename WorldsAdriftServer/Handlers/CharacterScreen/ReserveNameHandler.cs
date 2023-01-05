using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Token;
using WorldsAdriftServer.Helper;
using WorldsAdriftServer.Objects.DataObjects;
using WorldsAdriftServer.Handlers.CharacterScreen;
using WorldsAdriftServer.Helper.Data;

namespace WorldsAdriftServer.Handlers.Authentication
{
    internal class ReserveNameHandler : Handler
    {
        internal override string Method { get; } = "POST";
        internal override string[] URLs { get; } = { "/player/reserveName" };
        internal override bool CheckSteamToken { get; } = true;
        internal override bool CheckCharacterToken { get; } = false;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            ReserveNameRequest? reserveNameRequest = JObject.Parse(httpRequest.Body).ToObject<ReserveNameRequest>();

            if (!HttpParsers.HeaderByName("Security", httpRequest, out string steamToken) || !GetGuidFromAuthToken.Steam(steamToken, out string playerGuid) || reserveNameRequest == null)
            { return false; }


            if (DataStore.Instance.PlayerCharacterNameData.TryGetValue(reserveNameRequest.screenName, out NameData? nameData))
            {
                if (nameData.PlayerGuid != playerGuid || !string.IsNullOrEmpty(nameData.Guid))
                {
                    return false;
                }
                else
                {
                    nameData.Guid = reserveNameRequest.characterUid;
                }
            }
            else
            {
                nameData = new(reserveNameRequest.screenName, reserveNameRequest.characterUid, playerGuid);
                CharacterData? characterData = DataStore.Instance.CharacterDataDictionary[reserveNameRequest.characterUid];

                DataStore.Instance.PlayerCharacterNameData.Add(nameData.Name, nameData);
                characterData.Name = reserveNameRequest.screenName;
            }

            return new CharacterListHandler().Handle(httpSession, httpRequest);
        }
    }
    internal class ReserveNameRequest
    {
        public string screenName { get; set; } = string.Empty;
        public string characterUid { get; set; } = string.Empty;
    }
}
