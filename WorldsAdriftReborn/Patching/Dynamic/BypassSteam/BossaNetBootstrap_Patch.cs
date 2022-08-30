using System;
using Bossa.Travellers.BossaNet;
using HarmonyLib;

namespace WorldsAdriftReborn.Patching.Dynamic.BypassSteam
{
    [HarmonyPatch(typeof(BossaNetBootstrap))]
    internal class BossaNetBootstrap_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(BossaNetBootstrap.AuthenticateWithSteam), new Type[] {typeof(BossaNetBootstrap.OnAuthSuccess), typeof(BossaNetBootstrap.OnAuthNoLinkedBossaAccount), typeof(BossaNetBootstrap.OnAuthError), typeof(BossaNetBootstrap.OnAccountNotVerified) })]
        public static bool AuthenticateWithSteam_Prefix(BossaNetBootstrap.OnAuthSuccess onAuthSuccess, BossaNetBootstrap.OnAuthNoLinkedBossaAccount onAuthNoLinkedBossaAccount, BossaNetBootstrap.OnAuthError onAuthError, BossaNetBootstrap.OnAccountNotVerified onAccountNotVerified )
        {
            return false;
        }
    }
}
