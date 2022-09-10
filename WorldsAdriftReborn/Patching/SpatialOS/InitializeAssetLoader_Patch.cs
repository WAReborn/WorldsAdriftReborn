using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Improbable.Unity.Assets;
using Improbable.Unity.Entity;
using UnityEngine;
using WorldsAdriftReborn.Config;

namespace WorldsAdriftReborn.Patching.SpatialOS
{
    [HarmonyPatch(typeof(DefaultTemplateProvider))]
    internal class InitializeAssetLoader_Patch
    {
        /*
         * the following patch forces the DefaultTemplateProvider to load assets from local files instead of trying to use the stream method.
         * The stream method produced some errors. Im not sure if it was trying to download from a server (seemed so) or trying to "stream" from some local files
         */
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(DefaultTemplateProvider), "InitializeAssetLoader")]
        public static IEnumerable<CodeInstruction> InitializeAssetLoader_Transpiler(IEnumerable<CodeInstruction> instructions )
        {
            CodeMatcher matcher = new CodeMatcher(instructions);

            // for some reason it starts at -1 so force 0
            matcher.Start()
                .MatchForward(false,
                    new CodeMatch(OpCodes.Ldarg_0),
                    new CodeMatch(i => i.opcode == OpCodes.Call && ((MethodInfo)i.operand).Name == "get_Configuration"),
                    new CodeMatch(OpCodes.Ldstr),
                    new CodeMatch(OpCodes.Ldarg_0),
                    new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(DefaultTemplateProvider), "UseLocalPrefabs")))
                .InsertAndAdvance(new CodeInstruction(OpCodes.Ldarg_0),
                    Transpilers.EmitDelegate<Func<bool>>(() =>
                    {
                        return true; // UseLocalPrefabs = true
                    }));
            for(int i = 0; i < 6; i++)
            {
                matcher.SetAndAdvance(OpCodes.Nop, null);
            }
            matcher.Advance(1)
                .InsertAndAdvance(new CodeInstruction(OpCodes.Ldarg_0),
                    Transpilers.EmitDelegate<Func<AssetDatabaseStrategy>>(() =>
                    {
                        return AssetDatabaseStrategy.Local; // LoadingStrategy = AssetDatabaseStrategy.Local
                    }));
            for(int i = 0; i < 5; i++)
            {
                matcher.SetAndAdvance(OpCodes.Nop, null);
            }
            matcher.Advance(1)
                .InsertAndAdvance(new CodeInstruction(OpCodes.Ldarg_0),
                    Transpilers.EmitDelegate<Func<string>>(() =>
                    {
                        ModSettings.modConfig.Reload();
                        return ModSettings.localAssetPath.Value; // LocalAssetDatabasePath = whatever is set in settings
                    }));
            for(int i = 0; i < 6; i++)
            {
                matcher.SetAndAdvance(OpCodes.Nop, null);
            }

            return matcher.InstructionEnumeration();
        }
    }
}
