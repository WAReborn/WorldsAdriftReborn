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
            // OnReceived isn't guaranteed to get entire request. Report what was received.
            for (int ByteIndex=0; ByteIndex < size; ++ByteIndex)
            {
                Console.Write((char)buffer[ByteIndex]);
            }
            base.OnReceived(buffer, offset, size);
        }

        // NOTE: OnReceivedRequest is only called once a complete request has been constructed inside HttpRequest's _cache.
        protected override void OnReceivedRequest( HttpRequest request )
        {
            if(request != null)
            {
                bool wasRequestHandled = request switch
                {
                    HttpRequest when request.Method == "POST" && request.Url == "/authenticate" =>
                        SteamAuthenticationHandler.HandleAuthRequest(this, request, "Jim Hawkins"),
                    HttpRequest when request.Method == "GET" && request.Url.Contains("/characterList/") =>
                        CharacterListHandler.HandleCharacterListRequest(this, request),
                    HttpRequest when request.Method == "POST" && request.Url.Contains("/reserveCharacterSlot/") =>
                        ReserveCharacterSlotHandler.HandleReserveCharacterSlot(this, request, "community_server"),
                    HttpRequest when request.Method == "GET" && request.Url == "/deploymentStatus" =>
                        DeploymentStatusHandler.HandleDeploymentStatusRequest(this, request, "awesome community server", "community_server", 0),
                    HttpRequest when request.Method == "GET" && request.Url == "/authorizeCharacter" =>
                        CharacterAuthHandler.HandleCharacterAuth(this, request),
                    HttpRequest when request.Method == "POST" && request.Url.Contains("/character/") =>
                        CharacterSaveHandler.HandleCharacterSave(this, request),
                    HttpRequest when request.Method == "GET" && request.Url.Contains("/memberships/character/") =>
                        CharacterMembershipsHandler.HandleCharacterMemberships(this, request),
                    HttpRequest when request.Method == "GET" && request.Url.Contains("/memberships/invites/character/") =>
                        InvitesAndApplicationsForPlayerHandler.HandleInvitesAndApplicationsForPlayer(this, request),
                    HttpRequest when request.Method == "GET" && request.Url.Contains("/alliances") =>
                        ListAlliancesHandler.HandleListAlliances(this, request),
                    HttpRequest when request.Method == "POST" && request.Url.Contains("/crews") =>
                        CreateCrewHandler.HandleCreateCrew(this, request),
                    HttpRequest when request.Method == "POST" && request.Url.Contains("/alliance") =>
                        CreateAllianceHandler.HandleCreateAlliance(this, request),
                    _ => false
                };
                Console.WriteLine($"\nWas request handled: {wasRequestHandled}\n");
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
