using GameStateMachine;
using HarmonyLib;

namespace WorldsAdriftReborn.Patching.Dynamic.BypassSteam
{
    [HarmonyPatch(typeof(ConnectToNeededServersState))]
    internal class ConnectToNeededServersState_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ConnectToNeededServersState), "ConnectToAnalytics")]
        public static bool ConnectToAnalytics_Prefix(ref RSG.IPromise<object> __result )
        {
            __result = RSG.Promise<object>.Resolved(null);
            return false;
        }
    }
}
