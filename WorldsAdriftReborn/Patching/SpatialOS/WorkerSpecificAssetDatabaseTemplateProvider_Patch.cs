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
using GameDBFSIM;

namespace WorldsAdriftReborn.Patching.SpatialOS
{
    [HarmonyPatch()]
    internal class WorkerSpecificAssetDatabaseTemplateProvider_Patch
    {
        // will force the game to update the gameDB
        public static class GameDBAccessorPatch
        {
            private static readonly string CustomGameDBServerUrl = "https://your-custom-url.com";
            private static readonly string CustomS3DownloadUrl = "https://your-custom-s3-url.com";

            [HarmonyPatch(typeof(ConfigKeys))]
            public static class ConfigKeysPatch
            {
                // The game appears to attempt to use 2 URLs to download gameDB
                // One has the name of S3 suggesting a possible Amazon URL?
                // We will set one URL to the gameDB and make the other bounce to see what happens.
                [HarmonyPostfix]
                [HarmonyPatch("GameDBServerUrl")]
                public static void Postfix( ref string __result )
                {
                    // Replace the original result with your custom URL
                    __result = CustomGameDBServerUrl;
                }

                [HarmonyPostfix]
                [HarmonyPatch("S3DownloadUrl")]
                public static void PostfixS3( ref string __result )
                {
                    // Replace the original result with your custom S3 URL
                    __result = CustomS3DownloadUrl;
                }
            }

            // Add a method, constructor, or code block to execute the gameDBAccessor.TriggerGameDBUpdate
            public static void ExecuteGameDBUpdate()
            {
                GameDBAccessor gameDBAccessor = new GameDBAccessor();
                // Assuming gameDBAccessor is an instance of GameDBAccessor
                gameDBAccessor.TriggerGameDBUpdate(GameDBAccessor.Identifier.FSIM);
            }
        }

        // In your game's initialization code (e.g., in an Awake method)
        public class GameInitializer : MonoBehaviour
        {
            private void Awake()
            {
                // Apply the Harmony patch
                Harmony harmony = new Harmony("your.mod.id");
                harmony.PatchAll();
                GameDBAccessorPatch.ExecuteGameDBUpdate();
            }
        }


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
