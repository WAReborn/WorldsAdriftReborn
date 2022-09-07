using System;
using System.Reflection;
using HarmonyLib;

namespace WorldsAdriftReborn.Patching.Dynamic.BypassSteam
{
    [HarmonyPatch()]
    internal class SteamManager_Patch
    {
        [HarmonyTargetMethod]
        static MethodBase GetInternalAuthenticateMethod()
        {
            return AccessTools.Method(
                                        AccessTools.TypeByName("SteamManager"),
                                        "Authenticate",
                                        new Type[]
                                        {
                                            typeof(float)
                                        });
        }

        [HarmonyPrefix]
        public static bool Authenticate_Prefix(ref RSG.IPromise<string> __result, float authTimeout)
        {
            __result = RSG.Promise<string>.Resolved("blahblahletmethroughdupdiduuu");
            return false;
        }
    }
}
