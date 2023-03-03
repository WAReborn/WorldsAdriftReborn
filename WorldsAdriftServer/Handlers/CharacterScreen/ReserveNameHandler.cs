using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCoreServer;
using Newtonsoft.Json;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.CharacterSelection;

namespace WorldsAdriftServer.Handlers.CharacterScreen
{
    internal class ReserveNameHandler
    {
        internal static void HandleCharacterReserveName( HttpSession session, HttpRequest request )
        {
            var reqO = JsonConvert.DeserializeObject<ReserveNameRequest>(request.Body);
            if( reqO != null )
            {
                GameContext db = new GameContext();
                var characterResult = db.Characters.FirstOrDefault(c => c.Name == reqO.screenName);
                db.Dispose();
                HttpResponse resp = new HttpResponse();
                if (characterResult == null)
                {
                    resp.SetBegin(200);
                    resp.SetBody(request.Body);
                    session.SendResponseAsync(resp);
                } else
                {
                    resp.SetBegin(400);
                    var respO = new ReserveNameResponse() { desc = $"The name: {reqO.screenName} is already taken." };
                    resp.SetBody(JsonConvert.SerializeObject(respO));
                    session.SendResponseAsync(resp);
                }
            }
        }
    }
}
