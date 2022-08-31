using System.Net;
using WorldsAdriftServer.Server;

namespace WorldsAdriftServer
{
    internal class WorldsAdriftServer
    {
        static void Main( string[] args )
        {
            int port = 8080;
            AuthServer server = new AuthServer(IPAddress.Any, port);

            //server.AddStaticContent() here to add some filesystem path to serve
            server.Start();

            Console.WriteLine("enter something to stop");
            Console.ReadKey();

            server.Stop();
        }
    }
}
