using HarmonyLib;

namespace WorldsAdriftReborn.Patching.Dynamic.BypassSteam
{
    [HarmonyPatch(typeof(SteamManager))]
    internal class SteamManager_Patch
    {
    }
}
