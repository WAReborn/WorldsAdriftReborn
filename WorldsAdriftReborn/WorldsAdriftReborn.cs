using System;
using BepInEx;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using WorldsAdriftReborn.Config;
using System.Windows.Forms;
using System.IO;
using System.Linq;

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

                // Verify game assembly compatibility
                Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(innerAssembly => innerAssembly.GetName().Name == "Assembly-CSharp");
                string moduleVersionId = assembly.ManifestModule.ModuleVersionId.ToString();
                string expectedModuleVersionId = "70f2ca59-e029-4973-b4e9-e0098e0ad02d";
                if (moduleVersionId != expectedModuleVersionId)
                {
                    throw new IOException(
                        $"Referenced {assembly.ManifestModule.Name} assembly does match the expected ModuleVersionId, most likely a incompatible version of Worlds Adrift is used. " +
                        $"Please refer to the WorldsAdriftReborn readme on how to obtain the correct version of the game.\n" +
                        $"(Provided ModuleVersionId: {moduleVersionId} does not match the expected ModuleVersionId: {expectedModuleVersionId})"
                    );
                }
                
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
