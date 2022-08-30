using Bossa.Travellers.Utils;
using HarmonyLib;

namespace WorldsAdriftReborn.Patching.Dynamic.BypassSteam
{
    [HarmonyPatch(typeof(SteamChecker))]
    internal class SteamChecker_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(SteamChecker.GetSteamBranch))]
        public static bool GetSteamBranch(ref string __result )
        {
            __result = "TheCoolBranch";
            return false;
        }
    }
}
