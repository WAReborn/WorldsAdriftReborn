using System;
using System.Runtime.InteropServices;
using Bossa.Travellers.Alliance;
using Bossa.Travellers.Analytics;
using Bossa.Travellers.Clock;
using Bossa.Travellers.Controls;
using Bossa.Travellers.Craftingstation;
using Bossa.Travellers.Devconsole;
using Bossa.Travellers.Ecs;
using Bossa.Travellers.Interact;
using Bossa.Travellers.Inventory;
using Bossa.Travellers.Items;
using Bossa.Travellers.Loot;
using Bossa.Travellers.Misc;
using Bossa.Travellers.Player;
using Bossa.Travellers.Refdata;
using Bossa.Travellers.Rope;
using Bossa.Travellers.Scanning;
using Bossa.Travellers.Ship.Lock;
using Bossa.Travellers.Social;
using Bossa.Travellers.Weather;
using Bossa.Travellers.World;
using Improbable;
using Improbable.Collections;
using Improbable.Corelib.Metrics;
using Improbable.Corelib.Worker.Checkout;
using Improbable.Corelibrary.Activation;
using Improbable.Corelibrary.Math;
using Improbable.Corelibrary.Transforms;
using Improbable.Corelibrary.Transforms.Global;
using Improbable.Math;
using Improbable.Worker.Internal;
using WorldsAdriftRebornGameServer.DLLCommunication;
using WorldsAdriftRebornGameServer.Game.Items;
using WorldsAdriftRebornGameServer.Networking.Singleton;
using WorldsAdriftRebornGameServer.Networking.Wrapper;

namespace WorldsAdriftRebornGameServer.Game.Components
{
    internal class ComponentsSerializer
    {
        public unsafe static void InitAndSerialize(ENetPeerHandle player, long entityId, uint componentId, byte** buffer, uint* length)
        {
            for(int i = 0; i < ComponentsManager.Instance.ClientComponentVtables.Length; i++)
            {
                if (ComponentsManager.Instance.ClientComponentVtables[i].ComponentId == componentId)
                {
                    ulong refId = 0;
                    object obj = null;

                    if(componentId == 8065)
                    {
                        Blueprint.Data bData = new Blueprint.Data(new BlueprintData("Player"));
                        obj = bData;
                    }
                    else if(componentId == 190602)
                    {
                        TransformStateData tInit = new TransformStateData(new FixedPointVector3(new Improbable.Collections.List<long> { 0, 100, 0 }),
                                                                new Quaternion32(1),
                                                                null,
                                                                new Improbable.Math.Vector3d(0f, 0f, 0f),
                                                                new Improbable.Math.Vector3f(0f, 0f, 0f),
                                                                new Improbable.Math.Vector3f(0f, 0f, 0f),
                                                                false,
                                                                0f);
                        TransformState.Data tData = new TransformState.Data(tInit);

                        obj = tData;
                    }
                    else if(componentId == 190601)
                    {
                        TransformHierarchyState.Data thData = new TransformHierarchyState.Data(new TransformHierarchyStateData(new Improbable.Collections.List<Child> { }));

                        obj = thData;
                    }
                    else if(componentId == 1080)
                    {
                        SchematicsLearnerGSimState.Data schematicGsimData = new SchematicsLearnerGSimState.Data(new Improbable.Collections.List<string>(), new Map<string, string>(), false, new Improbable.Collections.List<string>());
                        obj = schematicGsimData;
                    }
                    else if(componentId == 1081)
                    {
                        InventoryState.Data iData = new InventoryState.Data(new InventoryStateData(100,
                                                                                        "{}",
                                                                                        ItemHelper.GetDefaultItems(),
                                                                                        ItemHelper.GetStashItems(true, true),
                                                                                        10,
                                                                                        18,
                                                                                        new Improbable.Collections.List<string> { },
                                                                                        true,
                                                                                        3));
                        obj = iData;
                    }
                    else if(componentId == 1086)
                    {
                        PlayerName.Data pData = new PlayerName.Data(new PlayerNameData("sp00ktober", "id", "cUid", "bossaToken", "bossaId"));

                        obj = pData;
                    }
                    else if (componentId == 1088)
                    {
                        PlayerPropertiesState.Data ppData = new PlayerPropertiesState.Data(new PlayerPropertiesStateData(new Map<string, string> { },
                                                                                                                new Map<string, string>
                                                                                                                {
                                                                                                                    {"Head", "hair_dreads" },
                                                                                                                    {"Body", "torso_ponchoVariantB" },
                                                                                                                    {"Feet", "legs_wrap" },
                                                                                                                    {"Face", "face_C" }
                                                                                                                },
                                                                                                                new Improbable.Collections.List<string> { },
                                                                                                                false));
                        obj = ppData;
                    }
                    else if(componentId == 1077)
                    {
                        HealthState.Data hData = new HealthState.Data(new HealthStateData(200, 200, true, 0f, true, new Improbable.Collections.List<EntityId> { }, 1f, 1f));

                        obj = hData;
                    }
                    else if(componentId == 1280)
                    {
                        WearableUtilsState.Data wData = new WearableUtilsState.Data(new WearableUtilsStateData(new Improbable.Collections.List<int> { }, new Improbable.Collections.List<float> { }, new Improbable.Collections.List<bool> { }));

                        obj = wData;
                    }
                    else if(componentId == 1211)
                    {
                        InteractAgentState.Data iaData = new InteractAgentState.Data(new InteractAgentStateData(true,
                                                                                                        new EntityId(0),
                                                                                                        new EntityId(0),
                                                                                                        new EntityId(0),
                                                                                                        new Improbable.Math.Vector3f(0f, 0f, 0f),
                                                                                                        new Improbable.Math.Coordinates(),
                                                                                                        1,
                                                                                                        1));
                        obj = iaData;
                    }
                    else if(componentId == 1212)
                    {
                        InteractAgentServerState.Data iasData = new InteractAgentServerState.Data(new InteractAgentServerStateData(new EntityId(0),
                                                                                                                        0,
                                                                                                                        1,
                                                                                                                        new EntityId(0),
                                                                                                                        new EntityId(0),
                                                                                                                        Bossa.Travellers.Items.MultitoolMode.Default,
                                                                                                                        new Option<ScalaSlottedInventoryItem> { }));
                        obj = iasData;
                    }
                    else if(componentId == 6924)
                    {
                        AllianceNameState.Data anData = new AllianceNameState.Data(new AllianceNameStateData("WA Alliance"));

                        obj = anData;
                    }
                    else if(componentId == 6925)
                    {
                        AllianceAndCrewWorkerState.Data acData = new AllianceAndCrewWorkerState.Data(new AllianceAndCrewWorkerStateData("alliance_invalid_uid", "crew_invalid_uid"));

                        obj = acData;
                    }
                    else if(componentId == 1082)
                    {
                        InventoryModificationState.Data imData = new InventoryModificationState.Data();

                        obj = imData;
                    }
                    else if(componentId == 1087)
                    {
                        PlayerPermissionsState.Data ppData = new PlayerPermissionsState.Data(Role.NonAdmin);

                        obj = ppData;
                    }
                    else if(componentId == 4444)
                    {
                        MountedGunShotState.Data mgData = new MountedGunShotState.Data();

                        obj = mgData;
                    }
                    else if(componentId == 1109)
                    {
                        PilotState.Data psData = new PilotState.Data(new PilotStateData(new EntityId(0), new EntityId(0), ControlVehicleType.None));

                        obj = psData;
                    }
                    else if(componentId == 1071)
                    {
                        BuilderServerState.Data bsData = new BuilderServerState.Data(new BuilderServerStateData(new EntityId(0)));

                        obj = bsData;
                    }
                    else if(componentId == 1131)
                    {
                        WorldData.Data woData = new WorldData.Data(new WorldDataData(new EntityId(0), 0.15f, 1f, 1));

                        obj = woData;
                    }
                    else if(componentId == 1098)
                    {
                        RopeControlPoints.Data rcData = new RopeControlPoints.Data(new RopeControlPointsData(new Improbable.Collections.List<Coordinates> { }, new Improbable.Collections.List<DynamicRopePoint> { }, false, 0f));

                        obj = rcData;
                    }
                    else if(componentId == 1207)
                    {
                        ShipHullAgentState.Data shData = new ShipHullAgentState.Data(new ShipHullAgentStateData(new Improbable.Collections.List<ShipHullSchematicData> { }, new EntityId(0)));

                        obj = shData;
                    }
                    else if(componentId == 2001)
                    {
                        PlayerAnalyticsState.Data paData = new PlayerAnalyticsState.Data(new PlayerAnalyticsStateData("someuser_id",
                                                                                                            "somesession_id",
                                                                                                            false,
                                                                                                            "unity",
                                                                                                            new Improbable.Collections.List<string> { },
                                                                                                            "defaultPayload",
                                                                                                            false));
                        obj = paData;
                    }
                    else if(componentId == 1332)
                    {
                        KnowledgeServerState.Data ksData = new KnowledgeServerState.Data(new KnowledgeServerStateData(1,
                                                                                                            new Map<string, int> { },
                                                                                                            1,
                                                                                                            new Map<string, int> { }));
                        obj = ksData;
                    }
                    else if(componentId == 1079)
                    {
                        SchematicsLearnerClientState.Data scData = new SchematicsLearnerClientState.Data(new SchematicsLearnerClientStateData(new Improbable.Collections.List<string> { },
                                                                                                                                    new Improbable.Collections.List<string> { },
                                                                                                                                    10,
                                                                                                                                    20,
                                                                                                                                    10,
                                                                                                                                    10));
                        obj = scData;
                    }
                    else if(componentId == 190002)
                    {
                        Activated.Data aData = new Activated.Data(new ActivatedData(true, true, 0));

                        obj = aData;
                    }
                    else if(componentId == 190000)
                    {
                        EntityLoadingControl.Data elData = new EntityLoadingControl.Data(new EntityLoadingControlData(EntityLoadingControlData.EntityLoadingStates.Idle,
                                                                                                            0,
                                                                                                            5,
                                                                                                            100,
                                                                                                            false,
                                                                                                            new Improbable.Collections.List<EntityId> { }));
                        obj = elData;
                    }
                    else if(componentId == 1150)
                    {
                        PlayerActivationState.Data pcData = new PlayerActivationState.Data(new PlayerActivationStateData(true, 12345, 123));

                        obj = pcData;
                    }
                    else if(componentId == 1219)
                    {
                        ShipyardVisitorState.Data svData = new ShipyardVisitorState.Data(new ShipyardVisitorStateData(new EntityId(0), "abcdefg"));

                        obj = svData;
                    }
                    else if(componentId == 1003)
                    {
                        PlayerCraftingInteractionState.Data pcisData = new PlayerCraftingInteractionState.Data(new EntityId(1), true);
                        
                        obj = pcisData;
                    }
                    else if(componentId == 1005)
                    {
                        CraftingStationClientState.Data csData = new CraftingStationClientState.Data(new CraftingStationClientStateData("schematicId",
                                                                                                                                "owner",
                                                                                                                                new Improbable.Collections.List<SlottedMaterial> { },
                                                                                                                                new Improbable.Collections.List<Cipher> { },
                                                                                                                                12,
                                                                                                                                30f,
                                                                                                                                new Option<PredictedStatDataExtra> { }));
                        obj = csData;
                    }
                    else if(componentId == 8055)
                    {
                        NewPlayerState.Data npData = new NewPlayerState.Data(new NewPlayerStateData(true));

                        obj = npData;
                    }
                    else if(componentId == 4329)
                    {
                        PlayerBuffState.Data pbData = new PlayerBuffState.Data(new PlayerBuffStateData(new Improbable.Collections.List<Buff> { }));

                        obj = pbData;
                    }
                    else if(componentId == 8060)
                    {
                        FeedbackListener.Data fbData = new FeedbackListener.Data();

                        obj = fbData;
                    }
                    else if(componentId == 1095)
                    {
                        FSimTimeState.Data fsData = new FSimTimeState.Data(new FSimTimeStateData(0.15f, "fsimId", 100));

                        obj = fsData;
                    }
                    else if(componentId == 190300)
                    {
                        ClientPhysicsLatency.Data cpData = new ClientPhysicsLatency.Data(new ClientPhysicsLatencyData(250, 100));

                        obj = cpData;
                    }
                    else if(componentId == 1006)
                    {
                        DevelopmentConsoleState.Data dcData = new DevelopmentConsoleState.Data(new DevelopmentConsoleStateData(100, 100, "gsimHostname", new Coordinates(0, 0, 0), "zone"));

                        obj = dcData;
                    }
                    else if(componentId == 1008)
                    {
                        FsimStatus.Data fData = new FsimStatus.Data(new FsimStatusData(60f, 1234, 150, "fsimEngineId", 123));

                        obj = fData;
                    }
                    else if(componentId == 1073)
                    {
                        ClientAuthoritativePlayerState.Data capData = new ClientAuthoritativePlayerState.Data(new ClientAuthoritativePlayerStateData(new Improbable.Math.Vector3f(0f, 100f, 0f),
                                                                                                                                            new Improbable.Corelib.Math.Quaternion(1, 1, 1, 1),
                                                                                                                                            new EntityId(2),
                                                                                                                                            1f,
                                                                                                                                            100,
                                                                                                                                            new byte[] { },
                                                                                                                                            false,
                                                                                                                                            2,
                                                                                                                                            false,
                                                                                                                                            false,
                                                                                                                                            100));
                        obj= capData;
                    }
                    else if(componentId == 9005)
                    {
                        SocialWorkerId.Data wiData = new SocialWorkerId.Data(new SocialWorkerIdData("workerId"));

                        obj = wiData;
                    }
                    else if(componentId == 1040)
                    {
                        GamePropertiesState.Data gpData = new GamePropertiesState.Data(new GamePropertiesStateData(new Map<string, string> { }));

                        obj = gpData;
                    }
                    else if(componentId == 6902)
                    {
                        GsimEventAuditState.Data gsData = new GsimEventAuditState.Data(new GsimEventAuditStateData(new Map<string, int> { }));

                        obj = gsData;
                    }
                    else if(componentId == 1269)
                    {
                        RadialStormState.Data rsData = new RadialStormState.Data(new RadialStormStateData(0f));

                        obj = rsData;
                    }
                    else if(componentId == 1139)
                    {
                        WeatherCellState.Data wcData = new WeatherCellState.Data(new WeatherCellStateData(1f, new Vector3f(0f, 00f, 00f)));

                        obj = wcData;
                    }
                    else if(componentId == 1254)
                    {
                        IslandLightningTimerState.Data ilData = new IslandLightningTimerState.Data(new IslandLightningTimerStateData(50 * 1000, // must be >= 30  and below must be > 0 to trigger lightning rumbles. multiply by 1000 to actually get the value you want ingame (50 in this case)
                                                                                                                            0, // must be 0 or you will set the island into a storm
                                                                                                                            1234,
                                                                                                                            1234,
                                                                                                                            false,
                                                                                                                            1,
                                                                                                                            new Improbable.Collections.List<EntityId> { new EntityId(2) }));
                        obj = ilData;
                    }
                    else if(componentId == 1041)
                    {
                        // todo: check how we could get correct values for this.
                        IslandState.Data data = new IslandState.Data(new IslandStateData("949069116@Island",
                                                                                            new Coordinates(0, 0, 0),
                                                                                            1f,
                                                                                            new Vector3f(0,0,0),
                                                                                            new Vector3f(100f, 100f, 100f),
                                                                                            new Option<string>("I Dont know who made this island :("),
                                                                                            false,
                                                                                            new Improbable.Collections.List<IslandDatabank>()
                                                                                            ));
                        obj = data;
                    }
                    else if(componentId == 1042)
                    {
                        // todo: check how we could get correct values for this.
                        IslandFabricState.Data data = new IslandFabricState.Data(new IslandFabricStateData(5,
                                                                                                           0,
                                                                                                           0,
                                                                                                           new Improbable.Collections.List<EntityId> { new EntityId(0) },
                                                                                                           new Option<EntityId>(new EntityId(0)),
                                                                                                           new Option<string>(""),
                                                                                                           Bossa.Travellers.Biomes.BiomeType.Biome1,
                                                                                                           false,
                                                                                                           new Option<Coordinates>(new Coordinates(0,0,0)),
                                                                                                           new Option<double>(0),
                                                                                                           new Option<double>(0)));
                        obj = data;
                    }
                    else if(componentId == 190604)
                    {
                        GlobalTransformState.Data data = new GlobalTransformState.Data(new GlobalTransformStateData(new Coordinates(0, 0, 0),
                                                                                                                        new Improbable.Corelib.Math.Quaternion(0, 0, 0, 0),
                                                                                                                        new Vector3d(0, 0, 0),
                                                                                                                        0));
                        obj = data;
                    }else if(componentId == 1240)  // reader
                    {
                        LorePiecesCollectorGsimState.Data loreGsimData = new LorePiecesCollectorGsimState.Data(new Improbable.Collections.List<string>());
                        obj = loreGsimData;
                    }
                    else if(componentId == 1241) // writer
                    {
                        LorePiecesCollectorClientState.Data loreClientData = new LorePiecesCollectorClientState.Data();
                        obj = loreClientData;
                    }
                    else if (componentId == 8051)
                    {
                        ToolState.Data toolData = new ToolState.Data(new ToolStateData(30));

                        obj = toolData;
                    }
                    else if (componentId == 8050)
                    {
                        ToolRequestState.Data toolRequestData = new ToolRequestState.Data(new ToolRequestStateData());

                        obj = toolRequestData;
                    }
                    
                    else if (componentId == 6908)
                    {
                        ReferenceDataRequestState.Data eeeData = new ReferenceDataRequestState.Data(new ReferenceDataRequestStateData());

                        obj = eeeData;
                    }
                    else if (componentId == 1097)
                    {
                        ReferenceDataState.Data referenceData = new ReferenceDataState.Data(new ReferenceDataStateData(
                            new EntityId(-1), 
                            "",
                            new Map<string, string>(),
                            new Map<string, string>(), 
                            "{}",
                            "{}",
                            "{}",
                            10, 
                            new Map<string, string>(),
                            true
                        ));
                    
                        obj = referenceData;
                    }
                    else if (componentId == 1260)
                    {
                        SchematicsUnlearnerState.Data susData = new SchematicsUnlearnerState.Data();

                        obj = susData;
                    }
                    else if(componentId == 1109)
                    {
                        PilotState.Data pd = new PilotState.Data(new PilotStateData(new EntityId(10), new EntityId(10), ControlVehicleType.None));

                        obj = pd;
                    }
                    else
                    {
                        Console.WriteLine("[ToDo] unhandled component id needs investigation: " + componentId);
                    }

                    if (obj != null)
                    {
                        refId = ClientObjects.Instance.CreateReference(obj);
                        ComponentProtocol.ClientObject wrapper = new ComponentProtocol.ClientObject();
                        wrapper.Reference = refId;

                        ComponentProtocol.ClientSerialize serialize = Marshal.GetDelegateForFunctionPointer<ComponentProtocol.ClientSerialize>(ComponentsManager.Instance.ClientComponentVtables[i].Serialize);
                        serialize(componentId, 2, &wrapper, buffer, length);

                        // store refId for player and component as we need this to access the component later
                        // this needs to change in the future, we need to make use of the games structures.
                        // noone wants to work with this triple dictionary >.>
                        if (!GameState.Instance.ComponentMap.ContainsKey(player))
                        {
                            GameState.Instance.ComponentMap.Add(player, new Dictionary<long, Dictionary<uint, ulong>> {
                                { entityId, new Dictionary<uint, ulong> {
                                    {
                                        componentId, refId
                                    }
                                } }
                            });
                        }
                        else
                        {
                            if (GameState.Instance.ComponentMap[player].ContainsKey(entityId))
                            {
                                if (GameState.Instance.ComponentMap[player][entityId].ContainsKey(componentId))
                                {
                                    // here we need to decide if we want to update the existing refId with the new one or drop the creation above.
                                    // this case should only happen if the same component is added multiple times to the same entityId and player
                                    GameState.Instance.ComponentMap[player][entityId][componentId] = refId;
                                }
                                else
                                {
                                    GameState.Instance.ComponentMap[player][entityId].Add(componentId, refId);
                                }
                            }
                            else
                            {
                                GameState.Instance.ComponentMap[player].Add(entityId, new Dictionary<uint, ulong> { { componentId, refId } });
                            }
                        }
                    }
                }
            }
        }
        public unsafe static void ComponentUpdateApply( ENetPeerHandle player, long entityId, uint componentId, byte* buffer, uint length )
        {
            for (int i = 0; i < ComponentsManager.Instance.ClientComponentVtables.Length; i++)
            {
                if (ComponentsManager.Instance.ClientComponentVtables[i].ComponentId == componentId)
                {
                    if(GameState.Instance.ComponentMap.ContainsKey(player) && GameState.Instance.ComponentMap[player].ContainsKey(entityId) && GameState.Instance.ComponentMap[player][entityId].ContainsKey(componentId))
                    {
                        ComponentProtocol.ClientObject* wrapper = ClientObjects.ObjectAlloc();
                        ComponentProtocol.ClientDeserialize deserialize = Marshal.GetDelegateForFunctionPointer<ComponentProtocol.ClientDeserialize>(ComponentsManager.Instance.ClientComponentVtables[i].Deserialize);
                        ComponentProtocol.ClientSerialize serialize = Marshal.GetDelegateForFunctionPointer<ComponentProtocol.ClientSerialize>(ComponentsManager.Instance.ClientComponentVtables[i].Serialize);

                        if (deserialize(componentId, 1, buffer, length, &wrapper))
                        {
                            // now we got a reference to the deserialized component, we can use it to update the component that we already have for the player.
                            object storedComponent = ClientObjects.Instance.Dereference(GameState.Instance.ComponentMap[player][entityId][componentId]);
                            object newComponent = ClientObjects.Instance.Dereference(wrapper->Reference);

                            if (storedComponent != null && newComponent != null)
                            {
                                Console.WriteLine("[success] wow, we should be able to do something now!");

                                // this is testing code, opening the inventory triggers this
                                if(componentId == 1003)
                                {
                                    // apply update
                                    ((PlayerCraftingInteractionState.Update)newComponent).ApplyTo((PlayerCraftingInteractionState.Data)storedComponent);

                                    // create update object from data
                                    PlayerCraftingInteractionState.Update storedUpdate = (PlayerCraftingInteractionState.Update)((PlayerCraftingInteractionState.Data)storedComponent).ToUpdate();

                                    SendOPHelper.SendComponentUpdateOp(player, entityId, new System.Collections.Generic.List<uint> { componentId }, new System.Collections.Generic.List<object> { storedUpdate });
                                }
                                else if(componentId == 1082)
                                {
                                    ((InventoryModificationState.Update)newComponent).ApplyTo((InventoryModificationState.Data)storedComponent);
                                    InventoryModificationState.Update storedUpdate = (InventoryModificationState.Update)((InventoryModificationState.Data)storedComponent).ToUpdate();

                                    for(int j = 0; j < ((InventoryModificationState.Update)newComponent).equipWearable.Count; j++)
                                    {
                                        Console.WriteLine("[info] game wants to equip a wearable");
                                        Console.WriteLine("[info] id: " + ((InventoryModificationState.Update)newComponent).equipWearable[j].itemId);
                                        Console.WriteLine("[info] slot: " + ((InventoryModificationState.Update)newComponent).equipWearable[j].slotId);
                                        Console.WriteLine("[info] lockbox: " + ((InventoryModificationState.Update)newComponent).equipWearable[j].isLockboxItem);

                                        // send updates to equip the wearables
                                        WearableUtilsState.Update storedWearableUtilsState = (WearableUtilsState.Update)((WearableUtilsState.Data)ClientObjects.Instance.Dereference(GameState.Instance.ComponentMap[player][entityId][1280])).ToUpdate();
                                        PlayerPropertiesState.Update storedPlayerPropertiesState = (PlayerPropertiesState.Update)((PlayerPropertiesState.Data)ClientObjects.Instance.Dereference(GameState.Instance.ComponentMap[player][entityId][1088])).ToUpdate();
                                        InventoryState.Update storedInventoryState = (InventoryState.Update)((InventoryState.Data)ClientObjects.Instance.Dereference(GameState.Instance.ComponentMap[player][entityId][1081])).ToUpdate();

                                        storedWearableUtilsState.SetItemIds(new Improbable.Collections.List<int> { ((InventoryModificationState.Update)newComponent).equipWearable[j].itemId }).SetHealths(new Improbable.Collections.List<float> { 100f }).SetActive(new Improbable.Collections.List<bool> { true });
                                        for(int k = 0; k < storedInventoryState.inventoryList.Value.Count; k++)
                                        {
                                            if (storedInventoryState.inventoryList.Value[k].itemId == ((InventoryModificationState.Update)newComponent).equipWearable[j].itemId)
                                            {
                                                ScalaSlottedInventoryItem modifiedItem = storedInventoryState.inventoryList.Value[k];
                                                //modifiedItem.slotType = ItemHelper.GetItem(storedInventoryState.inventoryList.Value[k].itemTypeId).characterslot;
                                                modifiedItem.slotType = "Utility"; // this tells the game that the glider is equipped
                                                Console.WriteLine("[debug] setting to " + modifiedItem.slotType);

                                                storedInventoryState.inventoryList.Value[k] = modifiedItem;
                                            }
                                        }

                                        // NOTE: its absolutely crucial to send 1081 before 1088, this is because 1081 sets the item slotType to something meaningfull while 1088 expects some meaningful value if it should be equipped
                                        SendOPHelper.SendComponentUpdateOp(player, entityId, new System.Collections.Generic.List<uint> { 1280, 1081, 1088 }, new System.Collections.Generic.List<object> { storedWearableUtilsState, storedInventoryState, storedPlayerPropertiesState });
                                    }
                                    for (int j = 0; j < ((InventoryModificationState.Update)newComponent).equipTool.Count; j++)
                                    {
                                        Console.WriteLine("[info] game wants to equip a tool");
                                        Console.WriteLine("[info] id: " + ((InventoryModificationState.Update)newComponent).equipTool[j].itemId);
                                    }
                                    for (int j = 0; j < ((InventoryModificationState.Update)newComponent).craftItem.Count; j++)
                                    {
                                        Console.WriteLine("[info] game wants to craft an item");
                                        Console.WriteLine("[info] inventoryEntityId: " + ((InventoryModificationState.Update)newComponent).craftItem[j].inventoryEntityId);
                                        Console.WriteLine("[info] itemTypeId: " + ((InventoryModificationState.Update)newComponent).craftItem[j].itemTypeId);
                                        Console.WriteLine("[info] amount: " + ((InventoryModificationState.Update)newComponent).craftItem[j].amount);
                                    }
                                    for (int j = 0; j < ((InventoryModificationState.Update)newComponent).crossInventoryMoveItem.Count; j++)
                                    {
                                        Console.WriteLine("[info] game wants to cross inventory move item");
                                        Console.WriteLine("[info] srcItemId: " + ((InventoryModificationState.Update)newComponent).crossInventoryMoveItem[j].srcItemId);
                                        Console.WriteLine("[info] xPos: " + ((InventoryModificationState.Update)newComponent).crossInventoryMoveItem[j].xPos);
                                        Console.WriteLine("[info] yPos: " + ((InventoryModificationState.Update)newComponent).crossInventoryMoveItem[j].yPos);
                                        Console.WriteLine("[info] rotate: " + ((InventoryModificationState.Update)newComponent).crossInventoryMoveItem[j].rotate);
                                        Console.WriteLine("[info] srcInventoryEntityId: " + ((InventoryModificationState.Update)newComponent).crossInventoryMoveItem[j].srcInventoryEntityId);
                                        Console.WriteLine("[info] destInventoryItemId: " + ((InventoryModificationState.Update)newComponent).crossInventoryMoveItem[j].destInventoryEntityId);
                                        Console.WriteLine("[info] isLockBoxItem: " + ((InventoryModificationState.Update)newComponent).crossInventoryMoveItem[j].isLockboxItem);
                                    }
                                    for (int j = 0; j < ((InventoryModificationState.Update)newComponent).moveItem.Count; j++)
                                    {
                                        Console.WriteLine("[info] game wants to move an inventory item");
                                        Console.WriteLine("[info] inventoryEntityId: " + ((InventoryModificationState.Update)newComponent).moveItem[j].inventoryEntityId);
                                        Console.WriteLine("[info] itemId: " + ((InventoryModificationState.Update)newComponent).moveItem[j].itemId);
                                        Console.WriteLine("[info] xPos: " + ((InventoryModificationState.Update)newComponent).moveItem[j].xPos);
                                        Console.WriteLine("[info] yPos: " + ((InventoryModificationState.Update)newComponent).moveItem[j].yPos);
                                        Console.WriteLine("[info] rotate: " + ((InventoryModificationState.Update)newComponent).moveItem[j].rotate);
                                        Console.WriteLine("[info] isLockboxItem: " + ((InventoryModificationState.Update)newComponent).moveItem[j].isLockboxItem);
                                    }
                                    for (int j = 0; j < ((InventoryModificationState.Update)newComponent).removeFromHotBar.Count; j++)
                                    {
                                        Console.WriteLine("[info] game wants to remove from hotbar");
                                        Console.WriteLine("[info] slotIndex: " + ((InventoryModificationState.Update)newComponent).removeFromHotBar[j].slotIndex);
                                        Console.WriteLine("[info] isLockboxItem: " + ((InventoryModificationState.Update)newComponent).removeFromHotBar[j].isLockboxItem);
                                    }
                                    for (int j = 0; j < ((InventoryModificationState.Update)newComponent).assignToHotBar.Count; j++)
                                    {
                                        Console.WriteLine("[info] game wants to assign to hotbar");
                                        Console.WriteLine("[info] itemId: " + ((InventoryModificationState.Update)newComponent).assignToHotBar[j].itemId);
                                        Console.WriteLine("[info] slotIndex: " + ((InventoryModificationState.Update)newComponent).assignToHotBar[j].slotIndex);
                                        Console.WriteLine("[info] isLockboxItem: " + ((InventoryModificationState.Update)newComponent).assignToHotBar[j].isLockboxItem);
                                    }

                                    SendOPHelper.SendComponentUpdateOp(player, entityId, new System.Collections.Generic.List<uint> { componentId }, new System.Collections.Generic.List<object> { storedUpdate });
                                }
                                else
                                {
                                    Console.WriteLine("[ToDO] unhandled ComponentUpdate for component " + componentId);
                                }
                            }

                            ClientObjects.Instance.DestroyReference(wrapper->Reference);
                        }
                        else
                        {
                            Console.WriteLine("[error] failed to deserialize component id " + componentId);
                        }

                        ClientObjects.ObjectFree(componentId, 1, wrapper);
                    }
                    else
                    {
                        Console.WriteLine("[warning] could not match requested ComponentUpdate with local stored values. (" + componentId + ")");
                    }

                    return;
                }
            }

            Console.WriteLine("[warning] component id " + componentId + " cannot be handled by ComponentUpdate implementation.");
        }
    }
}
