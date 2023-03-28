using WorldsAdriftRebornGameServer.DLLCommunication;
using WorldsAdriftRebornGameServer.Game;

namespace WorldsAdriftRebornGameServer.Networking.Singleton
{
    internal class PlayerSyncStatus
    {
        private int syncStepPointer { get; set; }
        public int SyncStepPointer {
            get
            {
                return syncStepPointer;
            }
            set
            {
                syncStepPointer = value;
                Performed = false;
            }
        }
        public bool Performed { get; set; }
        public PlayerSyncStatus()
        {
            SyncStepPointer = 0;
        }
    }
    internal class PeerManager
    {
        private static PeerManager instance = null;
        private PeerManager()
        {
            server = new ENetHostHandle();
            playerState = new Dictionary<ENetPeerHandle, Dictionary<int, PlayerSyncStatus>>();
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
        // each world chunk has a list of actions that needs to be perfomred for every client to sync up to the current state, and each client has an individual pointer to his state
        public Dictionary<ENetPeerHandle, Dictionary<int, PlayerSyncStatus>> playerState { get; set; }
        public List<ENetPeerHandle> clientSetupState { get; set; }

        public void SetENetHostHandle(ENetHostHandle client )
        {
            server = client;
        }
    }
}
