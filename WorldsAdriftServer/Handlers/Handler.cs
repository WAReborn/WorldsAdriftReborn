using NetCoreServer;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Helper.Token;

namespace WorldsAdriftServer.Handlers
{
    public abstract class Handler
    {
        internal abstract string Method { get; }
        internal abstract string[] URLs { get; }
        internal abstract bool CheckSteamToken { get; }
        internal abstract bool CheckCharacterToken { get; }
        internal abstract bool Handle( HttpSession httpSession, HttpRequest httpRequest );
        internal bool TryHandle( HttpSession httpSession, HttpRequest httpRequest )
        {
            if (!(Method == httpRequest.Method))
            { return false; }

            foreach (string url in URLs)
            {
                if (!httpRequest.Url.Contains(url))
                { return false; }
            }

            if (CheckSteamToken && !CheckAuthToken.Steam(httpRequest))
            { return false; }

            if (CheckCharacterToken && !CheckAuthToken.Character(httpRequest))
            { return false; }

            bool wasHandled = Handle(httpSession, httpRequest);
            DataStore.WriteData(DataStore.Instance);

            if (wasHandled) { Console.WriteLine($"Request was handled by {GetType()}"); }
            else { Console.WriteLine($"Handler {GetType()} failed to handled request"); }

            return wasHandled;
        }
    }
}
