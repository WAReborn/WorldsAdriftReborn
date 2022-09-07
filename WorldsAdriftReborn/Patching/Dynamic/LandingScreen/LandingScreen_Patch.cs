using HarmonyLib;
using Travellers.UI.InfoPopups;

namespace WorldsAdriftReborn.Patching.Dynamic.LandingScreen
{
    [HarmonyPatch(typeof(Travellers.UI.Login.LandingScreen))]
    internal class LandingScreen_Patch
    {
        private static string drawbacks =  "Hello there!\n" +
                                    "As we are bypassing Steam we cannot open the overlay that would be here, sorry!\n" +
                                    "You need to manually start the Island Creator from your Steam library.";

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Travellers.UI.Login.LandingScreen.OpenIslandCreator))]
        public static bool OpenIslandCreator()
        {
            DialogPopupFacade.ShowOkDialog("Drawbacks", drawbacks, null, "Got it :)", true, null);
            return false;
        }
    }
}
