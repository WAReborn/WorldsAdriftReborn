using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using Assets.Improbable.Core.TemplateProviders;
using Bossa.Travellers.PrefabExporting.Preprocessors;
using HarmonyLib;
using Improbable.Assets;
using Improbable.Corelibrary.PreProcessor.Global;
using Improbable.Unity;
using Improbable.Unity.Assets;
using Improbable.Unity.Entity;
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

        [HarmonyPrefix]
        public static void GetEntityTemplate_Prefix( object __instance, string prefabName )
        {
            // The player seems to miss some components which cant be added through sdk calls that we know of, but they are added by the ExportProcess method which gets invoked when we compile the object
            // not sure if this will call the right one tho (there are multiple different ones) but it seems to produce different error messages when used compared to when not used.
            object assetDatabase = AccessTools.Field(AccessTools.TypeByName("WorkerSpecificAssetDatabaseTemplateProvider"), "AssetDatabase").GetValue(__instance);
            IDictionary<string, GameObject> dic = (IDictionary<string, GameObject>)AccessTools.Field(typeof(CachingAssetDatabase), "cachedGameObjects").GetValue(assetDatabase);
            PrefabCompiler p = new PrefabCompiler(WorkerPlatform.UnityClient);
            GameObject gObject;
            if (dic.TryGetValue(prefabName + "_unityclient", out gObject))
            {
                p.Compile(gObject);
                Debug.LogWarning("COMPILED PLAYER GAMEOBJECT!!!");
            }
            else
            {
                Debug.LogWarning("COMPILE FAILED " + prefabName + "_unityclient");
            }
        }
    }
}
