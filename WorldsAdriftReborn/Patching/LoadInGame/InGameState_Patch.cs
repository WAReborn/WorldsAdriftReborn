using System;
using System.Collections.Generic;
using Bossa.Travellers.Visualisers.Islands;
using GameStateMachine;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WorldsAdriftReborn.Patching.LoadInGame
{
    [HarmonyPatch(typeof(InGameState))]
    internal class InGameState_Patch
    {
        //Prefix patch to change some gameobject data
        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState.OnEnterState))]
        public static void InGameState_OnEnterState_Prefix()
        {
            Debug.LogWarning("TestPatch");
            List<GameObject> sceneObjects = new List<GameObject>();
            Scene scene = SceneManager.GetActiveScene();
            scene.GetRootGameObjects(sceneObjects);

            GameObject playerObject = null;
            GameObject atmosphericsObject = null;
            GameObject islandObject = null;

            //There is a better way to find a GO with compents that are attach to them
            //but since i think alot of this will be fix when the sever side start get set i think this is just a temp fix 
            for (int i = 0; i < sceneObjects.Count; i++)
            {
                if (playerObject != null && atmosphericsObject != null && islandObject != null)
                {
                    break;
                }
                if (sceneObjects[i].name == "Traveller@Player 1")
                {
                    playerObject = sceneObjects[i];
                }
                if (sceneObjects[i].name == "Atmospherics")
                {
                    atmosphericsObject = sceneObjects[i];
                }
                if (sceneObjects[i].name == "949069116@Island 2")
                {
                    islandObject = sceneObjects[i];
                }

            }


            playerObject.transform.position = new Vector3(0, -30, 0);

            GameObject cloudController = atmosphericsObject.transform.Find("CloudController").gameObject;
            CmdBufClouds cmdBufClouds = cloudController.GetComponent<CmdBufClouds>();
            cmdBufClouds.GlobalOffset = new Vector3(0, 0, 0);

            IslandVisualiser islandVisualiser = islandObject.GetComponent<IslandVisualiser>();
            islandVisualiser.enabled = true;

        }
    }
}
