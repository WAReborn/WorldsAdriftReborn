using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.SteamObjects;

namespace WorldsAdriftServer.Handlers.Authentication
{
    internal static class SteamAuthenticationHandler
    {
        internal static bool HandleAuthRequest( HttpSession session, HttpRequest request, string playerName )
        {
            SteamAuthRequestToken? steamAuthRequest = JObject.Parse(request.Body).ToObject<SteamAuthRequestToken>();

            if (steamAuthRequest == null)
            { return false; }

            if (!DataManger.GlobleDataStore.PlayerDataDictionary.TryGetValue(steamAuthRequest.steamCredential.secret, out PlayerData? playerData))
            {
                playerData = new PlayerData(steamAuthRequest.steamCredential.secret);
                DataManger.GlobleDataStore.PlayerDataDictionary.Add(playerData.Token, playerData);
                DataManger.WriteData(DataManger.GlobleDataStore);
            }

            SteamAuthResponseToken respToken = new(playerData.Token, "777", "999", true);
            respToken.screenName = playerName;

            return SendData.SendJObject((JObject)JToken.FromObject(respToken), session);
        }
    }
}
