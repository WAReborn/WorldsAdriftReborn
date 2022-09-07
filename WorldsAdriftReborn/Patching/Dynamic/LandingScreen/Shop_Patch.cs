using HarmonyLib;
using Travellers.UI.InfoPopups;

namespace WorldsAdriftReborn.Patching.Dynamic.LandingScreen
{
    [HarmonyPatch(typeof(Shop))]
    internal class Shop_Patch
    {
        private static string drawbacks = "Hello there!\n" +
                                    "As we are bypassing Steam we cannot open the overlay that would be here, sorry!\n";

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Shop.OpenShop))]
        public static bool OpenShop_Prefix()
        {
            DialogPopupFacade.ShowOkDialog("Drawbacks", drawbacks, null, "Got it :)", true, null);
            return false;
        }
    }
}
