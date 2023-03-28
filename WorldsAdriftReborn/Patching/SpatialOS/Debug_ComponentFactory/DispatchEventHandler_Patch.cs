using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Bossa.Travellers.Inventory;
using Bossa.Travellers.Misc;
using Bossa.Travellers.Player;
using Bossa.Travellers.Scanning;
using Bossa.Travellers.Utils;
using Bossa.Travellers.Visualisers.Profile;
using HarmonyLib;
using Improbable;
using Improbable.Collections;
using Improbable.Entity.Component;
using Improbable.Unity.Internal;
using Improbable.Unity.Visualizer;
using Improbable.Util.Injection;
using Improbable.Worker;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

/*
 * this is just to get a list of Component IDs mapped to their respective component type
 */
namespace WorldsAdriftReborn.Patching.SpatialOS.Debug_ComponentFactory
{
    [HarmonyPatch()]
    internal class DispatchEventHandler_Patch
    {
        [HarmonyTargetMethod]
        public static MethodBase GetTargetMethod()
        {
            return AccessTools.Method(
                                        AccessTools.TypeByName("DispatchEventHandler"),
                                        "RegisterComponentFactories");
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> RegisterComponentFactories_Transpiler(IEnumerable<CodeInstruction> instructions )
        {
            return new CodeMatcher(instructions)
                .MatchForward(true,
                    new CodeMatch(i => i.opcode == OpCodes.Callvirt && ((MethodInfo)i.operand).Name == "RegisterWithConnection"))
                .Insert(
                    new CodeInstruction(OpCodes.Ldloc_2),
                    Transpilers.EmitDelegate<Func<IComponentFactory, int>>(( componentFactory ) =>
                    {
                        Debug.LogWarning(componentFactory.ComponentId + " => " + componentFactory);
                        return 0;
                    }),
                    new CodeInstruction(OpCodes.Pop))
                .InstructionEnumeration();
        }

        // this is just a debug patch to serialize a SchematicData json
        [HarmonyPostfix]
        public static void RegisterComponentFactories_Postfix()
        {
            SchematicData data = new SchematicData();

            data.amountToCraft = 1;
            data.baseHp = 100f;
            data.baseStats = new Dictionary<string, float>();
            data.category = "Personal";
            data.cipherSlots = new System.Collections.Generic.List<Map<string, string>>();
            data.craftingRequirements = new CraftingItemData[0];
            data.description = "wolo";
            data.hullData = "hullData";
            data.iconId = "crafted items/3x4_glider";
            data.itemType = "hmm";
            data.modules = new Map<string, string>();
            data.rarity = 1;
            data.referenceData = "glider";
            data.schematicId = "glider";
            data.SchematicType = SchematicType.Fixed;
            data.timeToCraft = 10;
            data.title = "cool glider";
            data.unlearnable = false;
            data.uUID = "glider";

            Dictionary<string, SchematicData> dict = new Dictionary<string, SchematicData>();
            dict.Add("glider", data);

            Debug.Log("HERE IT COMES");
            Debug.Log(JsonConvert.SerializeObject(dict));
        }
    }
}
