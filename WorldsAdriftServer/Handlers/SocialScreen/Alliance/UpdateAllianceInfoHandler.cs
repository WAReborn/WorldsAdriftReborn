using NetCoreServer;

namespace WorldsAdriftServer.Handlers.SocialScreen.Alliance
{
    internal class UpdateAllianceInfoHandler : Handler
    {
        internal override string Method { get; } = "PATCH";
        internal override string[] URLs { get; } = { $"/alliance/{Config.Region}/" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = true;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            throw new NotImplementedException();
        }
    }
}
