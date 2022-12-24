using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using Bossa.Travellers.Weather;
using BossaECS.Core.Entity;
using HarmonyLib;
using Improbable.Entity.Component;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace WorldsAdriftReborn.Patching.SpatialOS.Debug_ComponentFactory
{
    /*
    * this is just to get a list of Component IDs mapped to their respective component type
    */
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
                        UnityEngine.Debug.LogWarning(componentFactory.ComponentId + " => " + componentFactory);
                        return 0;
                    }),
                    new CodeInstruction(OpCodes.Pop))
                .InstructionEnumeration();
        }
    }

    /*
     * this is just to add a breakpoint in the AddToIdComponentToEntityMapS::Execute() method
     */
    [HarmonyPatch()]
    internal class AddToIdComponentToEntityMapS_Transpiler
    {
        [HarmonyTargetMethod]
        public static MethodBase GetTargetMethod()
        {
            return AccessTools.Method(
                                        AccessTools.TypeByName("AddToIdComponentToEntityMapS`2").MakeGenericType(new System.Type[] {typeof(WASystems.Components.Weather.WeatherCellCoordsC), typeof(uint)}),
                                        "Execute");
        }

        delegate void debugIterator( object instance, WASystems.Components.Weather.WeatherCellCoordsC component, uint entityIndex);

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Execute_Transpiler(IEnumerable<CodeInstruction> instructions )
        {
            CodeMatcher matcher = new CodeMatcher(instructions)
                .MatchForward(true,
                    new CodeMatch(OpCodes.Nop),
                    new CodeMatch(OpCodes.Ldarg_0),
                    new CodeMatch(OpCodes.Ldfld),
                    new CodeMatch(OpCodes.Ldloc_2),
                    new CodeMatch(OpCodes.Ldloc_1),
                    new CodeMatch(i => i.opcode == OpCodes.Callvirt && ((MethodInfo)i.operand).Name == "Add"),
                    new CodeMatch(OpCodes.Nop))
                .InsertAndAdvance(new CodeInstruction(OpCodes.Ldarg_0))
                .InsertAndAdvance(new CodeInstruction(OpCodes.Ldloc_2))
                .InsertAndAdvance(new CodeInstruction(OpCodes.Ldloc_1))
                .InsertAndAdvance(Transpilers.EmitDelegate<debugIterator>(( object instance, WASystems.Components.Weather.WeatherCellCoordsC component, uint entityIndex) =>
                {
                    System.Type self = AccessTools.TypeByName("AddToIdComponentToEntityMapS`2").MakeGenericType(new System.Type[] { typeof(WASystems.Components.Weather.WeatherCellCoordsC), typeof(uint) });
                    EntityFilter filter = (EntityFilter)AccessTools.Field(self, "_filter").GetValue(instance);
                    EntityIterator iterator = filter.GetIterator();

                    UnityEngine.Debug.LogWarning("ADDING COMPONENT TO MAPPING: " + component + " => " + entityIndex);
                }));
            return matcher.InstructionEnumeration();
        }
    }
}
