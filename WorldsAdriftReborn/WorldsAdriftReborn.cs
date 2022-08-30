using System;
using BepInEx;
using System.Reflection;
using UnityEngine;
using Travellers.UI.InfoPopups;
using HarmonyLib;

namespace WorldsAdriftReborn
{
    [BepInPlugin("com.WAR.WorldsAdriftReborn", "WorldsAdriftReborn", "0.0.1")]
    internal class WorldsAdriftReborn : BaseUnityPlugin
    {
        private void Awake()
        {
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

                //string title = "test";
                //string message = "hueue";
                //DialogPopupFacade.ShowOkDialog(title, message, null, "NICE", true, null);
            }
            catch (Exception e)
            {
                Debug.Log("Unhandled exception occurred while patching the game: " + e);
            }
        }
    }
}
