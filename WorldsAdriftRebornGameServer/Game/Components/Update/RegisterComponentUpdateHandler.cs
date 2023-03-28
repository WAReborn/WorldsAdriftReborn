using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldsAdriftRebornGameServer.Game.Components.Update
{
    // registers a handler for a specific ComponentUpdateOp handler
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class RegisterComponentUpdateHandler : Attribute { }
}
