using System.Collections.Generic;
using Bossa.Travellers.Analytics;
using HarmonyLib;

namespace WorldsAdriftReborn.Patching.BypassPlayerAnalytics
{
    [HarmonyPatch(typeof(PlayerAnalytics))]
    internal class PlayerAnalytics_Patch
    {
        // if needed we can enable this again later but for now i disable it because it has a few nullrefs that i dont want to fix rn
        // important for FreezeDetector.Update()
        [HarmonyPrefix]
        [HarmonyPatch(nameof(PlayerAnalytics.GeneratePerformanceReport))]
        public static bool GeneratePerformanceReport(ref Dictionary<string, object> __result)
        {
            __result = new Dictionary<string, object>();
            return false;
        }
    }
}
