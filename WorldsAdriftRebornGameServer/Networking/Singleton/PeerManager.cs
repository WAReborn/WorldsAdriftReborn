using WorldsAdriftRebornGameServer.DLLCommunication;
using WorldsAdriftRebornGameServer.Game;

namespace WorldsAdriftRebornGameServer.Networking.Singleton
{
    internal class PeerManager
    {
        private static PeerManager instance = null;
        private PeerManager()
        {
            server = new ENetHostHandle();
            playerState = new Dictionary<ENetPeerHandle, GameState.State>();
            clientSetupState = new List<ENetPeerHandle>();
        }
        public static PeerManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new PeerManager();
                }
                return instance;
            }
        }

        public ENetHostHandle server { get; set; }
        public Dictionary<ENetPeerHandle, GameState.State> playerState { get; set; }
        public List<ENetPeerHandle> clientSetupState { get; set; }

        public void SetENetHostHandle(ENetHostHandle client )
        {
            this.server = client;
        }
    }
}
