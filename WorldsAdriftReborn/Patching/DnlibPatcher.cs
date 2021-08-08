using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using dnlib;
using WorldsAdriftReborn.Patching.Dnlib;

namespace WorldsAdriftReborn.Patching
{
    public class DnlibPatcher : IPatcher
    {
        private IEnumerable<IDnlibPatch> DiscoverPatches()
        {
            IEnumerable<IDnlibPatch> patches = typeof(DnlibPatcher).Assembly.GetTypes()
                .Where(p => typeof(IDnlibPatch).IsAssignableFrom(p) && p.IsClass)
                .Select(p => (IDnlibPatch)Activator.CreateInstance(p));
            return patches;
        }

        public void PatchAll()
        {
            IEnumerable<IDnlibPatch> patches = DiscoverPatches();

            foreach (IDnlibPatch patch in patches)
            {
                string patchName = patch.GetType().Name;
                Console.WriteLine($"Applying {patchName}");

                Console.WriteLine($"{patchName} {(patch.Patch() ? "succeeded" : "failed")}");
            }
        }
    }
}
