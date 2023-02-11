using System.Collections.Generic;
using HarmonyLib;
using Travellers.UI.Framework;
using Travellers.UI.InfoPopups;
using UnityEngine;

namespace WorldsAdriftReborn.Patching.LandingScreen
{
    [HarmonyPatch(typeof(LandingScreenState))]
    internal class LandingScreenState_Patch
    {
        private static void Close() => Application.Quit();
        private static void SkipYes() => PlayerPrefs.SetInt("skip", 1);
        private static void SkipNo() => PlayerPrefs.SetInt("skip", 0);
        
        [HarmonyPostfix]
        [HarmonyPatch(typeof(LandingScreenState), "CreateScreens")]
        public static void CreateScreens_Postfix( Dictionary<TypeCast, UIScreen> screenLookup )
        {
            if (!PlayerPrefs.HasKey("skip")) PlayerPrefs.SetInt("skip", -1);
            var pref = PlayerPrefs.GetInt("skip");
            if (pref == 1) return;

            DialogPopupFacade.ShowConfirmationDialog("Disclaimer", "Welcome!\nThis is a community made mod by a few devs in their spare time. We are NOT associated in any way with Bossa, all rights one the game and its assets belong to them.\nWe do replace SpatialOS with our own code by reverse engineering the Game. This is needed as it was a proprietary framework that cant be reused.\n\nEnough of that, you can read more about the project state and internals on our GitHub page or join our Discord if you got any questions left or just want to talk.\nHave fun :)", null, "Wohoo!", "Exit", Close, true, 3f);
            if (pref == -1) DialogPopupFacade.ShowConfirmationDialog("Disclaimer", "Skip this welcome message in the future?", SkipYes, "Yes", "No", SkipNo, true, 1f);
        }
    }
}
