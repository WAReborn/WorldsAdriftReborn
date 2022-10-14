using System;
using System.Collections.Generic;
using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.CharacterSelection;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.CharacterSelection;

namespace WorldsAdriftServer.Handlers.CharacterScreen
{
    internal static class ReserveCharacterSlotHandler
    {
        internal static bool HandleReserveCharacterSlot( HttpSession session, HttpRequest request, string serverIdentifier )
        {
            if(DataManger.GlobleDataStore.PlayerDataDictionary.TryGetValue(request.Header(0).Item2, out PlayerData? playerData))
            {
                CharacterListResponse characterListResponse = playerData.CharacterListResponse;
                characterListResponse.characterList.Add(Character.GenerateNewCharacter(serverIdentifier, "Name"));
                playerData.CharacterListResponse = characterListResponse;

                DataManger.WriteData(DataManger.GlobleDataStore);
                return SendData.SendJObject((JObject)JToken.FromObject(characterListResponse), session);
            }
            return false;
        }
    }
}
