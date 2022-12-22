using System;
using BepInEx;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using WorldsAdriftReborn.Config;
using System.Windows.Forms;

namespace WorldsAdriftReborn
{
    [BepInPlugin("com.WAR.WorldsAdriftReborn", "WorldsAdriftReborn", "0.0.1")]
    internal class WorldsAdriftReborn : BaseUnityPlugin
    {
        private void Awake()
        {
            // Adapted from: https://github.com/ManlyMarco/KK_GamepadSupport/blob/master/Core_GamepadSupport/GamepadSupportPlugin.cs
            try
            {
                // NEED to load the native dll BEFORE any class with DllImport is touched or the dll won't be found
                DependencyLoader.LoadDependencies();
            }
            catch (Exception ex)
            {
                string errorMsg = $"WorldsAdriftReborn plugin failed to load:\n{ex.Message}";

                Debug.LogError(errorMsg);

                MessageBox.Show(
                    errorMsg, 
                    "WorldsAdriftReborn Error",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                return;
            }

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
