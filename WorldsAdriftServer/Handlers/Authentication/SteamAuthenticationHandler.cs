using NetCoreServer;
using Newtonsoft.Json.Linq;
using WorldsAdriftServer.Objects;

namespace WorldsAdriftServer.Handlers.Authentication
{
    internal static class SteamAuthenticationHandler
    {
        internal static void HandleAuthRequest(HttpSession session, HttpRequest request, string playerName)
        {
            JObject reqO = JObject.Parse(request.Body);
            if (reqO != null)
            {
                SteamAuthRequestToken reqToken = reqO.ToObject<SteamAuthRequestToken>();

                if (reqToken != null)
                {
                    SteamAuthResponseToken respToken = new SteamAuthResponseToken("superCoolToken", "777", "999", true);
                    respToken.screenName = playerName;

                    JObject respO = (JObject)JToken.FromObject(respToken);
                    if (respO != null)
                    {
                        HttpResponse resp = new HttpResponse();
                        resp.SetBegin(200);
                        resp.SetBody(respO.ToString());

                        session.SendResponseAsync(resp);
                    }
                }
            }
        }
    }
}
