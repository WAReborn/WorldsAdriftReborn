using System.Net;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Server;

namespace WorldsAdriftServer
{
    internal class WorldsAdriftServer
    {
        static void Main( string[] args )
        {
            int restPort = 8080;

            RequestRouterServer restServer = new RequestRouterServer(IPAddress.Any, restPort);

            DataStore dataStore = DataStore.ReadData();
            DataStore.Instance = dataStore;

            //server.AddStaticContent() here to add some filesystem path to serve
            restServer.Start();

            Console.WriteLine("Press enter to stop");
            Console.ReadLine();

            restServer.Stop();
        }
    }
}
