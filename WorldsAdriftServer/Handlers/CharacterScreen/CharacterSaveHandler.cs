using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.CharacterSelection;

namespace WorldsAdriftServer.Handlers.CharacterScreen
{
    internal static class CharacterSaveHandler
    {
        internal static bool HandleCharacterSave( HttpSession session, HttpRequest request )
        {
            CharacterCreationData? requestCharacterData = JObject.Parse(request.Body).ToObject<CharacterCreationData>();

            if (requestCharacterData != null && DataManger.GlobleDataStore.PlayerDataDictionary.TryGetValue(request.Header(0).Item2, out PlayerData? playerData))
            {
                bool wasCharacterDataSaved = false;
                for (int i = 0; i < playerData.CharacterListResponse.characterList.Count; i++)
                {
                    CharacterCreationData playerCharacterData = playerData.CharacterListResponse.characterList[i];
                    if (playerCharacterData.characterUid == requestCharacterData.characterUid)
                    {
                        playerData.CharacterListResponse.characterList[i] = requestCharacterData;
                        playerData.CharacterListResponse.hasMainCharacter = true;
                        wasCharacterDataSaved = true;
                    }
                }

                if (!wasCharacterDataSaved)
                { return false; }

                if (playerData.CharacterListResponse.characterList.Count <= 5)
                {
                    playerData.CharacterListResponse.unlockedSlots = playerData.CharacterListResponse.characterList.Count + 1;
                }

                DataManger.WriteData(DataManger.GlobleDataStore);
                return SendData.SendJObject((JObject)JToken.FromObject(playerData.CharacterListResponse), session);
            }
            return false;
        }
    }
}
