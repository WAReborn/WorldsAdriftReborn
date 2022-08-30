using GameStateMachine;
using HarmonyLib;

namespace WorldsAdriftReborn.Patching.Dynamic.BypassEAC
{
    [HarmonyPatch(typeof(ConnectToNeededServersState))]
    internal class ConnectToNeededServersState_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof (ConnectToNeededServersState), "InitializeEACClient")]
        public static bool InitializeEACClient_Prefix(ref RSG.IPromise<object> __result)
        {
            // throws errors at bootup but still sais it initialized successfully
            return true;
        }
    }
}
