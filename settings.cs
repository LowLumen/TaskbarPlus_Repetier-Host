using System;
using RepetierHostExtender.interfaces;

namespace TaskbarPlus
{
    /// <summary>
    /// This static class is used for store all settings for the plugin
    /// Only iconOverlay and progressBar ar saved/loaded in/from the windows registry
    /// the other parameters are hard coded
    /// </summary>
    internal static class Settings
    {      
        // TaskbarPlus plugin version
        public static string pluginVersion = "1.0";
        // Plugin home page
        public static string homepage = "https://github.com/BrOncOVu/TaskbarPlus_Repetier-Host";
        // Repetier-Host AppUserModelID
        public static string AppUserModelIDs = "Repetier-Host";
        // If true debug info is added in the repetier host log.
        public static bool debugON = false;
        // only windows 7 and later support this taskbar feature
        public static bool supportedOS = CheckOSVersion();

        // taskbar icon settings
        public static bool iconOverlay = true;
        public static bool progressBar = true;

        
        /// <summary>
        /// Load plugin settings from the registry
        /// </summary>
        /// <param name="_host">IHost instance</param>
        public static void load_Settings(IHost _host)
        {
            IRegMemoryFolder Ireg = _host.GetRegistryFolder("TaskbarPlus");
            // Taskbar icon
            iconOverlay = Ireg.GetBool("iconOverlay", true);
            progressBar = Ireg.GetBool("progressBar", true);        
        }

        /// <summary>
        /// Check if the version of windows support taskbar feature
        /// </summary>
        /// <returns></returns>
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
