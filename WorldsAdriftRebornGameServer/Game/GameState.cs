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
            ISLAND_SPAWNED,
            PLAYER_SPAWNED
        }
    }
}
