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
                    //ItemData itemData = new ItemData("1", "prefab?", new ColorProperties(new UnityColor(0.5f, 0.5f, 0.5f, 0.5f), new UnityColor(0.5f, 0.5f, 0.5f, 0.5f), new UnityColor(0.5f, 0.5f, 0.5f, 0.5f)), 100f);
                    Dictionary<CharacterSlotType, ItemData> cosmetics = new Dictionary<CharacterSlotType, ItemData>();
                    CharacterUniversalColors colors = new CharacterUniversalColors();

                    Random r = new Random();
                    int num = r.Next(0, CustomisationSettings.skinColors.Length);

                    colors.SkinColor = CustomisationSettings.skinColors[num];
                    colors.LipColor = CustomisationSettings.lipColors[num];
                    colors.HairColor = CustomisationSettings.hairColors[r.Next(0, CustomisationSettings.hairColors.Length)];

                    cosmetics.Add(CharacterSlotType.Head, new ItemData(
                                                                        "1",
                                                                        CustomisationSettings.starterHeadItems.Keys.ToList<string>()[r.Next(0, CustomisationSettings.starterHeadItems.Keys.Count)],
                                                                        new ColorProperties(
                                                                                            CustomisationSettings.clothingColors[r.Next(0, 7)],
                                                                                            CustomisationSettings.clothingColors[r.Next(0, 7)]),
                                                                        100f));
                    cosmetics.Add(CharacterSlotType.Body, new ItemData(
                                                                        "2",
                                                                        CustomisationSettings.starterTorsoItems.Keys.ToList<string>()[r.Next(0, CustomisationSettings.starterTorsoItems.Keys.Count)],
                                                                        new ColorProperties(
                                                                                            CustomisationSettings.clothingColors[r.Next(0, 7)],
                                                                                            CustomisationSettings.clothingColors[r.Next(0, 7)]),
                                                                        100f));
                    cosmetics.Add(CharacterSlotType.Feet, new ItemData(
                                                                        "3",
                                                                        CustomisationSettings.starterLegItems.Keys.ToList<string>()[r.Next(0, CustomisationSettings.starterLegItems.Keys.Count)],
                                                                        new ColorProperties(
                                                                                            CustomisationSettings.clothingColors[r.Next(0, 7)],
                                                                                            CustomisationSettings.clothingColors[r.Next(0, 7)]),
                                                                        100f));
                    cosmetics.Add(CharacterSlotType.Face, new ItemData(
                                                                        "4",
                                                                        CustomisationSettings.starterFaceItems.Keys.ToList<string>()[r.Next(0, CustomisationSettings.starterFaceItems.Keys.Count)],
                                                                        default(ColorProperties),
                                                                        100f));
                    cosmetics.Add(CharacterSlotType.FacialHair, new ItemData(
                                                                        "5",
                                                                        CustomisationSettings.starterFacialHairItems.Keys.ToList<string>()[r.Next(0, CustomisationSettings.starterFacialHairItems.Keys.Count)],
                                                                        default(ColorProperties),
                                                                        100f));

                    CharacterCreationData characterData = new CharacterCreationData(1, "UID", "sp00ktober", "serverName?", "sp00ktober's_Server", cosmetics, colors, true, false, false);
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
                    serverStatus.Add("sp00ktober's_Server", new ServerStatusRecord("reborn server", "sp00ktober's_Server", ServerStatus.up, "1"));

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
