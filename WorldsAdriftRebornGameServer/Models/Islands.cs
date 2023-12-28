using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace WorldsAdriftRebornGameServer.Models
{
    public class IslandEntity
    {
        public long EntityId { get; set; }
        public string? Name { get; set; }

        public Vector3? Position { get; set; }
    }

}
