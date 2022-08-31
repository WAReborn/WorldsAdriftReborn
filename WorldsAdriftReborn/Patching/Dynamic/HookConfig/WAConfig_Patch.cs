using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

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
            if(key == "BossaNet.RestServerUrl")
            {
                __result = "http://127.0.0.1:8080";
                Debug.LogWarning("redirecting rest url to " + __result);
                return false;
            }
            Debug.LogWarning("not touching " + key);

            return true;
        }
    }
}
