using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using WorldsAdriftServer.Handlers;

namespace WorldsAdriftServer.Server
{
    internal class RequestRouterServer : HttpServer
    {
        public RequestRouterServer( IPAddress address, int port ) : base(address, port) { }

        protected override TcpSession CreateSession()
        {
            return new RequestRouterHandler(this);
        }

        protected override void OnError( SocketError error )
        {
            Console.WriteLine("Socket error: " + error.ToString());
        }
    }
}
