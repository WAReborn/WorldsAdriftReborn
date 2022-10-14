using NetCoreServer;
using Newtonsoft.Json.Linq;

namespace WorldsAdriftServer.Helper.Data
{
    internal static class SendData
    {
        public static bool SendJObject( JObject responceObject, HttpSession httpSession )
        {
            if (responceObject != null)
            {
                HttpResponse httpResponse = new HttpResponse(200);
                httpResponse.SetBody(responceObject.ToString());
                httpSession.SendResponseAsync(httpResponse);
                return true;
            }
            return false;
        }
    }
}
