using Bossa.Travellers.UI;
using HarmonyLib;
using Improbable.Collections;
using Improbable.Worker.Internal;
using Improbable;
using UnityEngine;
using System;
using Bossa.Travellers.Ecs;
using Improbable.Worker;
using Improbable.Corelibrary.Transforms;
using Improbable.Corelibrary.Math;
using Bossa.Travellers.Inventory;
using Bossa.Travellers.Player;
using Bossa.Travellers.Interact;
using Bossa.Travellers.Alliance;
using Bossa.Travellers.Controls;
using Bossa.Travellers.World;
using Bossa.Travellers.Rope;
using Improbable.Math;
using Bossa.Travellers.Items;
using Bossa.Travellers.Analytics;
using Bossa.Travellers.Scanning;
using Improbable.Corelibrary.Activation;
using Improbable.Corelib.Worker.Checkout;
using Bossa.Travellers.Ship.Lock;
using Bossa.Travellers.Craftingstation;
using Bossa.Travellers.Clock;
using Improbable.Corelib.Metrics;
using Bossa.Travellers.Devconsole;
using Bossa.Travellers.Social;
using Bossa.Travellers.Misc;
using Bossa.Travellers.Weather;
using Bossa.Travellers.Loot;

namespace WorldsAdriftReborn.Patching.Dynamic.InjectFallbackPatchnotes
{
    [HarmonyPatch(typeof(ChangeLogLoader))]
    internal class ChangeLogLoader_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ChangeLogLoader), "Start")]
        public static bool Start_Prefix(ChangeLogLoader __instance)
        {
            AccessTools.Method(typeof(ChangeLogLoader), "ParsePatchNotes").Invoke(__instance, new object[] { "someDummyShit" });

            /*
             * The following is used for serialization testing of the protobuf stuff, need to inject an object in there to serialize later
             */
            Debug.Log(ProtoBuf.Serializer.GetProto<Schema.Bossa.Travellers.Ecs.BlueprintData>());

            Blueprint.Data bData = new Blueprint.Data(new BlueprintData("Player"));

            TransformStateData tInit = new TransformStateData(  new FixedPointVector3(new List<long> { 0, 100, 0}),
                                                                new Quaternion32(1),
                                                                null,
                                                                new Improbable.Math.Vector3d(0f, 0f, 0f),
                                                                new Improbable.Math.Vector3f(0f, 0f, 0f),
                                                                new Improbable.Math.Vector3f(0f, 0f, 0f),
                                                                false,
                                                                0f);
            TransformState.Data tData = new TransformState.Data(tInit);

            TransformHierarchyState.Data thData = new TransformHierarchyState.Data(new TransformHierarchyStateData(new List<Child> { }));

            InventoryState.Data iData = new InventoryState.Data(new InventoryStateData( 100,
                                                                                        "{}",
                                                                                        new List<ScalaSlottedInventoryItem> { },
                                                                                        new List<ScalaSlottedInventoryItem> { },
                                                                                        200,
                                                                                        100,
                                                                                        new List<string> { },
                                                                                        true,
                                                                                        1));

            PlayerName.Data pData = new PlayerName.Data(new PlayerNameData("sp00ktober", "id", "cUid", "bossaToken", "bossaId"));

            PlayerPropertiesState.Data ppData = new PlayerPropertiesState.Data(new PlayerPropertiesStateData(   new Map<string, string> { },
                                                                                                                new Map<string, string>
                                                                                                                {
                                                                                                                    {"Head", "hair_dreads" },
                                                                                                                    {"Body", "torso_ponchoVariantB" },
                                                                                                                    {"Feet", "legs_wrap" },
                                                                                                                    {"Face", "face_C" }
                                                                                                                },
                                                                                                                new Improbable.Collections.List<string> { },
                                                                                                                false));

            HealthState.Data hData = new HealthState.Data(new HealthStateData(200, 200, true, 0f, true, new List<EntityId> { }, 1f, 1f));

            WearableUtilsState.Data wData = new WearableUtilsState.Data(new WearableUtilsStateData(new List<int> { }, new List<float> { }, new List<bool> { }));

            InteractAgentState.Data iaData = new InteractAgentState.Data(new InteractAgentStateData(    true,
                                                                                                        new EntityId(0),
                                                                                                        new EntityId(0),
                                                                                                        new EntityId(0),
                                                                                                        new Improbable.Math.Vector3f(0f, 0f, 0f),
                                                                                                        new Improbable.Math.Coordinates(),
                                                                                                        1,
                                                                                                        1));

            InteractAgentServerState.Data iasData = new InteractAgentServerState.Data(new InteractAgentServerStateData( new EntityId(0),
                                                                                                                        0,
                                                                                                                        1,
                                                                                                                        new EntityId(0),
                                                                                                                        new EntityId(0),
                                                                                                                        Bossa.Travellers.Items.MultitoolMode.Default,
                                                                                                                        new Option<ScalaSlottedInventoryItem> { }));

            AllianceNameState.Data anData = new AllianceNameState.Data(new AllianceNameStateData("WA Alliance"));
            AllianceAndCrewWorkerState.Data acData = new AllianceAndCrewWorkerState.Data(new AllianceAndCrewWorkerStateData("alliance_invalid_uid", "crew_invalid_uid"));

            InventoryModificationState.Data imData = new InventoryModificationState.Data();

            MountedGunShotState.Data mgData = new MountedGunShotState.Data();

            PilotState.Data psData = new PilotState.Data(new PilotStateData(new EntityId(0), new EntityId(0), ControlVehicleType.None));

            BuilderServerState.Data bsData = new BuilderServerState.Data(new BuilderServerStateData(new EntityId(0)));

            WorldData.Data woData = new WorldData.Data(new WorldDataData(new EntityId(0), 40f, 1f, 1));

            RopeControlPoints.Data rcData = new RopeControlPoints.Data(new RopeControlPointsData(new List<Coordinates> { }, new List<DynamicRopePoint> { }, false, 0f));

            ShipHullAgentState.Data shData = new ShipHullAgentState.Data(new ShipHullAgentStateData(new List<ShipHullSchematicData> { }, new EntityId(0)));

            PlayerAnalyticsState.Data paData = new PlayerAnalyticsState.Data(new PlayerAnalyticsStateData(  "someuser_id",
                                                                                                            "somesession_id",
                                                                                                            false,
                                                                                                            "unity",
                                                                                                            new List<string> { },
                                                                                                            "defaultPayload",
                                                                                                            false));

            KnowledgeServerState.Data ksData = new KnowledgeServerState.Data(new KnowledgeServerStateData(  1,
                                                                                                            new Map<string, int> { },
                                                                                                            1,
                                                                                                            new Map<string, int> { }));

            SchematicsLearnerClientState.Data scData = new SchematicsLearnerClientState.Data(new SchematicsLearnerClientStateData(  new List<string> { },
                                                                                                                                    new List<string> { },
                                                                                                                                    10,
                                                                                                                                    20,
                                                                                                                                    10,
                                                                                                                                    10));

            Activated.Data aData = new Activated.Data(new ActivatedData(true, true, 0));

            EntityLoadingControl.Data elData = new EntityLoadingControl.Data(new EntityLoadingControlData(  EntityLoadingControlData.EntityLoadingStates.Idle,
                                                                                                            0,
                                                                                                            5,
                                                                                                            100,
                                                                                                            false,
                                                                                                            new List<EntityId> { }));

            PlayerActivationState.Data pcData = new PlayerActivationState.Data(new PlayerActivationStateData(true, 12345, 123));

            ShipyardVisitorState.Data svData = new ShipyardVisitorState.Data(new ShipyardVisitorStateData(new EntityId(0), "abcdefg"));

            CraftingStationClientState.Data csData = new CraftingStationClientState.Data(new CraftingStationClientStateData(    "schematicId",
                                                                                                                                "owner",
                                                                                                                                new List<SlottedMaterial> { },
                                                                                                                                new List<Cipher> { },
                                                                                                                                12,
                                                                                                                                30f,
                                                                                                                                new Option<PredictedStatDataExtra> { }));

            NewPlayerState.Data npData = new NewPlayerState.Data(new NewPlayerStateData(true));

            PlayerBuffState.Data pbData = new PlayerBuffState.Data(new PlayerBuffStateData(new List<Buff> { }));

            FeedbackListener.Data fbData = new FeedbackListener.Data();

            FSimTimeState.Data fsData = new FSimTimeState.Data(new FSimTimeStateData(40f, "fsimId", 100));

            ClientPhysicsLatency.Data cpData = new ClientPhysicsLatency.Data(new ClientPhysicsLatencyData(250, 100));

            DevelopmentConsoleState.Data dcData = new DevelopmentConsoleState.Data(new DevelopmentConsoleStateData(100, 100, "gsimHostname", new Coordinates(0,0,0), "zone"));
            FsimStatus.Data fData = new FsimStatus.Data(new FsimStatusData(60f, 1234, 150, "fsimEngineId", 123));

            // this one is tricky because of the bone data
            ClientAuthoritativePlayerState.Data capData = new ClientAuthoritativePlayerState.Data(new ClientAuthoritativePlayerStateData(new Improbable.Math.Vector3f(0f, 100f, 0f),
                                                                                                                                            new Improbable.Corelib.Math.Quaternion(1,1,1,1),
                                                                                                                                            new EntityId(2),
                                                                                                                                            1f,
                                                                                                                                            100,
                                                                                                                                            new byte[] {},
                                                                                                                                            false,
                                                                                                                                            2,
                                                                                                                                            false,
                                                                                                                                            false,
                                                                                                                                            100));

            SocialWorkerId.Data wiData = new SocialWorkerId.Data(new SocialWorkerIdData("workerId"));

            GamePropertiesState.Data gpData = new GamePropertiesState.Data(new GamePropertiesStateData(new Map<string, string> { }));

            GsimEventAuditState.Data gsData = new GsimEventAuditState.Data(new GsimEventAuditStateData(new Map<string, int> { }));

            RadialStormState.Data rsData = new RadialStormState.Data(new RadialStormStateData(10f));

            WeatherCellState.Data wcData = new WeatherCellState.Data(new WeatherCellStateData(10f, new Vector3f(10f, 10f, 10f)));

            //---

            IslandLightningTimerState.Data ilData = new IslandLightningTimerState.Data(new IslandLightningTimerStateData(   50,
                                                                                                                            100,
                                                                                                                            1234,
                                                                                                                            2344,
                                                                                                                            true,
                                                                                                                            1,
                                                                                                                            new List<EntityId> { new EntityId(1) }));

            //Map<ulong, object> map = (Map<ulong, object>)AccessTools.Field(typeof(ClientObjects), "inFlightUpdates").GetValue(ClientObjects.Instance);
            //map.Add(1, ilData); // need to access this through serializer to see its output so we know how to make it ourself
            //AccessTools.Field(typeof(ClientObjects), "inFlightUpdates").SetValue(ClientObjects.Instance, map);

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ChangeLogLoader), "ParsePatchNotes")]
        public static bool ParsePatchNotes_Prefix(ChangeLogLoader __instance, string wwwText)
        {
            PatchNote patchNotePrefab = (PatchNote)AccessTools.Field(typeof(ChangeLogLoader), "patchNotePrefab").GetValue(__instance);
            UnityEngine.Transform patchNotesParent = (UnityEngine.Transform)AccessTools.Field(typeof(ChangeLogLoader), "patchNotesParent").GetValue(__instance);

            PatchNote patchNote = UnityEngine.Object.Instantiate<PatchNote>(patchNotePrefab, patchNotesParent);

            patchNote.version.text = "SpecialVersionBySp00ktober";
            patchNote.date.text = "30.08.2022";

            return false;
        }
    }
}
