using System;
using RepetierHostExtender.interfaces;

namespace TaskbarPlus
{
    /// <summary>
    /// This static class is used for store all settings for the plugin
    /// Only iconOverlay and progressBar ar saved/loaded in the windows registry
    /// the other parameters are hard coded
    /// </summary>
    internal static class Settings
    {      
        // TaskbarPlus version
        public static string pluginVersion = "1.0";
        // Plugin home page
        public static string homepage = "https://github.com/BrOncOVu/TaskbarPlus_Repetier";
        // Repetier-Host AppUserModelID
        public static string AppUserModelIDs = "Repetier-Host";
        // Repetier host registry key used for add the AppUserModelID
        public static string registrykeyPath = "Software\\Classes\\Repetier-Host";
        // If true debug info is added in the repetier host log.
        public static bool debugON = false;
        // only windows 7 and later support this taskbar feature
        public static bool supportedOS = CheckOSVersion();

        // taskbar icon settings
        public static bool iconOverlay = true;
        public static bool progressBar = true;

        
        /// <summary>
        /// Load all settings from the registry
        /// </summary>
        /// <param name="_host">IHost</param>
        public static void load_Settings(IHost _host)
        {
            IRegMemoryFolder Ireg = _host.GetRegistryFolder("TaskbarPlus");
            // Taskbar icon
            iconOverlay = Ireg.GetBool("iconOverlay", true);
            progressBar = Ireg.GetBool("progressBar", true);        
        }

        // Check if is windows 7 or recent OS
        private static bool CheckOSVersion()
        {
            System.OperatingSystem osInfo = System.Environment.OSVersion;

            if (Environment.OSVersion.Platform != PlatformID.Win32NT ||
               Environment.OSVersion.Version.CompareTo(new Version(6, 1)) < 1)
            {
                return false;
                // OS not support icon overlay or jumplist
            }
            return true;
        }

    }
}
