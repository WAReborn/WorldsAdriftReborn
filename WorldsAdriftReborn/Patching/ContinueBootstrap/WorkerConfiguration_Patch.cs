using HarmonyLib;
using Improbable.Unity.Configuration;
using UnityEngine;

namespace WorldsAdriftReborn.Patching.Dynamic.ContinueBootstrap
{
    /*
     * after the intro the game fetches some steam auth metadata and then creates the WorkerConfiguration which needs to have the projectName to be set.
     * 
     * This can probably get removed as now we replace spatialos with our dll
     */
    [HarmonyPatch(typeof(WorkerConfiguration))]
    internal class WorkerConfiguration_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(WorkerConfiguration), "ProjectName", MethodType.Getter)]
        public static bool ProjectName_Getter_Prefix(WorkerConfiguration __instance, ref string __result )
        {
            string projectName = (string)AccessTools.Field(typeof(WorkerConfiguration), "projectName").GetValue(__instance);
            string appName = (string)AccessTools.Field(typeof(WorkerConfiguration), "appName").GetValue(__instance);

            if (string.IsNullOrEmpty(projectName) && string.IsNullOrEmpty(appName))
            {
                __result = "WorldsAdriftReborn";
                return false;
            }

            return true;
        }
        /*
         * LocatorHost is the server address of the gRPC server, so we overwrite the setter here to our self
         */
        [HarmonyPrefix]
        [HarmonyPatch(typeof(WorkerConfiguration), "LocatorHost", MethodType.Setter)]
        public static bool LocatorHost_Setter_Prefix(ref string value )
        {
            Debug.LogWarning("ORIGNIAL: " + value);
            value = "some.host.to.receive.this";
            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch(typeof(WorkerConfiguration), "Port", MethodType.Setter)]
        public static bool LocatorHost_Setter_Prefix( ref ushort value )
        {
            Debug.LogWarning("ORIGNIAL PORT: " + value);
            //value = 443;
            return true;
        }
    }
}
