using HarmonyLib;
using Improbable.Unity.Configuration;

namespace WorldsAdriftReborn.Patching.Dynamic.ContinueBootstrap
{
    /*
     * after the intro the game fetches some steam auth metadata and then creates the WorkerConfiguration which needs to have the projectName to be set.
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
    }
}
