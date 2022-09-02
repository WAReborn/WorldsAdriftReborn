using System.Net.Sockets;
using NetCoreServer;
using WorldsAdriftServer.Handlers.Authentication;
using WorldsAdriftServer.Handlers.CharacterScreen;
using WorldsAdriftServer.Handlers.ServerStatus;

namespace WorldsAdriftServer.Handlers
{
    internal class RequestRouterHandler : HttpSession
    {
        public RequestRouterHandler( HttpServer server ) : base(server) { }

        protected override void OnReceived( byte[] buffer, long offset, long size )
        {
            Console.WriteLine("OnReceived");
            base.OnReceived( buffer, offset, size );
        }

        protected override void OnReceivedRequest( HttpRequest request )
        {
            if(request != null)
            {
                Console.WriteLine(request);
                if(request.Method == "POST" && request.Url == "/authenticate")
                {
                    SteamAuthenticationHandler.HandleAuthRequest(this, request, "Jim Hawkins");
                }
                else if (request.Method == "GET" && request.Url.Contains("/characterList/") && request.Url.Contains("/steam/1234"))
                {
                    CharacterListHandler.HandleCharacterListRequest(this, request, "community_server");
                }
                else if (request.Method == "POST" && request.Url.Contains("/reserveCharacterSlot/") && request.Url.Contains("/steam/1234"))
                {
                    // no need to handle this as we provide the needed data in HandleCharacterListRequest()
                }
                else if(request.Method == "GET" && request.Url == "/deploymentStatus")
                {
                    DeploymentStatusHandler.HandleDeploymentStatusRequest(this, request, "awesome community server", "community_server", 0);
                }
                else if(request.Method == "GET" && request.Url == "/authorizeCharacter")
                {
                    CharacterAuthHandler.HandleCharacterAuth(this, request);
                }
                else if(request.Method == "POST" && request.Url.Contains("/character/") && request.Url.Contains("/steam/1234/"))
                {
                    CharacterSaveHandler.HandleCharacterSave(this, request);
                }
            }
        }

        protected override void OnReceivedRequestError( HttpRequest request, string error )
        {
            Console.WriteLine("Request error: " + error);
        }

        protected override void OnError( SocketError error )
        {
            Console.WriteLine("Socket error: " + error.ToString());
        }
    }
}
