using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using WorldsAdriftServer.Handlers;

namespace WorldsAdriftServer.Server
{
    internal class AuthServer : HttpServer
    {
        public AuthServer( IPAddress address, int port ) : base(address, port) { }

        protected override TcpSession CreateSession()
        {
            return new SteamAuthSession(this);
        }

        protected override void OnError( SocketError error )
        {
            Console.WriteLine("Socket error: " + error.ToString());
        }
    }
}
