using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Assets.Scripts.Physics;
using Assets.Scripts.Player;
using Bossa.Travellers.Controls;
using Bossa.Travellers.Creatures;
using Bossa.Travellers.Items;
using GameStateMachine;
using HarmonyLib;
using Improbable;
using Improbable.Worker;
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

                                    // the following should be set from server i think
                                    // but for now i set them here

                                    Bossa.Travellers.Player.HealthStateData hData = new Bossa.Travellers.Player.HealthStateData();
                                    hData.health = 100;
                                    hData.maxHealth = 200;
                                    hData.alive = true;
                                    hData.invulnerableTime = 0;
                                    hData.autoRecover = true;
                                    hData.entitiesToNotifyOfDeath = new Improbable.Collections.List<EntityId>();
                                    hData.impactDamageMultiplier = 1;
                                    hData.totalDamageMultiplier = 1;

                                    PilotStateData pData = new PilotStateData();
                                    pData.drivingEntityId = new Improbable.EntityId(0);
                                    pData.controlEntityId = new Improbable.EntityId(0);
                                    pData.controlType = ControlVehicleType.None;

                                    ShipHullAgentStateData sData = new ShipHullAgentStateData();
                                    sData.schematics = new Improbable.Collections.List<ShipHullSchematicData>();
                                    sData.editorId = new EntityId(0);

                                    Bossa.Travellers.Player.HealthState.Impl hImpl = new Bossa.Travellers.Player.HealthState.Impl();
                                    AccessTools.Field(typeof(Bossa.Travellers.Player.HealthState.Impl), "data").SetValue(hImpl, new Bossa.Travellers.Player.HealthState.Data(hData));

                                    Type tConnectionHandle = AccessTools.TypeByName("ConnectionHandle");
                                    Type tConnection = AccessTools.TypeByName("Connection");
                                    PilotState.Impl pImpl = new PilotState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new PilotState.Data(pData)); // wtf

                                    ShipHullAgentState.Impl sImpl = new ShipHullAgentState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new ShipHullAgentState.Data(sData));

                                    LocalPlayer.Instance.healthVisualiser._health = hImpl;

                                    PlayerExternalData eData = (PlayerExternalData)AccessTools.Field(typeof(PlayerMove), "externalData").GetValue(LocalPlayer.Instance.playerMove);

                                    Type tPlayerExternalDataVisualizer = AccessTools.TypeByName("PlayerExternalDataVisualizer");
                                    AccessTools.Field(tPlayerExternalDataVisualizer, "_pilotState").SetValue(eData, pImpl);
                                    AccessTools.Field(tPlayerExternalDataVisualizer, "_healthState").SetValue(eData, hImpl);
                                    AccessTools.Field(tPlayerExternalDataVisualizer, "_hullEditState").SetValue(eData, sImpl);

                                    AccessTools.Field(typeof(PlayerMove), "externalData").SetValue(LocalPlayer.Instance.playerMove, eData);

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
