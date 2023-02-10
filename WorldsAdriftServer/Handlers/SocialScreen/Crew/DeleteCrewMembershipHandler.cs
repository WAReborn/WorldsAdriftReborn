using NetCoreServer;

namespace WorldsAdriftServer.Handlers.SocialScreen.Crew
{
    internal class DeleteCrewMembershipHandler : Handler
    {
        internal override string Method { get; } = "DELETE";
        internal override string[] URLs { get; } = { "/memberships/crew/" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = true;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            throw new NotImplementedException();
        }
    }
}
