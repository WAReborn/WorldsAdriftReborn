using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Helper.Token;
using WorldsAdriftServer.Objects.CharacterSelection;
using WorldsAdriftServer.Helper.Data;

namespace WorldsAdriftServer.Handlers.Authentication
{
    internal class CharacterAuthHandler : Handler
    {
        internal override string Method { get; } = "GET";
        internal override string[] URLs { get; } = { "/authorizeCharacter" };
        internal override bool CheckSteamToken { get; } = true;
        internal override bool CheckCharacterToken { get; } = false;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            if (!GenerateAuthToken.Character(httpRequest, out CharacterAuthResponse characterAuthToken))
            { return false; }

            return SendData.JObject((JObject)JToken.FromObject(characterAuthToken), httpSession);
        }
    }
}
