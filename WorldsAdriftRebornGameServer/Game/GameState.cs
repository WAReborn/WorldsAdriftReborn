using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private GameState()
        {
            WorldState = new Dictionary<int, List<SyncStep>>
            {
                { 0, new List<SyncStep>() }
            };
        }
    }
}
