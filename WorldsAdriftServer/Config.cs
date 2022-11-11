using WorldsAdriftServer.Objects.DeploymentStatus;

namespace WorldsAdriftServer
{
    internal static class Config
    {
        public static string GameVersion { get; } = "0.3.32.0.1880";
        public static string Region { get; } = "Comunity_Server";
        public static string CharacterHeader { get; } = "characterUid";
        public static string SecurityHeader { get; } = "Security";

        public static Dictionary<string, ServerStatusRecord> ServerStatusDictionary = new Dictionary<string, ServerStatusRecord>
        {
            { "kubo_community_server", new ServerStatusRecord("Kubo Community Server", "kubo_community_server", ServerStatus.UP, "0") },
            { "driss_community_server", new ServerStatusRecord("Driss Community Server", "driss_community_server", ServerStatus.UP, "0") },
        };

        //temp configs till Authencation is setup
        public static string SteamAuthError { get; } = "bossa_identity_not_linked";
        public static string PlayerId { get; } = "777";
        public static string BossaId { get; } = "999";
    }
}
