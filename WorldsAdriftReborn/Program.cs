using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WorldsAdriftReborn.Patching;

namespace WorldsAdriftReborn
{
    internal class Program
    {
        internal static bool ParseArgs(string[] args, ref string GameInstallationDirectory) 
        {
            GameInstallationDirectory = "";
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                switch(arg)
                {
                    case "--dir":
                        GameInstallationDirectory = args[i + 1];
                        break;
                }
            }

            //Default
            if (GameInstallationDirectory == "")
            {
                GameInstallationDirectory = Directory.GetCurrentDirectory();
            }

            return true;
        }

        public static void Main(string[] args)
        {
            string GameInstallationDirectory = "";
            ParseArgs(args, ref GameInstallationDirectory);

            IPatcher patcher = new DnlibPatcher();
            patcher.PatchAll();

            //Make sure console doesn't just close right away
            Console.ReadLine();
        }
    }
}
