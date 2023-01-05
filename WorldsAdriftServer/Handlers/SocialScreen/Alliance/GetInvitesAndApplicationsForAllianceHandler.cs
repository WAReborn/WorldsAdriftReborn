using NetCoreServer;

namespace WorldsAdriftServer.Handlers.SocialScreen.Alliance
{
    internal class GetInvitesAndApplicationsForAllianceHandler : Handler
    {
        internal override string Method { get; } = "GET";
        internal override string[] URLs { get; } = { "/memberships/invites/alliance/" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = true;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            throw new NotImplementedException();
        }
    }
}
