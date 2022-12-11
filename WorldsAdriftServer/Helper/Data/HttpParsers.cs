using NetCoreServer;

namespace WorldsAdriftServer.Helper
{
    internal static class HttpParsers
    {
        internal static bool GUIDsFromURL( string url, out List<string> guids )
        {
            guids = new List<string>();

            string[] splits = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string split in splits)
            {
                if (Guid.TryParse(split.ToCharArray(), out Guid result))
                { guids.Add(result.ToString()); }
            }
            return guids.Count > 0;
        }
        internal static bool HeaderByName( string name, HttpRequest httpRequest, out string value )
        {
            value = string.Empty;
            for (int i = 0; i < httpRequest.Headers; i++)
            {
                (string, string) header = httpRequest.Header(i);
                if (header.Item1 == name)
                {
                    value = header.Item2;
                    return true;
                }
            }
            return false;
        }
    }
}
