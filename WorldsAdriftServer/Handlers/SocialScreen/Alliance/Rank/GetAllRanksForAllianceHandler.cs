﻿using NetCoreServer;

namespace WorldsAdriftServer.Handlers.SocialScreen.Alliance.Rank
{
    internal class GetAllRanksForAllianceHandler : Handler
    {
        internal override string Method { get; } = "GET";
        internal override string[] URLs { get; } = { "/ranks" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = true;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            throw new NotImplementedException();
        }
    }
}
