using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace WorldsAdriftReborn.Patching.SpatialOS
{
    internal class ConcurrentAssetPrecacher_Patch
    {
        [HarmonyPatch()]
        class StartPrecachingAsset
        {
            [HarmonyTargetMethod]
            public static MethodBase GetTargetMethod()
            {
                return AccessTools.Method(
                                            AccessTools.TypeByName("ConcurrentAssetPrecacher"),
                                            "StartPrecachingAsset");
            }

            [HarmonyTranspiler]
            public static IEnumerable<CodeInstruction> StartPrecachingAsset_Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                CodeMatcher matcher = new CodeMatcher(instructions)
                    .MatchForward(true,
                        new CodeMatch(OpCodes.Ldarg_0),
                        new CodeMatch(OpCodes.Ldfld, AccessTools.Field(AccessTools.TypeByName("ConcurrentAssetPrecacher"), "AssetsToPrecache")),
                        new CodeMatch(OpCodes.Ldarg_0),
                        new CodeMatch(OpCodes.Ldfld, AccessTools.Field(AccessTools.TypeByName("ConcurrentAssetPrecacher"), "nextAssetToPrecacheIndex")),
                        new CodeMatch(i => i.opcode == OpCodes.Callvirt && ((MethodInfo)i.operand).Name == "get_Item"))
                    .Advance(1)
                    .Insert(Transpilers.EmitDelegate<Func<string, string>>(( string assetToPrecache ) =>
                    {
                        Debug.LogWarning("PRECACHING: " + assetToPrecache);
                        return assetToPrecache;
                    }));

                return matcher.InstructionEnumeration();
            }
        }
    }
}
