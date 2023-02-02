using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Bossa.Travellers.Player;
using HarmonyLib;
using Improbable;
using Improbable.Entity.Component;
using Improbable.Unity.Internal;
using Improbable.Unity.Visualizer;
using Improbable.Util.Injection;
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
    }

    // this is just a test to manually set required fields in ToolBeaviour when they are needed to pass a check to get it enabled
    [HarmonyPatch(typeof(EntityVisualizers))]
    internal class EntityVisualizers_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(EntityVisualizers), "UpdateActivation")]
        public static void UpdateActivation_Prefix(EntityVisualizers __instance, MonoBehaviour visualizer )
        {
            if(visualizer.GetType() == typeof(ToolBehaviour))
            {
                // reflection hell and very broken code incomming but it might make the inventory work
                // however this is brute force and we should find out how the game usually does this
                System.Type tConnectionHandle = AccessTools.TypeByName("ConnectionHandle");
                System.Type tConnection = AccessTools.TypeByName("Connection");

                ToolState.Impl impl1 = new ToolState.Impl((Improbable.Worker.Connection)AccessTools.Constructor(tConnection, new System.Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new ToolState.Data(3));
                ToolRequestState.Impl impl2 = new ToolRequestState.Impl((Improbable.Worker.Connection)AccessTools.Constructor(tConnection, new System.Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new ToolRequestState.Data());

                IMemberAdapter impl1FieldInfo = VisualizerMetadataLookup.Instance.GetFieldInfo(impl1.GetType(), visualizer.GetType());
                IMemberAdapter impl2FieldInfo = VisualizerMetadataLookup.Instance.GetFieldInfo(impl2.GetType(), visualizer.GetType());

                if(impl1FieldInfo != null && impl2FieldInfo != null)
                {
                    AccessTools.Method(typeof(EntityVisualizers), "InjectField").Invoke(__instance, new object[] { visualizer, impl1FieldInfo, impl1});
                    AccessTools.Method(typeof(EntityVisualizers), "InjectField").Invoke(__instance, new object[] { visualizer, impl2FieldInfo, impl2 });
                }
            }
        }
    }
}
