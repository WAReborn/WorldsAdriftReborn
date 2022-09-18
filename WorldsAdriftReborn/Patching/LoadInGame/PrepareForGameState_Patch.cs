using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Assets.Scripts.Physics;
using Assets.Scripts.Player;
using Assets.Scripts.Visualisers.Items;
using Assets.Visualizers;
using Bossa.Prototype.Character.Observer;
using Bossa.Travellers.Alliance;
using Bossa.Travellers.Controls;
using Bossa.Travellers.Creatures;
using Bossa.Travellers.Interact;
using Bossa.Travellers.Inventory;
using Bossa.Travellers.Items;
using Bossa.Travellers.Player;
using Bossa.Travellers.Scanning;
using Bossa.Travellers.Social;
using Bossa.Travellers.Utilityslot;
using Bossa.Travellers.Visualisers;
using Bossa.Travellers.Visualisers.Customisation;
using Bossa.Travellers.Visualisers.Player;
using Bossa.Travellers.Visualisers.Profile;
using Bossa.Travellers.World;
using GameStateMachine;
using HarmonyLib;
using Improbable;
using Improbable.Collections;
using Improbable.Corelibrary.Activation;
using Improbable.Worker;
using Newtonsoft.Json.Linq;
using Travellers.UI.Login;
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
                        loaded = true;
                        return;

                        Resources.LoadAll(""); // baaaaaad and takes a while, but then the object of the player gets loaded.

                        // player object does not get loaded into any valid scene so we need to deep search for it.
                        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                        {
                            if (go.name == "Traveller@Player_unityclient")
                            {
                                Debug.Log("FOUND PLAYER!");
                                GameObject go2 = GameObject.Instantiate(go, new Vector3(0,0,0), new Quaternion(0,0,0,0));
                                LocalPlayerInit init = go2.GetComponent<LocalPlayerInit>();

                                if (init != null)
                                {
                                    Debug.Log("FOUND INIT");
                                    //LocalPlayer.Instance.SetLocalPlayer(init);
                                    init.enabled = true;

                                    CharacterCustomisationVisualizer charCusVis = go2.GetComponent<CharacterCustomisationVisualizer>();

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

                                    // get player name from character selection screen, see CharacterSelectionScreen_Patch.EnterWorld_Prefix
                                    CharacterCreationData charData = CharacterDataLoader.Load().ToArray()[0];

                                    // creating data structs

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

                                    PlayerPropertiesClientStateData ppcData = new PlayerPropertiesClientStateData();

                                    WearableUtilsStateData wData = new WearableUtilsStateData();
                                    wData.itemIds = new Improbable.Collections.List<int>();
                                    wData.healths = new Improbable.Collections.List<float>();
                                    wData.active = new Improbable.Collections.List<bool>();

                                    PlayerNameData pnData = new PlayerNameData();
                                    pnData.name = charData.Name;
                                    pnData.playerId = charData.Id.ToString();
                                    pnData.characterUid = charData.characterUid;
                                    pnData.bossaNetGameClientToken = "bossaTokenABC";
                                    pnData.bossaId = "bossaIDABC";

                                    InteractAgentStateData iaData = new InteractAgentStateData();
                                    iaData.useItemKeyHeld = true;
                                    iaData.lookingAt = new EntityId(0);
                                    iaData.lookingAtInteractive = new EntityId(0);
                                    iaData.debugLookingAt = new EntityId(0);
                                    iaData.lookDirectionEuler = Improbable.Math.Vector3f.ZERO;
                                    iaData.lookHitPoint = new Improbable.Math.Coordinates();
                                    iaData.itemSlot = 1;
                                    iaData.selectedHotbar = 1;

                                    InteractAgentServerStateData iasData = new InteractAgentServerStateData();
                                    iasData.equipId = new EntityId(0);
                                    iasData.equipInventoryId = 0;
                                    iasData.aimType = 1;
                                    iasData.forcedInteract = new EntityId(0);
                                    iasData.exclusivelyUsingEntityId = new EntityId(0);
                                    iasData.multitoolMode = MultitoolMode.Default;
                                    iasData.selectedHotbarItem = new Option<ScalaSlottedInventoryItem>();

                                    UtilitySlotActivatedStateData uData = new UtilitySlotActivatedStateData();
                                    uData.head = true;
                                    uData.body = true;
                                    uData.feet = true;
                                    uData.headTotalHealth = new Option<float>();
                                    uData.headCurrentHealth = new Option<float>();
                                    uData.bodyTotalHealth = new Option<float>();
                                    uData.bodyCurrentHealth = new Option<float>();
                                    uData.feetTotalHealth = new Option<float>();
                                    uData.feetCurrentHealth = new Option<float>();

                                    WorldDataData woData = new WorldDataData();
                                    woData.timeKeeperEntityId = new EntityId(0);
                                    woData.time = 0f;
                                    woData.timeRate = 1f;
                                    woData.days = 3;

                                    ChatListenerData cData = new ChatListenerData();
                                    cData.owner = "chatOwner";

                                    NewChatListenerData ncData = new NewChatListenerData();

                                    AllianceAndCrewWorkerStateData aData = new AllianceAndCrewWorkerStateData();
                                    aData.allianceUid = new Option<string>();
                                    aData.crewUid = new Option<string>();

                                    AllianceNameStateData asData = new AllianceNameStateData();
                                    asData.allianceName = new Option<string>();

                                    // creating implementations

                                    AllianceNameState.Impl asImpl = new AllianceNameState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new AllianceNameState.Data(asData));
                                    AllianceAndCrewWorkerState.Impl aImpl = new AllianceAndCrewWorkerState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new AllianceAndCrewWorkerState.Data(aData));
                                    PlayerName.Impl pnImpl = new PlayerName.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new PlayerName.Data(pnData));

                                    InventoryState.Impl iImpl = new InventoryState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new InventoryState.Data(iData));
                                    SchematicsLearnerClientState.Impl scImpl = new SchematicsLearnerClientState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new SchematicsLearnerClientState.Data(scData));
                                    KnowledgeServerState.Impl kImpl = new KnowledgeServerState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new KnowledgeServerState.Data(kData));

                                    LorePiecesCollectorGsimState.Impl gImpl = new LorePiecesCollectorGsimState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new LorePiecesCollectorGsimState.Data(gData));

                                    ToolState.Impl tImpl = new ToolState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new ToolState.Data(tData));

                                    PlayerPropertiesState.Impl psImpl = new PlayerPropertiesState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new PlayerPropertiesState.Data(psData));

                                    PlayerPropertiesClientState.Impl ppcImpl = new PlayerPropertiesClientState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new PlayerPropertiesClientState.Data(ppcData));

                                    WearableUtilsState.Impl wImpl = new WearableUtilsState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new WearableUtilsState.Data(wData));

                                    InteractAgentState.Impl iaImpl = new InteractAgentState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new InteractAgentState.Data(iaData));

                                    InteractAgentServerState.Impl iasImpl = new InteractAgentServerState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new InteractAgentServerState.Data(iasData));

                                    UtilitySlotActivatedState.Impl uImpl = new UtilitySlotActivatedState.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new UtilitySlotActivatedState.Data(uData));

                                    WorldData.Impl woImpl = new WorldData.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new WorldData.Data(woData));

                                    ChatListener.Impl cImpl = new ChatListener.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new ChatListener.Data(cData));
                                    NewChatListener.Impl ncImpl = new NewChatListener.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new NewChatListener.Data(ncData));

                                    // getting visualisers to set values in

                                    Type tLpVisualizers = AccessTools.TypeByName("LocalPlayerVisualizers");
                                    Type tChatVisualizer = AccessTools.TypeByName("ChatVisualizer");
                                    Type tPlayerLookingAt = AccessTools.TypeByName("PlayerLookingAt");
                                    object lpVisualisers = AccessTools.Field(typeof(LocalPlayer), "_visualizers").GetValue(LocalPlayer.Instance);
                                    PlayerNameVisualizer pNameVis = (PlayerNameVisualizer)AccessTools.Field(tLpVisualizers, "PlayerNameVisualiser").GetValue(lpVisualisers);
                                    InventoryVisualiser iVis = (InventoryVisualiser)AccessTools.Field(tLpVisualizers, "inventoryVisualiser").GetValue(lpVisualisers);
                                    ScanningAgentVisualizer sVis = (ScanningAgentVisualizer)AccessTools.Field(tLpVisualizers, "scanningAgentVisualizer").GetValue(lpVisualisers);
                                    LorePiecesCollectorVisualizer lVis = (LorePiecesCollectorVisualizer)AccessTools.Field(tLpVisualizers, "lorePiecesCollectorVisualizer").GetValue(lpVisualisers);
                                    ToolBehaviour tBeh = (ToolBehaviour)AccessTools.Field(tLpVisualizers, "toolBehaviour").GetValue(lpVisualisers);
                                    PlayerPropertiesVisualiser ppVis = (PlayerPropertiesVisualiser)AccessTools.Field(tLpVisualizers, "playerProperties").GetValue(lpVisualisers);
                                    GearWearablesVisualizer gVis = go2.GetComponent<GearWearablesVisualizer>();
                                    PlayerEquipmentVisualizer peVis = (PlayerEquipmentVisualizer)AccessTools.Field(tLpVisualizers, "equipmentVisualizer").GetValue(lpVisualisers);
                                    LeftArmAimingVisualizer laVis = (LeftArmAimingVisualizer)AccessTools.Field(tLpVisualizers, "leftArmAimingVisualizer").GetValue (lpVisualisers);
                                    UtilitySlotActivatedBehaviour uBeh = go2.GetComponent<UtilitySlotActivatedBehaviour>();
                                    PlayerHotbarVisualizer hVis = (PlayerHotbarVisualizer)AccessTools.Field(tLpVisualizers, "hotbarVisualiser").GetValue(lpVisualisers);
                                    InteractAgentObserver iObs = (InteractAgentObserver)AccessTools.Field(tLpVisualizers, "interactAgentObserver").GetValue(lpVisualisers);
                                    WorldStateVisualizer woVis = go2.GetComponent<WorldStateVisualizer>();
                                    object cVis = go2.GetComponent(tChatVisualizer);
                                    object plBeh = go2.GetComponent(tPlayerLookingAt);
                                    InteractAgentVisualiser iaVis = (InteractAgentVisualiser)AccessTools.Field(tPlayerLookingAt, "interactAgentVisualiser").GetValue(plBeh);

                                    // setting values in visualisers

                                    AccessTools.Field(typeof(PlayerNameVisualizer), "_allianceAndCrewWorkerState").SetValue(pNameVis, aImpl);
                                    AccessTools.Field(typeof(PlayerNameVisualizer), "_allianceNameState").SetValue(pNameVis, asImpl);
                                    AccessTools.Field(typeof(PlayerNameVisualizer), "_name").SetValue(pNameVis, pnImpl);

                                    AccessTools.Field(typeof(InventoryVisualiser), "_inventoryState").SetValue(iVis, iImpl);
                                    AccessTools.Field(typeof(InventoryVisualiser), "_schematicsState").SetValue(iVis, scImpl);
                                    AccessTools.Field(typeof(InventoryVisualiser), "_knowledgeState").SetValue(iVis, kImpl);

                                    AccessTools.Field(typeof(ScanningAgentVisualizer), "_state").SetValue(sVis, kImpl);

                                    AccessTools.Field(typeof(LorePiecesCollectorVisualizer), "_serverState").SetValue(lVis, gImpl);

                                    tBeh.ToolState = tImpl;

                                    AccessTools.Field(typeof(PlayerPropertiesVisualiser), "playerProps").SetValue(ppVis, psImpl);
                                    AccessTools.Field(typeof(PlayerPropertiesVisualiser), "_propWriter").SetValue(ppVis, ppcImpl);

                                    AccessTools.Field(typeof(GearWearablesVisualizer), "_wearableUtilsState").SetValue(gVis, wImpl);
                                    AccessTools.Field(typeof(GearWearablesVisualizer), "_inventoryState").SetValue(gVis, iImpl);

                                    AccessTools.Field(typeof(PlayerEquipmentVisualizer), "_interactState").SetValue(peVis, iaImpl);

                                    AccessTools.Field(typeof(LeftArmAimingVisualizer), "_serverState").SetValue(laVis, iasImpl);
                                    AccessTools.Field(typeof(LeftArmAimingVisualizer), "_state").SetValue(laVis, iaImpl);

                                    AccessTools.Field(typeof(UtilitySlotActivatedBehaviour), "_state").SetValue(uBeh, uImpl);

                                    AccessTools.Field(typeof(PlayerHotbarVisualizer), "_interactClientState").SetValue(hVis, iaImpl);

                                    AccessTools.Field(typeof(InteractAgentObserver), "_properties").SetValue(iObs, psImpl);
                                    AccessTools.Field(typeof(InteractAgentObserver), "_serverState").SetValue(iObs, iasImpl);
                                    AccessTools.Field(typeof(InteractAgentObserver), "interactWriter").SetValue(iObs, iaImpl);

                                    AccessTools.Field(typeof(WorldStateVisualizer), "_worldData").SetValue(woVis, woImpl);

                                    AccessTools.Field(tChatVisualizer, "_listener").SetValue(cVis, cImpl);
                                    AccessTools.Field(tChatVisualizer, "_newChatListener").SetValue(cVis, ncImpl);

                                    AccessTools.Field(typeof(InteractAgentVisualiser), "clientState").SetValue(iaVis, iaImpl);
                                    AccessTools.Field(typeof(InteractAgentVisualiser), "serverState").SetValue(iaVis, iasImpl);
                                    AccessTools.Field(tPlayerLookingAt, "targettingPlayer").SetValue(plBeh, pNameVis);

                                    // setting visualizers back in LocalPlayerVisualisers

                                    AccessTools.Field(tLpVisualizers, "PlayerNameVisualiser").SetValue(lpVisualisers, pNameVis);
                                    AccessTools.Field(tLpVisualizers, "inventoryVisualiser").SetValue(lpVisualisers, iVis);
                                    AccessTools.Field(tLpVisualizers, "scanningAgentVisualizer").SetValue(lpVisualisers, sVis);
                                    AccessTools.Field(tLpVisualizers, "lorePiecesCollectorVisualizer").SetValue(lpVisualisers, lVis);
                                    AccessTools.Field(tLpVisualizers, "toolBehaviour").SetValue(lpVisualisers, tBeh);
                                    AccessTools.Field(tLpVisualizers, "playerProperties").SetValue(lpVisualisers, ppVis);
                                    AccessTools.Field(tLpVisualizers, "equipmentVisualizer").SetValue(lpVisualisers, peVis);
                                    AccessTools.Field(tLpVisualizers, "leftArmAimingVisualizer").SetValue(lpVisualisers, laVis);
                                    AccessTools.Field(tLpVisualizers, "hotbarVisualiser").SetValue(lpVisualisers, hVis);

                                    AccessTools.Field(typeof(LocalPlayer), "_visualizers").SetValue(LocalPlayer.Instance, lpVisualisers);

                                    AccessTools.Field(typeof(CharacterCustomisationVisualizer), "_properties").SetValue(charCusVis, psImpl);
                                    AccessTools.Field(typeof(CharacterCustomisationVisualizer), "_inventory").SetValue(charCusVis, iImpl);

                                    AccessTools.Field(tPlayerLookingAt, "interactAgentVisualiser").SetValue(plBeh, iaVis);

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
                                    object pedv = go2.GetComponent(tPlayerExternalDataVisualizer);
                                    ((Behaviour)pedv).enabled = true;
                                    charCusVis.enabled = true;
                                    go2.GetComponent<PlayerPropertiesVisualiser>().enabled = true;
                                    gVis.enabled = true;
                                    go2.GetComponent<PlayerNameVisualizer>().enabled = true;
                                    go2.GetComponent<PlayerEquipmentVisualizer>().enabled = true;
                                    go2.GetComponent<PlayerGenderVisualizer>().enabled = true;
                                    go2.GetComponent<CustomisationSenderVisualiser>().enabled = true;
                                    go2.GetComponent<LeftArmAimingVisualizer>().enabled = true;
                                    uBeh.enabled = true;
                                    go2.GetComponent<InteractAgentVisualiser>().enabled = true;
                                    go2.GetComponent<HealthUIVisualizer>().enabled = true;
                                    go2.GetComponent<PlayerHotbarVisualizer>().enabled = true;
                                    go2.GetComponent<InteractAgentObserver>().enabled = true;
                                    woVis.enabled = true;
                                    ((Behaviour)cVis).enabled = true;
                                    go2.GetComponent<PlayerActivationVisualiser>().enabled = true; // this fades out of loading screen

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
