using Bossa.Travellers.Utils;
using HarmonyLib;

namespace WorldsAdriftReborn.Patching.Dynamic.BypassSteam
{
    [HarmonyPatch(typeof(LoginMetadata))]
    internal class LoginMetadata_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoginMetadata.SteamMetadata))]
        public static bool SteamMetadata_Prefix(ref LoginMetadata __result )
        {
            __result = new LoginMetadata
            {
                UserId = "123",
                Credentials = "456",
                Platform = "steam",
                SteamAppId = "789"
            };
            return false;
        }
    }
}
