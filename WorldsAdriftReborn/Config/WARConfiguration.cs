using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using WorldsAdriftReborn.HelperClasses;

namespace WorldsAdriftReborn.Config
{
    public class GeneralConfiguration
    {
        public string NtpServerUrl { get; set; }
        public string AssetLoaderFilePath { get; set; }
        public string GameServerHost { get; set; }

        public GeneralConfiguration(string ntpServerUrl, string assetLoaderFilePath, string gameServerHost) 
        {
            NtpServerUrl = ntpServerUrl;
            AssetLoaderFilePath = assetLoaderFilePath;
            GameServerHost = gameServerHost;
        }

        public override string ToString()
        {
            return $"|{nameof(GeneralConfiguration)}|{nameof(NtpServerUrl)}: {NtpServerUrl}," +
                   $" {nameof(AssetLoaderFilePath)}: {AssetLoaderFilePath}," +
                   $" {nameof(GameServerHost)}: {GameServerHost}";
        }
    }

    public class RESTConfiguration
    {
        public string ServerUrl { get; set; }
        public string ServerDeploymentUrl { get; set; }

        public RESTConfiguration(string serverUrl, string serverDeploymentUrl) 
        { 
            ServerUrl = serverUrl;
            ServerDeploymentUrl = serverDeploymentUrl;
        }

        public override string ToString()
        {
            return $"|{nameof(RESTConfiguration)}|{nameof(ServerUrl)}: {ServerUrl}," +
                   $" {nameof(ServerDeploymentUrl)}: {ServerDeploymentUrl}";
        }
    }

    public class SteamConfiguration
    {
        public string SteamUserId { get; set; }
        public string SteamAppId { get; set; }
        public string SteamBranchName { get; set; }

        public SteamConfiguration(string steamUserId, string steamAppId, string steamBranchName) 
        {
            SteamUserId = steamUserId;
            SteamAppId = steamAppId;
            SteamBranchName = steamBranchName;
        }

        public override string ToString()
        {
            return $"|{nameof(SteamConfiguration)}|{nameof(SteamUserId)}: {SteamUserId}," +
                   $" {nameof(SteamAppId)}: {SteamAppId}," +
                   $" {nameof(SteamBranchName)}: {SteamBranchName}";
        }
    }

    public class WARConfiguration
    {
        public GeneralConfiguration GeneralConfig { get; set; }
        public RESTConfiguration RESTConfig { get; set; }
        public SteamConfiguration SteamConfig { get; set; }

        private void BuildConfiguration(JObject jsonObj)
        {
            try
            {
                JToken warConfig = jsonObj[WARConstants.WARConfig];

                JToken steamConfigSection = warConfig[WARConstants.Steam];
                string steamUserId = steamConfigSection[WARConstants.SteamUserId].ToString();
                string steamAppId = steamConfigSection[WARConstants.SteamAppId].ToString();
                string steamBranchName = steamConfigSection[WARConstants.SteamBranchName].ToString();

                JToken restConfigSection = warConfig[WARConstants.REST];
                string restServerUrl = restConfigSection[WARConstants.RESTServerUrl].ToString();
                string restServerDeploymentUrl = restConfigSection[WARConstants.RESTServerDeploymentUrl].ToString();

                JToken ntpConfigSection = warConfig[WARConstants.NTP];
                string ntpServerUrl = ntpConfigSection[WARConstants.NTPServerUrl].ToString();

                JToken assetLoaderConfigSection = warConfig[WARConstants.AssetLoader];
                string assetLoaderFilePath = assetLoaderConfigSection[WARConstants.AssetLoaderFilePath].ToString();

                JToken gameServerConfigSection = warConfig[WARConstants.GameServer];
                string gameServerHost = gameServerConfigSection[WARConstants.GameServerHost].ToString();

                GeneralConfig = new GeneralConfiguration(ntpServerUrl, assetLoaderFilePath, gameServerHost);
                RESTConfig = new RESTConfiguration(restServerUrl, restServerDeploymentUrl);
                SteamConfig = new SteamConfiguration(steamUserId, steamAppId, steamBranchName);
            }
            catch(Exception e)
            {
                Debug.Log($"An exception occured while trying to build the WARConfig: {e}");
            }
        }

        public WARConfiguration(JObject jsonObj)
        {
            BuildConfiguration(jsonObj);
        }

        public override string ToString()
        {
            return $"{WARConstants.LogConfigBoarder}\nWAR Configuration: \n{GeneralConfig}\n{RESTConfig}\n{GeneralConfig}\n{WARConstants.LogConfigBoarder}";
        }
    }
}
