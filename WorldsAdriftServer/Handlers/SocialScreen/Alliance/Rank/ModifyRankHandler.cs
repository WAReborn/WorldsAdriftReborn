using NetCoreServer;

namespace WorldsAdriftServer.Handlers.SocialScreen.Alliance.Rank
{
    internal class ModifyRankHandler : Handler
    {
        internal override string Method { get; } = "PUT";
        internal override string[] URLs { get; } = { "/rank" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = true;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            throw new NotImplementedException();
        }
    }
}
