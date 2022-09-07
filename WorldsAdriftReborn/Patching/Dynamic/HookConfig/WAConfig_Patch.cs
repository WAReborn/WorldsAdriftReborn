using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using WorldsAdriftReborn.Config;

namespace WorldsAdriftReborn.Patching.Dynamic.HookConfig
{
    [HarmonyPatch]
    internal class WAConfig_Patch
    {
        [HarmonyTargetMethod]
        public static MethodBase GetTargetMethod()
        {
            return AccessTools.Method(
                                        AccessTools.TypeByName("WAConfig"),
                                        "Get",
                                        new Type[]
                                        {
                                            typeof(string)
                                        }).MakeGenericMethod(typeof(string));
        }

        [HarmonyPrefix]
        public static bool Get_Prefix(ref string __result, string key )
        {
            ModSettings.modConfig.Reload();

            if(key == "BossaNet.RestServerUrl")
            {
                __result = ModSettings.restServerUrl.Value;
                return false;
            }
            else if(key == "BossaNet.DeploymentStatusUrl")
            {
                __result = ModSettings.restServerDeploymentUrl.Value;
                return false;
            }
            Debug.LogWarning("not touching " + key);

            return true;
        }
    }
}
