using System.Net.Sockets;
using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Objects;
using WorldsAdriftServer.Objects.CharacterSelection;
using WorldsAdriftServer.Objects.deploymentStatus;

namespace WorldsAdriftServer.Handlers
{
    internal class SteamAuthSession : HttpSession
    {
        public SteamAuthSession( HttpServer server ) : base(server) { }

        protected override void OnReceived( byte[] buffer, long offset, long size )
        {
            Console.WriteLine("OnReceived");

            MemoryStream ms = new MemoryStream(buffer);
            BinaryReader br = new BinaryReader(ms);

            Console.WriteLine(br.ReadString());

            br.Close();
            ms.Close();

            base.OnReceived( buffer, offset, size );
        }

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

                            SteamAuthResponseToken respToken = new SteamAuthResponseToken("superCoolToken", "777", "999", true);
                            respToken.screenName = "sp00ktober";

                            JObject respO = (JObject)JToken.FromObject(respToken);
                            if(respO != null)
                            {
                                Console.WriteLine("sending steam auth response");
                                // need to craft more granular as NetCoreServer does merge two http headers (MakeOkResponse() already calls SetBody() which sets a Content-Length header, setting the body later again adds another one)
                                HttpResponse resp = new HttpResponse();
                                resp.SetBegin(200);
                                resp.SetBody(respO.ToString());

                                SendResponseAsync(resp);
                            }
                        }
                    }
                }
                // /characterList/{buildNumber}/steam/1234
                // sent when user clicks on the Play button
                else if (request.Method == "GET" && request.Url.Contains("/characterList/") && request.Url.Contains("/steam/1234"))
                {
                    ItemData itemData = new ItemData("1", "prefab?", new ColorProperties(new UnityColor(0.5f, 0.5f, 0.5f, 0.5f), new UnityColor(0.5f, 0.5f, 0.5f, 0.5f), new UnityColor(0.5f, 0.5f, 0.5f, 0.5f)), 100f);
                    Dictionary<CharacterSlotType, ItemData> dict = new Dictionary<CharacterSlotType, ItemData>();
                    CharacterUniversalColors colors = new CharacterUniversalColors();

                    dict.Add(CharacterSlotType.Body, itemData);

                    CharacterCreationData characterData = new CharacterCreationData(1, "UID", "sp00ktober", "sp00ktober", "hmm", dict, colors, true, false, false);
                    List<CharacterCreationData> list = new List<CharacterCreationData>();

                    list.Add(characterData);

                    CharacterListResponse characterList = new CharacterListResponse(list);

                    JObject respO = (JObject)JToken.FromObject(characterList);
                    if(respO != null)
                    {
                        HttpResponse resp = new HttpResponse();
                        resp.SetBegin(200);
                        resp.SetBody(respO.ToString());

                        Console.WriteLine("sending response for characters");
                        SendResponseAsync(resp);
                    }
                }
                // after requesting the character list the game directly checks the deploymentStatus
                else if(request.Method == "GET" && request.Url == "/deploymentStatus")
                {
                    Console.WriteLine("got deploymentStatus request.");

                    Dictionary<string, ServerStatusRecord> serverStatus = new Dictionary<string, ServerStatusRecord>();
                    serverStatus.Add("notSureYetWhatComesHere", new ServerStatusRecord("sp00ktober", "hmm", ServerStatus.up, "1"));

                    JObject respO = (JObject)JToken.FromObject(serverStatus);
                    if(respO != null)
                    {
                        Console.WriteLine("sending deploymentStatus response");

                        HttpResponse resp = new HttpResponse();
                        resp.SetBegin(200);
                        resp.SetBody(respO.ToString());

                        SendResponseAsync(resp);
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
