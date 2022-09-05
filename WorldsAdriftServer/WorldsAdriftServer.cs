using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using NetCoreServer;
using WorldsAdriftServer.Server;

namespace WorldsAdriftServer
{
    internal class WorldsAdriftServer
    {
        static void Main( string[] args )
        {
            int restPort = 8080;
            int TcpProxyPort = 4444;

            //var context = new SslContext(SslProtocols.Tls12, new X509Certificate2("certificate.pfx", "cheat"));

            RequestRouterServer restServer = new RequestRouterServer(IPAddress.Any, restPort);
            /*
             * The game connects over TLS and expects a valid certificate!
             * As i have a reverse nginx proxy setup which handles the TLS part for me i can use plain TCP here.
             */
            gRPC_Proxy sslProxy = new gRPC_Proxy(IPAddress.Any, TcpProxyPort);

            //server.AddStaticContent() here to add some filesystem path to serve
            restServer.Start();
            sslProxy.Start();

            Console.WriteLine("enter something to stop");
            Console.ReadKey();

            restServer.Stop();
            sslProxy.Stop();
        }
    }
}
