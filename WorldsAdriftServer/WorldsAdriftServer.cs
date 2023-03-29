using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Server;

namespace WorldsAdriftServer
{
    internal class WorldsAdriftServer
    {
        static void Main( string[] args )
        {
            GameContext db = new GameContext();
            int restPort = 8080;

            RequestRouterServer restServer = new RequestRouterServer(IPAddress.Any, restPort);
            Console.WriteLine("Run migrations...");
            db.Database.MigrateAsync().Wait();

            //server.AddStaticContent() here to add some filesystem path to serve
            restServer.Start();

            Console.WriteLine("enter something to stop");
            Console.ReadKey();

            restServer.Stop();
        }
    }
}
