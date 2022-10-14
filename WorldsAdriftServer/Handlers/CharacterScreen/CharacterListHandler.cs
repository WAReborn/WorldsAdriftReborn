using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.CharacterSelection;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.CharacterSelection;

namespace WorldsAdriftServer.Handlers.CharacterScreen
{
    internal static class CharacterListHandler
    {
        /*
         * URL: /characterList/{buildNumber}/steam/1234
         * 
         * once the user clicks on the play button the game requests a list of characters.
         * the response also decides whether there is an option to create a new character using the unlockedSlots field
         */
        internal static bool HandleCharacterListRequest( HttpSession session, HttpRequest request )
        {
            CharacterListResponse characterListResponse;
            if (!DataManger.GlobleDataStore.PlayerDataDictionary.TryGetValue(request.Header(0).Item2, out PlayerData? playerData))
            {
                playerData = new PlayerData(request.Cookie(1).Item2);
                DataManger.GlobleDataStore.PlayerDataDictionary.Add(playerData.Token, playerData);
            }
            characterListResponse = playerData.CharacterListResponse;

            if (characterListResponse.characterList.Count <= 5)
            {
                characterListResponse.unlockedSlots = characterListResponse.characterList.Count + 1;
            }

            DataManger.WriteData(DataManger.GlobleDataStore);

            return SendData.SendJObject((JObject)JToken.FromObject(characterListResponse), session);
        }
    }
}
