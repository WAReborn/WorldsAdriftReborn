using Bossa.Travellers.Visualisers.HealthCheck;
using HarmonyLib;

namespace WorldsAdriftReborn.Patching.SpatialOS.Prevent_Disconnect
{
    [HarmonyPatch(typeof(HeartbeatVisualiser))]
    internal class HeartbeatVisualizer_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(HeartbeatVisualiser), "Update")]
        public static bool Update_Prefix()
        {
            // the only purpose of the original method is to check for Time.realtimeSinceStartup - this.lastGsimMessage > 65f and if thats the case disconnect from server
            // as we have a lot of temp and dummy data atm we just skip this method, but should fix it up later on
            return false;
        }
    }
}
