using Bossa.Travellers.Utils;
using HarmonyLib;
using WorldsAdriftReborn.Config;

namespace WorldsAdriftReborn.Patching.Dynamic.BypassSteam
{
    [HarmonyPatch(typeof(SteamChecker))]
    internal class SteamChecker_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(SteamChecker.GetSteamBranch))]
        public static bool GetSteamBranch(ref string __result )
        {
            ModSettings.modConfig.Reload();

            __result = ModSettings.steamBranchName.Value;
            return false;
        }

        // Travellers.UI.Login.SetLoginFormActive() calls this and disables login if we return true or crash because of other steam bypasses
        [HarmonyPrefix]
        [HarmonyPatch(typeof(SteamChecker), "IsSteamBranchPTS")]
        public static bool IsSteamBranchPTS_Prefix(ref bool __result )
        {
            __result = false;
            return false;
        }
    }
}
