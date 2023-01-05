using System.Net.Sockets;
using NetCoreServer;
using WorldsAdriftServer.Handlers.Authentication;
using WorldsAdriftServer.Handlers.CharacterScreen;
using WorldsAdriftServer.Handlers.ServerStatus;
using WorldsAdriftServer.Handlers.SocialScreen;
using WorldsAdriftServer.Helper.Data;

namespace WorldsAdriftServer.Handlers
{
    internal class RequestRouterHandler : HttpSession
    {
        public RequestRouterHandler( HttpServer server ) : base(server) { }

        protected override void OnReceived( byte[] buffer, long offset, long size )
        {
            // OnReceived isn't guaranteed to get entire request. Report what was received.
            if (buffer != null && size != 0)  { DataParser.ParseIncomingData(buffer, offset, size); }
            base.OnReceived(buffer, offset, size);
        }

        // NOTE: OnReceivedRequest is only called once a complete request has been constructed inside HttpRequest's _cache.
        protected override void OnReceivedRequest( HttpRequest request )
        {
            try
            {
                foreach (Handler handler in Handlers)
                {
                    if (handler.TryHandle(this, request))
                    { break; }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                SendData.Error(this, request);
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

        internal static List<Handler> Handlers { get; } = new()
        {
            new CharacterAuthHandler(),
            new ReserveNameHandler(),
            new SteamAuthenticationHandler(),
            new CharacterCreateHandler(),
            new CharacterDeleteHandler(),
            new CharacterListHandler(),
            new CharacterSaveHandler(),
            new DeploymentStatusHandler(),
            new AcceptInviteHandler(),
            new CancelInviteHandler(),
            new CharacterMembershipsHandler(),
            new CharacterSearchHandler(),
            new InvitesAndApplicationsForPlayerHandler(),
            new RejectInviteHandler(),
            new SendInviteHandler(),
            new AllianceBatchRequestHandler(),
            new AllianceSearchHandler(),
            new ApplyToAllianceHandler(),
            new CreateAllianceHandler(),
            new DeleteAllianceMembershipHandler(),
            new DisbandAllianceHandler(),
            new GetAllianceForCharacterHandler(),
            new GetAllianceHandler(),
            new GetInvitesAndApplicationsForAllianceHandler(),
            new ListAlliancesHandler(),
            new ListMembersByAllianceHandler(),
            new UpdateAllianceInfoHandler(),
            new UpdatePlayerMembershipHandler(),
            new CreateRankHandler(),
            new DeleteRankHandler(),
            new GetAllRanksForAllianceHandler(),
            new ModifyRankHandler(),
            new CreateCrewHandler(),
            new DeleteCrewMembershipHandler(),
            new DisbandCrewHandler(),
            new GetCrewMembersHandler(),
            new GetMyCrewDataHandler(),
            new ListCrewInvitesHandler(),
            new VersionCheckHandler(),
        };
    }
}
