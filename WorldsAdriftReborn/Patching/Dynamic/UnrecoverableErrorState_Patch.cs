using System;
using GameStateMachine;
using HarmonyLib;
using Travellers.UI.InfoPopups;
using UnityEngine;

namespace WorldsAdriftReborn.Patching.Dynamic
{
    [HarmonyPatch(typeof(UnrecoverableErrorState))]
    internal class UnrecoverableErrorState_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UnrecoverableErrorState.OnEnterState))]
        public static bool OnEnterState_Prefix(UnrecoverableErrorState __instance )
        {
            string title = NetworkExceptionHelpers.ExceptionMessageTitle((Exception)AccessTools.Field(typeof(UnrecoverableErrorState), "_exception").GetValue(__instance));
            string message = NetworkExceptionHelpers.ExceptionAsUserFacingError((Exception)AccessTools.Field(typeof(UnrecoverableErrorState), "_exception").GetValue(__instance));

            if(title == "Connection Error" && message == "Steam needs to be running.")
            {
                DialogPopupFacade.ShowOkDialog("This will be...", "a fun journey i guess :>", null, "CONTINUE", true, null);
                return false;
            }

            if(title == "Connection Error" && message.Contains("Sadly, all things must come to an end, and this is now true of Worlds Adrift."))
            {
                DialogPopupFacade.ShowOkDialog("Sadly...", "Nah forget that, you can continue :>", null, "CONTINUE", true, null);
                return false;
            }

            return true;
        }
    }
}
