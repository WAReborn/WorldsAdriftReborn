using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldsAdriftServer.Objects.CharacterSelection
{
    internal class ReserveNameResponse
    {
        public string desc { get; set; }
    }

    internal class ReserveNameRequest
    {
        public string screenName { get; set; }
        public string characterUid { get; set; }
    }
}
