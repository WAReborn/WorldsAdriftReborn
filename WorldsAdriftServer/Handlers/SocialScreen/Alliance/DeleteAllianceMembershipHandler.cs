﻿using NetCoreServer;

namespace WorldsAdriftServer.Handlers.SocialScreen.Alliance
{
    internal class DeleteAllianceMembershipHandler : Handler
    {
        internal override string Method { get; } = "DELETE";
        internal override string[] URLs { get; } = { "/memberships/character/" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = true;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            throw new NotImplementedException();
        }
    }
}
