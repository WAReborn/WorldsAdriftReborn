using System.Net;
using WorldsAdriftServer.Server;

namespace WorldsAdriftServer
{
    internal class WorldsAdriftServer
    {
        static void Main( string[] args )
        {
            int restPort = 8080;

            RequestRouterServer restServer = new RequestRouterServer(IPAddress.Any, restPort);

            //server.AddStaticContent() here to add some filesystem path to serve
            restServer.Start();

            Console.WriteLine("enter something to stop");
            Console.ReadKey();

            restServer.Stop();
        }
    }
}
