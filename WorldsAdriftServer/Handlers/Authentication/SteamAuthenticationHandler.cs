using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Helper.Token;
using WorldsAdriftServer.Objects.DataObjects;
using WorldsAdriftServer.Objects.SteamObjects;

namespace WorldsAdriftServer.Handlers.Authentication
{
    internal class SteamAuthenticationHandler : Handler
    {
        internal override string Method { get; } = "POST";
        internal override string[] URLs { get; } = { "/authenticate" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = false;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            SteamAuthRequestToken? steamAuthRequest = JObject.Parse(httpRequest.Body).ToObject<SteamAuthRequestToken>();

            if (steamAuthRequest == null || string.IsNullOrEmpty(steamAuthRequest.BossaCredential.UserKey))
            {
                SendData.JObject((JObject)JToken.FromObject(new SteamAuthResponseToken()), httpSession);
                return true;
            }

            if (!DataStore.Instance.PlayerCharacterNameData.TryGetValue(steamAuthRequest.BossaCredential.UserKey, out NameData? nameData))
            {
                PlayerData playerData = new(steamAuthRequest.BossaCredential.UserKey);
                nameData = new(steamAuthRequest.BossaCredential.UserKey, string.Empty, playerData.Guid);

                DataStore.Instance.PlayerDataDictionary.Add(playerData.Guid, playerData);
                DataStore.Instance.PlayerCharacterNameData.Add(nameData.Name, nameData);
            }

            if (!GenerateAuthToken.Steam(steamAuthRequest, nameData.PlayerGuid, out SteamAuthResponseToken responseToken))
            { return false; }

            return SendData.JObject((JObject)JToken.FromObject(responseToken), httpSession);
        }
    }
}
