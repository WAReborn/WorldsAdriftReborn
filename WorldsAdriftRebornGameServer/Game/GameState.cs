using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorldsAdriftRebornGameServer.DLLCommunication;

namespace WorldsAdriftRebornGameServer.Game
{
    internal class SyncStep
    {
        public GameState.NextStateRequirement NextStateRequirement { get; set; }
        public Action<object> Step { get; set; } = new Action<object>((object o) => { });
        public SyncStep(GameState.NextStateRequirement req, Action<object> action)
        {
            NextStateRequirement = req;
            Step = action;
        }
    }
    internal class GameState
    {
        public enum NextStateRequirement
        {
            NOTHING,
            ASSET_LOADED_RESPONSE,
            ADDED_ENTITY_RESPONSE
        }

        private static GameState instance { get; set; }
        public static GameState Instance
        {
            get
            {
                return instance ?? (instance = new GameState());
            }
        }
        // each world chunk has a list of actions that needs to be perfomred for every client to sync up to the current state.
        public Dictionary<int, List<SyncStep>> WorldState { get; set; }
        // we need to store mappings for components for each player to handle updates of them
        // this crappy dictionary nesting needs to be replaced, we need to make use of the games own structures here, this is only temporary
        public Dictionary<ENetPeerHandle, Dictionary<long, Dictionary<uint, ulong>>> ComponentMap = new Dictionary<ENetPeerHandle, Dictionary<long, Dictionary<uint, ulong>>>();

        private GameState()
        {
            WorldState = new Dictionary<int, List<SyncStep>>
            {
                { 0, new List<SyncStep>() }
            };
        }
    }
}
