using Bossa.Travellers.UI;
using HarmonyLib;
using UnityEngine;

namespace WorldsAdriftReborn.Patching.Dynamic.InjectFallbackPatchnotes
{
    [HarmonyPatch(typeof(ChangeLogLoader))]
    internal class ChangeLogLoader_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ChangeLogLoader), "Start")]
        public static bool Start_Prefix(ChangeLogLoader __instance)
        {
            AccessTools.Method(typeof(ChangeLogLoader), "ParsePatchNotes").Invoke(__instance, new object[] { "someDummyShit" });
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ChangeLogLoader), "ParsePatchNotes")]
        public static bool ParsePatchNotes_Prefix(ChangeLogLoader __instance, string wwwText)
        {
            PatchNote patchNotePrefab = (PatchNote)AccessTools.Field(typeof(ChangeLogLoader), "patchNotePrefab").GetValue(__instance);
            Transform patchNotesParent = (Transform)AccessTools.Field(typeof(ChangeLogLoader), "patchNotesParent").GetValue(__instance);

            PatchNote patchNote = UnityEngine.Object.Instantiate<PatchNote>(patchNotePrefab, patchNotesParent);

            patchNote.version.text = "SpecialVersionBySp00ktober";
            patchNote.date.text = "30.08.2022";

            return false;
        }
    }
}
