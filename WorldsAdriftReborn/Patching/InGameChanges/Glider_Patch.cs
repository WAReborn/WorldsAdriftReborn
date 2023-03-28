using System.Reflection;
using Assets.Scripts.Player.Utilities;
using HarmonyLib;

namespace WorldsAdriftReborn.Patching.InGameChanges
{
    // gives glider infinite energy
    [HarmonyPatch(typeof(Glider))]
    internal class Glider_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Glider), "EvaluateControlState")]
        public static void EvaluateControlState_Prefix(Glider __instance, ref UserControlCharacter.State state )
        {
            AccessTools.Field(typeof(Glider), "energy").SetValue(__instance, 1f);
        }
    }
}
