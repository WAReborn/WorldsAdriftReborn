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
        internal static void ShowUsage()
        {
            Console.WriteLine("--dir <dir> - Directory of your Worlds Adrift installation\n--help - List the commands and their different functions");
        }
        internal static bool ParseArgs(string[] args) 
        {
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                switch(arg)
                {
                    case "--dir":
                        GlobalValues.GameDirectory = args[i + 1];
                        break;
                    case "--help":
                        ShowUsage();
                        break;
                }
            }

            //Default
            if (GlobalValues.GameDirectory == "")
            {
                GlobalValues.GameDirectory = Directory.GetCurrentDirectory();
            }

            return true;
        }

        public static void Main(string[] args)
        {
            ParseArgs(args);

            //
            IPatcher patcher = new DnlibPatcher();
            patcher.PatchAll();
            //

            //Make sure console doesn't just close right away
            Console.ReadLine();
        }
    }
}
