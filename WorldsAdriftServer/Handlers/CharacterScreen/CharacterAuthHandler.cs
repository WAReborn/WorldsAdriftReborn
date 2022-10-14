using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Data;
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
        internal static bool HandleCharacterAuth( HttpSession session, HttpRequest request )
        {
            CharacterAuthResponse response = new(request.Header(0).Item2, "1", 123, "12.12.12", true);
            return SendData.SendJObject((JObject)JToken.FromObject(response), session);
        }
    }
}
