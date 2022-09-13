using Assets.Scripts.Player;
using HarmonyLib;
using Improbable.Collections;
using Newtonsoft.Json.Linq;

namespace WorldsAdriftReborn.Patching.LoadInGame
{
    [HarmonyPatch(typeof(CharacterCustomisationVisualizer))]
    internal class CharacterCustomisationVisualizer_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(CharacterCustomisationVisualizer), "OnCustomisationUpdated")]
        public static void OnCustomisationUpdated_Prefix(ref Map<string, string> obj )
        {
            /*
             * this patch goes along the one in CharacterSelectionScreen_Patch.EnterWorld_Prefix
             * the character data is set there and in case we have UseBossaNet true we check if we need to load data locally to avoid nre
             * 
             * this is only temporary and for testing, should be implemented properly later
             */
            if (!obj.ContainsKey("bossaNetCharacterData"))
            {
                JObject o = (JObject)JToken.FromObject(CharacterDataLoader.Load().ToArray()[0]);
                obj.Add("bossaNetCharacterData", o.ToString());
            }
        }
    }
}
