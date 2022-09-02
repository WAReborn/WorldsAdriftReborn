using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Objects.CharacterSelection;

namespace WorldsAdriftServer.Handlers.CharacterScreen
{
    internal static class CharacterSaveHandler
    {
        internal static void HandleCharacterSave(HttpSession session, HttpRequest request )
        {
            JObject reqO = JObject.Parse(request.Body);
            if(reqO != null)
            {
                CharacterCreationData characterData = reqO.ToObject<CharacterCreationData>();
                if(characterData != null)
                {
                    // todo for future: store changes
                    HttpResponse resp = new HttpResponse();

                    resp.SetBegin(200);
                    resp.SetBody("{}"); // the game does want to have a valid JObject. Its stored in CharacterSelectionHandler.LastReceivedCharacterList so maybe important to pass valid stuff here in the future

                    session.SendResponseAsync(resp);
                }
            }
        }
    }
}
