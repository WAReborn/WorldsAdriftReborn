using System;
using Bossa.Travellers.BossaNet;
using HarmonyLib;
using WorldsAdriftReborn.Config;

namespace WorldsAdriftReborn.Patching.Dynamic.BypassSteam
{
    [HarmonyPatch(typeof(BossaNetBootstrap))]
    internal class BossaNetBootstrap_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(BossaNetBootstrap.AuthenticateWithSteam), new Type[] {typeof(string), typeof(string), typeof(BossaNetBootstrap.OnAuthSuccess), typeof(BossaNetBootstrap.OnAuthNoLinkedBossaAccount), typeof(BossaNetBootstrap.OnAuthError), typeof(BossaNetBootstrap.OnAccountNotVerified) })]
        public static bool AuthenticateWithSteam_Prefix(ref string steamUserId, string steamAuthToken, BossaNetBootstrap.OnAuthSuccess onAuthSuccess, BossaNetBootstrap.OnAuthNoLinkedBossaAccount onAuthNoLinkedBossaAccount, BossaNetBootstrap.OnAuthError onAuthError, BossaNetBootstrap.OnAccountNotVerified onAccountNotVerified )
        {
            ModSettings.modConfig.Reload();
            steamUserId = ModSettings.steamUserId.Value;

            return true;
        }
    }
}
