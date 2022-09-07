using System;
using Bossa.Travellers.Utils;
using HarmonyLib;
using Improbable;

namespace WorldsAdriftReborn.Patching.Dynamic.ContinueBootstrap
{
    [HarmonyPatch(typeof(Bootstrap))]
    internal class Bootstrap_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Bootstrap.Connect), new Type[] {typeof(LoginMetadata)})]
        public static bool Connect_Prefix(Bootstrap __instance, LoginMetadata loginMetadata )
        {
            __instance.WorkerConfigurationData.Debugging.ProtocolLoggingOnStartup = true;
            return true;
        }
    }
}
