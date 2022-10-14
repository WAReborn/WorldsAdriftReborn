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

            //server.AddStaticContent() here to add some filesystem path to serve
            restServer.Start();

            DataManger dataManger = new DataManger();

            Console.WriteLine("enter something to stop");
            Console.ReadKey();
            //this will only save if they press a key to stop
            //would like to find a way to triger this on console close
            DataManger.WriteData(DataManger.GlobleDataStore);

            restServer.Stop();
        }
    }
}
