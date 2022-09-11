using System.Reflection;
using HarmonyLib;
namespace WorldsAdriftReborn.Patching.SpatialOS
{
    internal class ConnectionLifecycle_Patch
    {
        [HarmonyPatch()]
        class PrecacheAssets
        {
            [HarmonyTargetMethod]
            public static MethodBase GetTargetMethod()
            {
                return AccessTools.Method(
                                            AccessTools.TypeByName("ConnectionLifecycle"),
                                            "PrecacheAssets");
            }

            /*
             * Once you get behind the loading screen the game will initialize the SpatialOS connection.
             * during that process it will try to precache a few assets which are loaded in dynamically over the internet
             * for now we skip this but we may need to enable this later on again (see ConcurrentAssetPrecacher_Patch.StartPrecachingAsset.StartPrecachingAsset_Transpiler)
             */
            [HarmonyPrefix]
            public static bool PrecacheAssets_Prefix()
            {
                return true;
            }
        }
    }
}
