using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

// Adapted from https://github.com/ManlyMarco/KK_GamepadSupport/blob/master/Core_GamepadSupport/DependencyLoader.cs

namespace WorldsAdriftReborn
{
    internal static class DependencyLoader
    {
        [DllImport("__Internal", CharSet = CharSet.Ansi)]
        private static extern void mono_dllmap_insert( IntPtr assembly, string dll, string func, string tdll, string tfunc );

        public static void LoadDependencies()
        {
            // This points to the CoreSdkDll.dll which should be included with the WorldsAdriftReborn bepinex plugin
            string replacementDllPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "CoreSdkDll"
            );

            // Check if replacementDll exists before attempting mono_dllmap_insert
            string replacementDllPathIncludingExtension = Path.GetExtension(replacementDllPath) != "" ? replacementDllPath : $"{replacementDllPath}.dll";
            if (!File.Exists(replacementDllPathIncludingExtension))
            {
                throw new IOException($"Unable to find {replacementDllPathIncludingExtension}, file does not exist.");
            }

            mono_dllmap_insert(IntPtr.Zero, "CoreSdkDll", null, replacementDllPath, null);
        }
    }
}
