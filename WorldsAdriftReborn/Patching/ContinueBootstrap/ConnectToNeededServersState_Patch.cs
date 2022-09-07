using GameStateMachine;
using HarmonyLib;

namespace WorldsAdriftReborn.Patching.Dynamic.ContinueBootstrap
{
    [HarmonyPatch(typeof(ConnectToNeededServersState))]
    internal class ConnectToNeededServersState_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ConnectToNeededServersState), "ValidateClientVersion")]
        public static bool ValidateClientVersion_Prefix(ref RSG.IPromise<object> __result )
        {
            __result = RSG.Promise<object>.Resolved(null);
            return false;
        }
    }
}
