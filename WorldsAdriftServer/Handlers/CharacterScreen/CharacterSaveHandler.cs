using NetCoreServer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.CharacterSelection;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.CharacterSelection;

namespace WorldsAdriftServer.Handlers.CharacterScreen
{
    internal static class CharacterSaveHandler
    {
        internal static void HandleCharacterSave( HttpSession session, HttpRequest request )
        {
            var reqO = JsonConvert.DeserializeObject<CharacterCreationData>(request.Body);
            GameContext db = new GameContext();
            if (reqO != null)
            {
                reqO.Cosmetics[CharacterSlotType.Head].Id = "1";
                reqO.Cosmetics[CharacterSlotType.Body].Id = "2";
                reqO.Cosmetics[CharacterSlotType.Feet].Id = "3";
                reqO.Cosmetics[CharacterSlotType.Face].Id = "4";
                reqO.Cosmetics[CharacterSlotType.FacialHair].Id = "5";
                reqO.Cosmetics[CharacterSlotType.Head].Health = 100f;
                reqO.Cosmetics[CharacterSlotType.Body].Health = 100f;
                reqO.Cosmetics[CharacterSlotType.Feet].Health = 100f;
                reqO.Cosmetics[CharacterSlotType.Face].Health = 100f;
                reqO.Cosmetics[CharacterSlotType.FacialHair].Health = 100f;
                var characterId = reqO.characterUid;
                Guid.TryParse(characterId, out Guid guidRes);
                var characterResult = db.Characters.FirstOrDefault(c => c.Id == guidRes);

                // todo for future: store changes
                if (characterResult == null)
                {
                    CharacterDataDTO characterData = new CharacterDataDTO(Guid.NewGuid(), reqO.Name, "awesome community server", reqO.serverIdentifier, JsonConvert.SerializeObject(reqO.Cosmetics), JsonConvert.SerializeObject(reqO.UniversalColors), reqO.isMale, reqO.seenIntro, reqO.skippedTutorial);
                    db.Characters.Add(characterData);
                }
                else
                {
                    characterResult.Cosmetics = JsonConvert.SerializeObject(reqO.Cosmetics);
                    characterResult.SeenIntro = reqO.seenIntro;
                    characterResult.SkippedTutorial = reqO.skippedTutorial;
                    db.Characters.Update(characterResult);
                }
                db.SaveChangesAsync().Wait();

                HttpResponse resp = new HttpResponse();

                resp.SetBegin(200);
                List<CharacterCreationData> characters = Character.GetCharacterList(db);
                CharacterListResponse characterList = new CharacterListResponse(characters);
                characterList.unlockedSlots = characters.Count; // let the player create a new character below the list of existing characters (last provided character above must be a GenerateNewCharacter())
                characterList.hasMainCharacter = true;
                characterList.havenFinished = true;
                JObject respO = (JObject)JToken.FromObject(characterList);
                if (respO != null)
                {
                    resp = new HttpResponse();
                    resp.SetBegin(200);
                    resp.SetBody(respO.ToString());

                    session.SendResponseAsync(resp);
                }
            }
        }
    }
}
