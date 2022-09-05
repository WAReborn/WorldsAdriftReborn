using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using WorldsAdriftServer.Handlers;

namespace WorldsAdriftServer.Server
{
    internal class gRPC_Proxy : TcpServer
    {
        public gRPC_Proxy( IPAddress address, int port ) : base(address, port) { }
        protected override TcpSession CreateSession()
        {
            return new gRPC_Proxy_Session(this);
        }
        protected override void OnError( SocketError error )
        {
            Console.WriteLine("TCP SERVER ON ERROR");
        }
    }
}
