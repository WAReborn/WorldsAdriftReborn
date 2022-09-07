using Bossa.Travellers.Utils;
using HarmonyLib;
using WorldsAdriftReborn.Config;

namespace WorldsAdriftReborn.Patching.Dynamic.BypassSteam
{
    [HarmonyPatch(typeof(LoginMetadata))]
    internal class LoginMetadata_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoginMetadata.SteamMetadata))]
        public static bool SteamMetadata_Prefix(ref LoginMetadata __result )
        {
            ModSettings.modConfig.Reload();

            __result = new LoginMetadata
            {
                UserId = ModSettings.steamUserId.Value,
                Credentials = "456",
                Platform = "steam",
                SteamAppId = ModSettings.steamAppId.Value
            };
            return false;
        }
    }
}
