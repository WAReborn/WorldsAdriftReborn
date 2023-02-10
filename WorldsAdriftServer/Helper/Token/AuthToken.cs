using NetCoreServer;
using WorldsAdriftServer.Objects.CharacterSelection;
using WorldsAdriftServer.Objects.SteamObjects;

namespace WorldsAdriftServer.Helper.Token
{
    internal class GenerateAuthToken
    {
        internal static bool Steam( SteamAuthRequestToken request, string guid, out SteamAuthResponseToken steamAuthToken )
        {
            //TODO generate token
            steamAuthToken = new(guid, true);
            steamAuthToken.ScreenName = request.BossaCredential.UserKey;
            return true;
        }
        internal static bool Character( HttpRequest httpRequest, out CharacterAuthResponse characterAuthToken )
        {
            HttpParsers.HeaderByName("characterUid", httpRequest, out string characterGuid);
            //TODO generate token
            characterAuthToken = new(characterGuid, true);
            return true;
        }
    }
    internal class CheckAuthToken
    {
        internal static bool Steam( HttpRequest httpRequest )
        {
            //TODO check token
            return true;
        }
        internal static bool Character( HttpRequest httpRequest )
        {
            //TODO check token
            return true;
        }
    }
    internal class GetGuidFromAuthToken
    {
        internal static bool Steam( string token, out string guid )
        {
            //TODO get guid from token
            guid = token;
            return true;
        }
        //We may not need this one as this date is stored in the HttpRequest header when this token is uesd
        internal static bool Character( string token, out string guid )
        {
            //TODO get guid from token
            guid = token;
            return true;
        }
    }
}
