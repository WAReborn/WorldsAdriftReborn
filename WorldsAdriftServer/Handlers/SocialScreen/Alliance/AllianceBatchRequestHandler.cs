using NetCoreServer;

namespace WorldsAdriftServer.Handlers.SocialScreen.Alliance
{
    internal class AllianceBatchRequestHandler : Handler
    {
        internal override string Method { get; } = "POST";
        internal override string[] URLs { get; } = { $"/alliance/{Config.Region}/batch" };
        internal override bool CheckSteamToken { get; } = false;
        internal override bool CheckCharacterToken { get; } = true;
        internal override bool Handle( HttpSession httpSession, HttpRequest httpRequest )
        {
            throw new NotImplementedException();
        }
    }
}
