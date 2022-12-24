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
            playerInitializedComponents = new Dictionary<ENetPeerHandle, Dictionary<long, List<uint>>>(); //for each ENetPeerHandle we have a dict with entityId as key and a list of added components to it
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
        public Dictionary<ENetPeerHandle, Dictionary<long, List<uint>>> playerInitializedComponents { get; set; }

        public void SetENetHostHandle(ENetHostHandle client )
        {
            this.server = client;
        }
    }
}
