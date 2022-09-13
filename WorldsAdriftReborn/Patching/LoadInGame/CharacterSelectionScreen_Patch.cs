using HarmonyLib;
using Improbable.Collections;
using Travellers.UI.Login;

namespace WorldsAdriftReborn.Patching.LoadInGame
{
    [HarmonyPatch(typeof(CharacterSelectionScreen))]
    internal class CharacterSelectionScreen_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(CharacterSelectionScreen.EnterWorld))]
        public static void EnterWorld_Prefix(CharacterSelectionScreen __instance)
        {
            /*
             * CharacterCustomisationVisualizer calls CharacterDataLoader.Load() in its OnEnable()
             * this in turn calls PlayerPrefs.GetString("WorldsAdrift.Preferences.CharacterCreationData") and expects a List<CharacterCreationData> (as JSON object)
             * as the game only stores the character data in there when you create a new character i make this prefix
             * 
             * later we probably want to set this server side and tell clients about the character data i guess, this is only for testing
             * 
             * NOTE: the game only tries to read data from here if WAConfig.Get<bool>(ConfigKeys.UseBossaNet) is false
             */
            List<CharacterCreationData> list = new List<CharacterCreationData>();
            LobbySystem lsys = (LobbySystem)AccessTools.Field(typeof(CharacterSelectionScreen), "_lobbySys").GetValue(__instance);

            list.Add(lsys.CurrentCreationData);
            CharacterDataLoader.Save(list);
        }
    }
}
