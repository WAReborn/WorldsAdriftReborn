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
            // Adapted from: https://github.com/ManlyMarco/KK_GamepadSupport/blob/master/Core_GamepadSupport/GamepadSupportPlugin.cs
            try
            {
                // NEED to load the native dll BEFORE any class with DllImport is touched or the dll won't be found
                DependencyLoader.LoadDependencies();
            }
            catch (Exception ex)
            {
                Debug.LogError("WorldsAdriftReborn plugin failed to load: " + ex.Message);
                // TODO: We probably do not want to return here, but give some clearly defined error (perhaps even in the game itself if possible)
                return;

                //if (System.Windows.Forms.Application.MessageLoop)
                //{
                //    // WinForms app
                //    System.Windows.Forms.Application.Exit();
                //}
                //else
                //{
                //    // Console app
                //    System.Environment.Exit(1);
                //}
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
