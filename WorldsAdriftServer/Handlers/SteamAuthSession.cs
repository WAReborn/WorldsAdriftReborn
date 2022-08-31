using System.Net.Sockets;
using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Objects;

namespace WorldsAdriftServer.Handlers
{
    internal class SteamAuthSession : HttpSession
    {
        public SteamAuthSession( HttpServer server ) : base(server) { }

        protected override void OnReceivedRequest( HttpRequest request )
        {
            if(request != null)
            {
                Console.WriteLine(request);
                if(request.Method == "POST" && request.Url == "/authenticate")
                {
                    JObject reqO = JObject.Parse(request.Body);
                    if(reqO != null)
                    {
                        SteamAuthRequestToken reqToken = reqO.ToObject<SteamAuthRequestToken>();

                        if(reqToken != null)
                        {
                            Console.WriteLine("received steam auth request");

                            SteamAuthResponseToken respToken = new SteamAuthResponseToken("superCoolToken", "777", "999");
                            respToken.screenName = "sp00ktober";

                            JObject respO = (JObject)JToken.FromObject(respToken);
                            if(respO != null)
                            {
                                Console.WriteLine("sending steam auth response");
                                SendResponseAsync(Response.MakeOkResponse().SetBody(respO.ToString()));
                            }
                        }
                    }
                }
            }
        }

        protected override void OnReceivedRequestError( HttpRequest request, string error )
        {
            Console.WriteLine("Request error: " + error);
        }

        protected override void OnError( SocketError error )
        {
            Console.WriteLine("Socket error: " + error.ToString());
        }
    }
}
