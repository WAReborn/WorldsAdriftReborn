using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.CharacterSelection;
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
        internal static void HandleCharacterListRequest(HttpSession session, HttpRequest request, string serverIdentifier )
        {
            List<CharacterCreationData> list = new List<CharacterCreationData>();

            list.Add(Character.GenerateRandomCharacter(serverIdentifier, "Billy Bones"));
            list.Add(Character.GenerateRandomCharacter(serverIdentifier, "Long John Silver"));
            list.Add(Character.GenerateNewCharacter(serverIdentifier, "Jim Hawkins"));

            CharacterListResponse characterList = new CharacterListResponse(list);
            characterList.unlockedSlots = list.Count; // let the player create a new character below the list of existing characters (last provided character above must be a GenerateNewCharacter())
            characterList.hasMainCharacter = true;
            characterList.havenFinished = true;

            JObject respO = (JObject)JToken.FromObject(characterList);
            if (respO != null)
            {
                HttpResponse resp = new HttpResponse();
                resp.SetBegin(200);
                resp.SetBody(respO.ToString());

                session.SendResponseAsync(resp);
            }
        }
    }
}
