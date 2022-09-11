using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Assets.Scripts.Physics;
using GameStateMachine;
using HarmonyLib;
using UnityEngine;

namespace WorldsAdriftReborn.Patching.LoadInGame
{
    public delegate void CreatePlayer();

    [HarmonyPatch(typeof(PrepareForGameState))]
    internal class PrepareForGameState_Patch
    {
        private static bool loaded = false;

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(PrepareForGameState.Update))]
        public static IEnumerable<CodeInstruction> Update_Transpiler(IEnumerable<CodeInstruction> instructions )
        {
            CodeMatcher matcher = new CodeMatcher(instructions)
                .MatchForward(false,
                    new CodeMatch(i => i.opcode == OpCodes.Call && ((MethodInfo)i.operand).Name == "CanStart"),
                    new CodeMatch(OpCodes.Brtrue),
                    new CodeMatch(OpCodes.Ret))
                .Advance(2)
                .Insert(Transpilers.EmitDelegate<CreatePlayer>(() =>
                {
                    if (!loaded)
                    {
                        Resources.LoadAll(""); // baaaaaad and takes a while, but then the object of the player gets loaded.

                        // player object does not get loaded into any valid scene so we need to deep search for it.
                        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                        {
                            if (go.name == "Traveller@Player_unityclient")
                            {
                                Debug.Log("FOUND PLAYER!");
                                GameObject go2 = GameObject.Instantiate(go);
                                LocalPlayerInit init = go2.GetComponent<LocalPlayerInit>();

                                if (init != null)
                                {
                                    Debug.Log("FOUND INIT");
                                    LocalPlayer.Instance.SetLocalPlayer(init);
                                    Debug.Log("SETTING PLAYER ACTIVE"); // maybe not needed
                                    go2.SetActive(true);
                                    break;
                                }
                            }
                        }
                        Debug.Log("DONE");
                        loaded = true;
                    }
                }));

            return matcher.InstructionEnumeration();
        }
    }
}
