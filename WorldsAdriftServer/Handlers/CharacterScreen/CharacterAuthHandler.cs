using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Objects.CharacterSelection;

namespace WorldsAdriftServer.Handlers.CharacterScreen
{
    internal static class CharacterAuthHandler
    {
        /*
         * When the player clicks on "Enter World" the game sends a request for this answer.
         * it also adds two headers: Security and characterUid. first contains the steam auth token, second the characters uid.
         * in the future we should check all of those, for now allow all
         */
        internal static void HandleCharacterAuth(HttpSession session, HttpRequest request )
        {
            HttpResponse resp = new HttpResponse();
            CharacterAuthResponse authResp = new CharacterAuthResponse("token", "1", 123, "12.12.12", true);

            JObject respO = (JObject)JToken.FromObject(authResp);
            if(respO != null)
            {
                resp.SetBegin(200);
                resp.SetBody(respO.ToString());

                session.SendResponseAsync(resp);
            }
        }
    }
}
