using System;
using BepInEx;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using WorldsAdriftReborn.Config;

namespace WorldsAdriftReborn
{
    [BepInPlugin("com.WAR.WorldsAdriftReborn", "WorldsAdriftReborn", "0.0.1")]
    internal class WorldsAdriftReborn : BaseUnityPlugin
    {
        private void Awake()
        {
            ModSettings.InitConfig();
            InitPatches();
        }

        private static void InitPatches()
        {
            Debug.Log("Patching Worlds Adrift...");

            try
            {
                Debug.Log("Applying patches from WorldsAdriftReborn 0.0.1");

                Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "com.WAR.com");

                Debug.Log("Patching completed successfully");
            }
            catch (Exception e)
            {
                Debug.Log("Unhandled exception occurred while patching the game: " + e);
            }
        }
    }
}
