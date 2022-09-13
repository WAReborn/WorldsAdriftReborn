using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Assets.Scripts.Physics;
using Assets.Scripts.Player;
using Assets.Visualizers;
using Bossa.Travellers.Alliance;
using Bossa.Travellers.Controls;
using Bossa.Travellers.Creatures;
using Bossa.Travellers.Inventory;
using Bossa.Travellers.Items;
using Bossa.Travellers.Player;
using Bossa.Travellers.Scanning;
using Bossa.Travellers.Visualisers;
using Bossa.Travellers.Visualisers.Profile;
using GameStateMachine;
using HarmonyLib;
using Improbable;
using Improbable.Collections;
using Improbable.Corelibrary.Activation;
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

                                    CharacterCustomisationVisualizer charCusVis = go2.GetComponent<CharacterCustomisationVisualizer>();

                                    // the following should be set from server i think
                                    // but for now i set them here

                                    // this is to fix NRE in PlayerExternalDataVisualizer in PlayerMove

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

                                    // this is to fix NRE in WADiscordRichPresence
                                    // this is a fix for InGameState regarding the inventory and HotBar

                                    InventoryStateData iData = new InventoryStateData();
                                    iData.updateSequence = 100;
                                    iData.jsonData = "";
                                    iData.inventoryList = new Improbable.Collections.List<ScalaSlottedInventoryItem>();
                                    iData.lockBoxItems = new Improbable.Collections.List<ScalaSlottedInventoryItem>();
                                    iData.width = 200;
                                    iData.height = 50;
                                    iData.allowedItems = new Improbable.Collections.List<string>();
                                    iData.hasBelt = false;
                                    iData.beltRow = 0;

                                    SchematicsLearnerClientStateData scData = new SchematicsLearnerClientStateData();
                                    scData.defaultSchematics = new Improbable.Collections.List<string>();
                                    scData.learnedSchematics = new Improbable.Collections.List<string>();
                                    scData.maxInventorySchematics = 5;
                                    scData.maxPhysicalSchematics = 10;
                                    scData.maxCookingSchematics = 15;
                                    scData.maxClothingSchematics = 20;

                                    KnowledgeServerStateData kData = new KnowledgeServerStateData();
                                    kData.knowledge = 7;
                                    kData.knowledgeNodeUses = new Improbable.Collections.Map<string, int>();
                                    kData.lifetimeKnowledge = 14;
                                    kData.cipherSlotCounts = new Map<string, int>();

                                    LorePiecesCollectorGsimStateData gData = new LorePiecesCollectorGsimStateData();
                                    gData.knownLore = new Improbable.Collections.List<string>();

                                    ToolStateData tData = new ToolStateData();
                                    tData.unlockedTools = 5;

                                    PlayerPropertiesStateData psData = new PlayerPropertiesStateData();
                                    psData.properties = new Map<string, string>();
                                    psData.customisation = new Map<string, string>();
                                    psData.ownedAppIds = new Improbable.Collections.List<string>();
                                    psData.observerCamMode = new Option<bool>();

                                    AllianceAndCrewWorkerStateData aData = new AllianceAndCrewWorkerStateData();
                                    aData.allianceUid = new Option<string>();
                                    aData.crewUid = new Option<string>();

                                    AllianceNameStateData asData = new AllianceNameStateData();
                                    asData.allianceName = new Option<string>();

                                    AllianceNameState.Impl asImpl = new AllianceNameState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new AllianceNameState.Data(asData));
                                    AllianceAndCrewWorkerState.Impl aImpl = new AllianceAndCrewWorkerState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new AllianceAndCrewWorkerState.Data(aData));
                                    
                                    InventoryState.Impl iImpl = new InventoryState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new InventoryState.Data(iData));
                                    SchematicsLearnerClientState.Impl scImpl = new SchematicsLearnerClientState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new SchematicsLearnerClientState.Data(scData));
                                    KnowledgeServerState.Impl kImpl = new KnowledgeServerState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new KnowledgeServerState.Data(kData));

                                    LorePiecesCollectorGsimState.Impl gImpl = new LorePiecesCollectorGsimState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new LorePiecesCollectorGsimState.Data(gData));

                                    ToolState.Impl tImpl = new ToolState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new ToolState.Data(tData));

                                    PlayerPropertiesState.Impl psImpl = new PlayerPropertiesState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new PlayerPropertiesState.Data(psData));

                                    Type tLpVisualizers = AccessTools.TypeByName("LocalPlayerVisualizers");
                                    object lpVisualisers = AccessTools.Field(typeof(LocalPlayer), "_visualizers").GetValue(LocalPlayer.Instance);
                                    PlayerNameVisualizer pNameVis = (PlayerNameVisualizer)AccessTools.Field(tLpVisualizers, "PlayerNameVisualiser").GetValue(lpVisualisers);
                                    InventoryVisualiser iVis = (InventoryVisualiser)AccessTools.Field(tLpVisualizers, "inventoryVisualiser").GetValue(lpVisualisers);
                                    ScanningAgentVisualizer sVis = (ScanningAgentVisualizer)AccessTools.Field(tLpVisualizers, "scanningAgentVisualizer").GetValue(lpVisualisers);
                                    LorePiecesCollectorVisualizer lVis = (LorePiecesCollectorVisualizer)AccessTools.Field(tLpVisualizers, "lorePiecesCollectorVisualizer").GetValue(lpVisualisers);
                                    ToolBehaviour tBeh = (ToolBehaviour)AccessTools.Field(tLpVisualizers, "toolBehaviour").GetValue(lpVisualisers);

                                    AccessTools.Field(typeof(PlayerNameVisualizer), "_allianceAndCrewWorkerState").SetValue(pNameVis, aImpl);
                                    AccessTools.Field(typeof(PlayerNameVisualizer), "_allianceNameState").SetValue(pNameVis, asImpl);

                                    AccessTools.Field(typeof(InventoryVisualiser), "_inventoryState").SetValue(iVis, iImpl);
                                    AccessTools.Field(typeof(InventoryVisualiser), "_schematicsState").SetValue(iVis, scImpl);
                                    AccessTools.Field(typeof(InventoryVisualiser), "_knowledgeState").SetValue(iVis, kImpl);

                                    AccessTools.Field(typeof(ScanningAgentVisualizer), "_state").SetValue(sVis, kImpl);

                                    AccessTools.Field(typeof(LorePiecesCollectorVisualizer), "_serverState").SetValue(lVis, gImpl);

                                    tBeh.ToolState = tImpl;

                                    AccessTools.Field(tLpVisualizers, "PlayerNameVisualiser").SetValue(lpVisualisers, pNameVis);
                                    AccessTools.Field(tLpVisualizers, "inventoryVisualiser").SetValue(lpVisualisers, iVis);

                                    AccessTools.Field(tLpVisualizers, "scanningAgentVisualizer").SetValue(lpVisualisers, sVis);

                                    AccessTools.Field(tLpVisualizers, "lorePiecesCollectorVisualizer").SetValue(lpVisualisers, lVis);

                                    AccessTools.Field(tLpVisualizers, "toolBehaviour").SetValue(lpVisualisers, tBeh);

                                    AccessTools.Field(typeof(LocalPlayer), "_visualizers").SetValue(LocalPlayer.Instance, lpVisualisers);

                                    AccessTools.Field(typeof(CharacterCustomisationVisualizer), "_properties").SetValue(charCusVis, psImpl);
                                    AccessTools.Field(typeof(CharacterCustomisationVisualizer), "_inventory").SetValue(charCusVis, iImpl);

                                    // initialize PlayerActivationVisualiser to pass PrepareForGameState.CanStart()

                                    PlayerActivationStateData paData = new PlayerActivationStateData();
                                    paData.playerActive = true;
                                    paData.lastActiveTime = 123;
                                    paData.lastDeactiveTime = 456;

                                    ActivatedData acData = new ActivatedData();
                                    acData.isActive = true;
                                    acData.activateRequested = true;
                                    acData.activationCallbacksPending = 0;

                                    PlayerActivationState.Impl paImpl = new PlayerActivationState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new PlayerActivationState.Data(paData));
                                    Activated.Impl acImpl = new Activated.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new Activated.Data(acData));

                                    AccessTools.Field(typeof(PlayerActivationVisualiser), "_playerActivationState").SetValue(LocalPlayer.Instance.activationVisualiser, paImpl);
                                    AccessTools.Field(typeof(PlayerActivationVisualiser), "_state").SetValue(LocalPlayer.Instance.activationVisualiser, acImpl);

                                    // call OnEnable() on behaviours that are ready
                                    go2.GetComponent<ToolBehaviour>().enabled = true;
                                    go2.GetComponent<InventoryVisualiser>().enabled = true;

                                    // probably part of PlayerMove
                                    //Component pedv = go2.GetComponent(tPlayerExternalDataVisualizer);
                                    //AccessTools.Field(tPlayerExternalDataVisualizer, "enabled").SetValue(pedv, true);

                                    charCusVis.enabled = true;

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
