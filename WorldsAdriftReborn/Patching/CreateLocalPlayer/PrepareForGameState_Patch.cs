using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Assets.Scripts.Player;
using Assets.Scripts.Visualisers.Items;
using Assets.Visualizers;
using Bossa.Prototype.Character.Observer;
using Bossa.Travellers;
using Bossa.Travellers.Alliances;
using Bossa.Travellers.Analytics;
using Bossa.Travellers.Behaviours;
using Bossa.Travellers.Controls.Observer;
using Bossa.Travellers.CraftingStation;
using Bossa.Travellers.Crew;
using Bossa.Travellers.DevConsole;
using Bossa.Travellers.Visualisers;
using Bossa.Travellers.Visualisers.Customisation;
using Bossa.Travellers.Visualisers.Profile;
using GameStateMachine;
using HarmonyLib;
using Improbable.Core.Entity;
using Improbable.Unity.Entity;
using UnityEngine;
using UnityEngine.AI;

namespace WorldsAdriftReborn.Patching.CreateLocalPlayer
{
    [HarmonyPatch(typeof(PrepareForGameState))]
    internal class PrepareForGameState_Patch
    {
        /*
         * The Update loop waits for LocalPlayer to get created/ready until it eventually times out and throws an error forcing the user to quit.
         * So far i did not find the place where the LocalPlayer gets created, so i'll just create it here manually once the game would normally throw the error.
         */
        private static GameObject playerObject = null;
        private static GameObject equippableSlot = null;

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(PrepareForGameState.Update))]
        public static IEnumerable<CodeInstruction> Update_Transpiler(IEnumerable<CodeInstruction> instructions )
        {
            CodeMatcher matcher = new CodeMatcher(instructions)
                .MatchForward(false,
                    new CodeMatch(OpCodes.Ldarg_0),
                    new CodeMatch(OpCodes.Ldarg_0),
                    new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(PrepareForGameState), "_improbableBootstrap")))
                .Advance(-2)
                .Set(OpCodes.Ldc_R4, 20f) // do not wait 300 but 20 secs
                .Advance(2)
                .InsertAndAdvance(Transpilers.EmitDelegate<Func<int>>(() =>
                {
                    if(playerObject != null)
                    {
                        playerObject = new GameObject();
                        playerObject.name = "CustomLocalPlayer";

                        equippableSlot = new GameObject();
                        equippableSlot.name = "#EquippableSlot";

                        equippableSlot.transform.parent = playerObject.transform;

                        EntityObject eObject = new EntityObject(new Improbable.EntityId(1), playerObject, "", null);
                        EntityObjectStorage eStorage = playerObject.AddComponent<EntityObjectStorage>();

                        AccessTools.Method(typeof(EntityObjectStorage), "set_Entity").Invoke(eStorage, new object[] { eObject });

                        playerObject.AddComponent<UIFocusObserver>();
                        playerObject.AddComponent<CustomisationSenderVisualiser>();

                        playerObject.AddComponent(AccessTools.TypeByName("PlayerLookingAt"));
                        InteractAgentObserver iaObserver = playerObject.AddComponent<InteractAgentObserver>();
                        //AccessTools.Field(typeof(InteractAgentObserver), "_serverState").SetValue(iaObserver, new Bossa.Travellers.Interact.InteractAgentServerState.Reader);
                        playerObject.AddComponent<InventoryModificationBehaviour>();

                        playerObject.AddComponent<PlayerPropertiesVisualiser>();
                        playerObject.AddComponent<InventoryVisualiser>();
                        playerObject.AddComponent<PlayerCraftingInteractionBehaviour>();
                        playerObject.AddComponent<MultitoolCraftingBehaviour>();
                        playerObject.AddComponent<RespawnVisualizer>();
                        playerObject.AddComponent<HealthUIVisualizer>();
                        playerObject.AddComponent<LogoutBehaviour>();
                        playerObject.AddComponent<ShipyardVisitorVisualizer>();
                        playerObject.AddComponent<CrewManagementVisualiser>();
                        playerObject.AddComponent<PlayerAnalytics>();
                        playerObject.AddComponent<ToolBehaviour>();
                        playerObject.AddComponent<ScanningAgentVisualizer>();
                        playerObject.AddComponent<LeftArmAimingVisualizer>();
                        playerObject.AddComponent<InteractAgentObserver>();
                        playerObject.AddComponent<LorePiecesCollectorVisualizer>();
                        playerObject.AddComponent<ShipHullAgentVisualizer>();
                        playerObject.AddComponent<PlayerShipBlueprintInteractionBehaviour>();
                        playerObject.AddComponent<DebugInfoController>();
                        playerObject.AddComponent<PlacementPreview>();
                        playerObject.AddComponent<TimedInteractionController>();
                        playerObject.AddComponent<PlayerEquipmentVisualizer>();
                        playerObject.AddComponent<ReferenceDataVisualiser>();
                        playerObject.AddComponent<ServerClock>();
                        playerObject.AddComponent<PlayerActivationVisualiser>();
                        playerObject.AddComponent<SlotsVisualiser>();
                        playerObject.AddComponent<PlayerHotbarVisualizer>();
                        playerObject.AddComponent<ClientAuthoritativePlayerMovement>();
                        playerObject.AddComponent<PlayerNameVisualizer>();
                        playerObject.AddComponent<PlayerBuffBehaviour>();
                        playerObject.AddComponent<NewPlayerVisualiser>();
                        playerObject.AddComponent<FsimFpsVisualiser>();
                        playerObject.AddComponent<FSimTimeVisualiser>();
                        playerObject.AddComponent<PlayerScannerToolVisualizer>();
                        playerObject.AddComponent<AllianceAndCrewVisualiser>();
                        playerObject.AddComponent<AllianceClientBehaviour>();

                        playerObject.AddComponent<LocalPlayerInit>();
                        playerObject.SetActive(true);
                        Debug.LogWarning("CREATING PLAYER!");
                    }
                    return 0;
                }))
                .InsertAndAdvance(new CodeInstruction(OpCodes.Pop));

            for(; matcher.Opcode != OpCodes.Ret;)
            {
                Debug.Log("overwriting " + matcher.Opcode);
                matcher.SetAndAdvance(OpCodes.Nop, null);
            }
            matcher.Set(OpCodes.Nop, null);

            return matcher.InstructionEnumeration();
        }
    }
}
