using System;
using System.Collections.Generic;
using HarmonyLib;
using Improbable.Unity.Assets;
using UnityEngine;

namespace WorldsAdriftReborn.Patching.SpatialOS
{
    [HarmonyPatch(typeof(CachingAssetDatabase))]
    internal class CachingAssetDatabase_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(CachingAssetDatabase.LoadAsset))]
        public static void LoadAsset_Prefix(CachingAssetDatabase __instance, string prefabName, Action<GameObject> onAssetLoaded, Action<Exception> onError )
        {
            Debug.LogWarning("TRYING TO LOAD " + prefabName);

            Dictionary<string, GameObject> objects = (Dictionary<string, GameObject>)AccessTools.Field(typeof(CachingAssetDatabase), "cachedGameObjects").GetValue(__instance);
            foreach (KeyValuePair<string, GameObject> kvp in objects)
            {
                Debug.LogWarning("IN CACHE: " + kvp.Key);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CachingAssetDatabase.LoadAsset))]
        public static void LoadAsset_Postfix( CachingAssetDatabase __instance, string prefabName, Action<GameObject> onAssetLoaded, Action<Exception> onError )
        {
            Debug.LogWarning("DID I LOAD IT? " + prefabName);

            Dictionary<string, GameObject> objects = (Dictionary<string, GameObject>)AccessTools.Field(typeof(CachingAssetDatabase), "cachedGameObjects").GetValue(__instance);
            foreach (KeyValuePair<string, GameObject> kvp in objects)
            {
                Debug.LogWarning("IN CACHE: " + kvp.Key);
            }
        }
    }
}
