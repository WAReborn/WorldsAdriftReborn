using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldsAdriftReborn.Patching.Dnlib
{
    public class HelloWorldPatch : IDnlibPatch
    {
        public bool Patch()
        {
            Console.WriteLine("Hello World");
            return true;
        }
    }
}
