using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldsAdriftReborn.Patching;

namespace WorldsAdriftReborn
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IPatcher patcher = new DnlibPatcher();

            patcher.PatchAll();
        }
    }
}
