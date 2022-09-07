using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace WorldsAdriftReborn.Patching.Ecs
{
    [HarmonyPatch(typeof(EcsBootstrap))]
    internal class EcsBootstrap_Patch
    {
        /*
         * As we plan to replace SpatialOS and SpatialOS.IsConnected is false at this point we need to skip the return in the check for it to proceed with initialization.
         * We cant just overwrite the getter of SpatialOS.IsConnected as it will break the game (you cant reach the main menu anymore)
         */
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(EcsBootstrap), "Init")]
        public static IEnumerable<CodeInstruction> Init_Transpiler(IEnumerable<CodeInstruction> instructions )
        {
            CodeMatcher matcher = new CodeMatcher(instructions)
                .MatchForward(false,
                    new CodeMatch(OpCodes.Ret))
                .Set(OpCodes.Nop, null);

            return matcher.InstructionEnumeration();
        }
    }
}
