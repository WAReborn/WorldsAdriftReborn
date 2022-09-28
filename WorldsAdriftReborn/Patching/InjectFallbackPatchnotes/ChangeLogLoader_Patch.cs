using Bossa.Travellers.UI;
using HarmonyLib;
using Improbable.Collections;
using Improbable.Worker.Internal;
using Improbable;
using UnityEngine;
using System;
using Bossa.Travellers.Ecs;
using Improbable.Worker;

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

            BlueprintData blueprintData = new BlueprintData();
            blueprintData.identifier = "asdf";

            Type tConnectionHandle = AccessTools.TypeByName("ConnectionHandle");
            Type tConnection = AccessTools.TypeByName("Connection");
            Blueprint.Impl bImpl = new Blueprint.Impl((Connection)AccessTools.Constructor(tConnection, new Type[] { tConnectionHandle }).Invoke(new object[] { AccessTools.Constructor(tConnectionHandle).Invoke(new object[] { }) }), new EntityId(0), new Blueprint.Data(blueprintData));

            Blueprint.Update update = new Blueprint.Update();
            update.identifier = "asdf";

            Blueprint.Data bData = new Blueprint.Data(new BlueprintData("Player"));

            //Map<ulong, object> map = (Map<ulong, object>)AccessTools.Field(typeof(ClientObjects), "inFlightUpdates").GetValue(ClientObjects.Instance);
            //map.Add(1, bData); // need to access this through serializer to see its output so we know how to make it ourself
            //AccessTools.Field(typeof(ClientObjects), "inFlightUpdates").SetValue(ClientObjects.Instance, map);

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ChangeLogLoader), "ParsePatchNotes")]
        public static bool ParsePatchNotes_Prefix(ChangeLogLoader __instance, string wwwText)
        {
            PatchNote patchNotePrefab = (PatchNote)AccessTools.Field(typeof(ChangeLogLoader), "patchNotePrefab").GetValue(__instance);
            Transform patchNotesParent = (Transform)AccessTools.Field(typeof(ChangeLogLoader), "patchNotesParent").GetValue(__instance);

            PatchNote patchNote = UnityEngine.Object.Instantiate<PatchNote>(patchNotePrefab, patchNotesParent);

            patchNote.version.text = "SpecialVersionBySp00ktober";
            patchNote.date.text = "30.08.2022";

            return false;
        }
    }
}
