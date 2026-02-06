using System.Net;
using IniParser;
using IniParser.Model;
using IniParser.Parser;
using ConfigFile;
using WorldsAdriftServer.Server;

namespace WorldsAdriftServer
{
    internal class WorldsAdriftServer
    {
        static void Main( string[] args )
        {

            if (File.Exists("Config.ini"))
            {
                Console.WriteLine("The Config File Already exist");
            }
            else
            {
                Console.WriteLine("Creating Config File");
                PrepareConfigFile();
            }

            //Initialize the Config File
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("Config.ini");
            string serverPort = data["Network"]["serverport"];
            int restPort = int.Parse(serverPort);
            Console.WriteLine("Port set to : " + restPort);
            RequestRouterServer restServer = new RequestRouterServer(IPAddress.Any, restPort);

            //server.AddStaticContent() here to add some filesystem path to serve
            restServer.Start();

            Console.WriteLine("enter something to stop");
            Console.ReadKey();

            restServer.Stop();
        }
        static void PrepareConfigFile()
        {
            //Create the Config.ini file
            var MyIni = new IniFile("Config.ini");
            MyIni.Write("serverport", "8080", "Network");
            Console.WriteLine("Config File Created");
            return;
        }
    }
}
