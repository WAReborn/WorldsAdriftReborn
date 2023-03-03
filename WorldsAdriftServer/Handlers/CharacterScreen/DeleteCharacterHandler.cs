using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCoreServer;
using Newtonsoft.Json;
using WorldsAdriftServer.Helper.CharacterSelection;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.CharacterSelection;

namespace WorldsAdriftServer.Handlers.CharacterScreen
{
    internal class DeleteCharacterHandler
    {
        internal static void HandleCharacterDelete( HttpSession session, HttpRequest request, string characterId )
        {
            GameContext db = new GameContext();
            var character = db.Characters.FirstOrDefault(c => c.Id == Guid.Parse(characterId));
            var respO = new HttpResponse();
            if(character != null)
            {
                db.Characters.Remove(character);
                db.SaveChanges();
                var list = Character.GetCharacterList(db);
                var listResponse = new CharacterListResponse(list);
                listResponse.hasMainCharacter = true;
                listResponse.unlockedSlots = list.Count;
                listResponse.havenFinished = true;
                respO.SetBegin(200);
                respO.SetBody(JsonConvert.SerializeObject(listResponse));
            }
            else
            {
                respO.SetBegin(404);
                respO.SetBody("character does not exist");
            }
            session.SendResponseAsync(respO);
        }
    }
}
