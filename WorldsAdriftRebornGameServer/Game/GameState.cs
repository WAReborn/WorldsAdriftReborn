using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldsAdriftRebornGameServer.Game
{
    internal class GameState
    {
        public enum State
        {
            NEWLY_CONNECTED,
            NEWLY_CONNECTED_PENDING,
            PLAYER_ASSET_LOADED,
            ISLAND_LOAD_PENDING,
            ISLAND_LOADED,
            ISLAND_SPAWN_PENDING,
            ISLAND_SPAWNED,
            PLAYER_SPAWN_PENDING,
            PLAYER_SPAWNED,
            DONE
        }
    }
}
