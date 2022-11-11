using NetCoreServer;
using Newtonsoft.Json.Linq;

namespace WorldsAdriftServer.Helper.Data
{
    internal static class SendData
    {
        public static bool JObject( JObject jObject, HttpSession httpSession )
        {
            if (jObject != null)
            {
                HttpResponse httpResponse = new(200);
                httpResponse.SetBody(jObject.ToString());
                httpSession.SendResponseAsync(httpResponse);
                return true;
            }
            return false;
        }
        public static void Error( HttpSession httpSession, HttpRequest httpRequest )
        {
            HttpResponse httpResponse = new(500);
            httpSession.SendResponseAsync(httpResponse);
            httpSession.Dispose();
        }
    }
}
