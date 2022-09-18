using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace WorldsAdriftReborn.Patching.SpatialOS
{
    [HarmonyPatch()]
    internal class WorkerSpecificAssetDatabaseTemplateProvider_Patch
    {
        [HarmonyTargetMethod]
        public static MethodBase GetTargetMethod()
        {
            return AccessTools.Method(
                                        AccessTools.TypeByName("WorkerSpecificAssetDatabaseTemplateProvider"),
                                        "GetEntityTemplate",
                                        new Type[]
                                        {
                                            typeof(string)
                                        });
        }

        private static void onSuccess(string something) { Debug.LogWarning("SUCCESS " + something); }
        private static void onError( Exception e ) { Debug.LogWarning("FAILED " + e.Message); }

        [HarmonyPrefix]
        public static void GetEntityTemplate_Prefix( object __instance, string prefabName )
        {
            // not sure where the game would normally call this, but as GetEntityTemplate expects the asset to be in cache (when loading from file system) we need to load it in (manually for now)
            AccessTools.Method(AccessTools.TypeByName("WorkerSpecificAssetDatabaseTemplateProvider"), "PrepareTemplate").Invoke(__instance, new object[] { prefabName, new Action<string>(onSuccess), new Action<Exception>(onError) });
        }
    }
}
