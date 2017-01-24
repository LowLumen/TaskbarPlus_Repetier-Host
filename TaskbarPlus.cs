using System;
using RepetierHostExtender.interfaces;
using RepetierHostExtender.utils;
using RepetierHostExtender.basic;
using System.IO;
using System.Runtime.InteropServices;


namespace TaskbarPlus
{


    public class TaskbarPlus : IHostPlugin
    {
        IHost host;

        /// <summary>
        /// Called first to allow filling some lists. Host is not fully set up at that moment.
        /// </summary>
        /// <param name="host"></param>
        public void PreInitalize(IHost _host)
        {
            host = _host;

            if (!Settings.supportedOS)
                return;
            
            // Set the appUserModelID to Repetierhost.exe process befor UI is loaded
            SetCurrentProcessExplicitAppUserModelID(Settings.AppUserModelIDs);
            // Set a key in the windows registry.
            // If the plugin is deleted this string remain in the registry
            // and the recent/frequent list not work correctly!!
            SetRegKey(); 
            
        }
        /// <summary>
        /// Called after everything is initalized to finish parts, that rely on other initializations.
        /// Here you must create and register new Controls and Windows.
        /// </summary>
        public void PostInitialize()
        {
            // load all plugin settings from the registry
            Settings.load_Settings(host);

            // load plugin translation file
            string langPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "translations");
            if (Directory.Exists(langPath))
                Trans.trans.AddFolder(langPath);

            
            // Add the plugin settings Control to Repetier-Host preference window
            SettingsControl SettingsCon = new SettingsControl();
            SettingsCon.Connect(host);
            host.RegisterHostComponent(SettingsCon);
        
            // Add some info in the about dialog
            host.AboutDialog.RegisterThirdParty(
                "TaskbarPlus", "\r\n\r\n TaskbarPlus Plugin for Repetier-Host."
                + "\r\n Version: " + Settings.pluginVersion
                + "\r\n Homepage: " + Settings.homepage);


            if (!Settings.supportedOS)
            {
                host.LogError("TaskbarPlus plugin Error: OS not supported, only windows 7 or above");
                return;
            }

            /// icon taskbar code ///

            // Change the TaskbarIcon window handle with the repetier host main window
            // This initialize the TaskbarIcon class
            TaskbarIcon.set.OwnerHandle = host.HostWindow.Handle;

            // All Event used
            host.Connection.eventConnectionChange += event_ConnectionChange;
            host.JobFinishedEvent += event_JobFinishedEvent;
            host.JobStoppedEvent += event_JobFinishedEvent;
            host.ProgressChanged += event_ProgressChanged;
            host.SDStateChanged += event_SDStateChanged;
            
        }
        /// <summary>
        /// Last round of plugin calls. All controls exist, so now you may modify them to your wishes.
        /// </summary>
        public void FinializeInitialize()
        {
            // Set the appUserModelID to repetier host window
            //  using this if SetCurrentProcessExplicitAppUserModelID not work
            // AppID.SetWindowAppId(host.HostWindow.Handle, settings.AppUserModelIDs);
        }

        /// <summary>
        /// Write the AppUserModelID in the Repetier-Host registry key
        /// </summary>
        private bool SetRegKey()
        {
            Microsoft.Win32.RegistryKey key;

            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Settings.registrykeyPath);
            // if AppUserModelID exist return;
            if (key == null || key.GetValue("AppUserModelID") != null)
                return false;

            // if key not exist add it, is the first load for the plugin!!
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(Settings.registrykeyPath);
            key.SetValue("AppUserModelID", Settings.AppUserModelIDs);
            key.Close();

            return true;
        }


        #region Event response

        private void event_ConnectionChange(string msg)
        {
            // print the string debug in repetier-host log
            if (Settings.debugON)
                host.LogInfo("TaskbarPlus DEBUG: eventConnectionChange:" + msg);

            //Select the icon overlay
            if (host.Connection.connector.IsConnected())
            {
                // Printer is connected, add overlay icon
                if (Settings.iconOverlay)
                    TaskbarIcon.set.OverlayIcon(OverIconType.stop);
            }
            else
            {
                // Printer not connected, delete icon
                if (Settings.iconOverlay)
                    TaskbarIcon.set.OverlayIcon(OverIconType.NULL);
            }

            // Reset the Progress bar status
            if (Settings.progressBar)
                TaskbarIcon.set.ProgressState(ProgressStateType.NoProgress);
                   
        }

        private void event_ProgressChanged(ProgressType pType, double value)
        {
            // print the string debug in repetier-host log
            if (Settings.debugON)
            {
                host.LogInfo("TaskbarPlus DEBUG: ProgressChanged " 
                    + pType.ToString() + ":" + value.ToString()
                    + " - IsJobRunning?: " + (host.IsJobRunning ? "yes" : "no")
                    );
            }

            // Select the overlay icon and update the Progress Bar status
            switch (pType)
            {
                case ProgressType.PRINT_JOB://normal print
                    // Somtimes ProgressChanged() is called AFTER JobFinishedEvent() 
                    // and the icon is reseted to play status
                    // Check if job is running
                    if (host.IsJobRunning)
                    {
                        if (Settings.iconOverlay)
                            TaskbarIcon.set.OverlayIcon(OverIconType.play);
                        if (Settings.progressBar)
                            TaskbarIcon.set.ProgressState(ProgressStateType.Normal);
                    }

                    break;
                case ProgressType.SDPRINT://print from SD card

                    if (Settings.iconOverlay)
                        TaskbarIcon.set.OverlayIcon(OverIconType.sdPrinting);
                    if (Settings.progressBar)
                        TaskbarIcon.set.ProgressState(ProgressStateType.Normal);

                    break;
                case ProgressType.UPLOAD_JOB://upload file to bard
                    if (host.IsJobRunning)
                    {
                        if (Settings.iconOverlay)
                            TaskbarIcon.set.OverlayIcon(OverIconType.sdUpload);
                        if (Settings.progressBar)
                            TaskbarIcon.set.ProgressState(ProgressStateType.Normal);
                    }

                    break;

                case ProgressType.CACHE_JOB:// not sure.. print from cache? use play icon
                    if (host.IsJobRunning)
                    {
                        if (Settings.iconOverlay)
                            TaskbarIcon.set.OverlayIcon(OverIconType.play);
                        if (Settings.progressBar)
                            TaskbarIcon.set.ProgressState(ProgressStateType.Normal);
                    }
                    break;

                case ProgressType.NONE://no print

                    if (Settings.iconOverlay)
                        TaskbarIcon.set.OverlayIcon(OverIconType.stop);
                    if (Settings.progressBar)
                        TaskbarIcon.set.ProgressState(ProgressStateType.NoProgress);

                    break;
            }

            // update the progress bar value 
            // value range is : 0 to 100 (%)
            if (Settings.progressBar)
                TaskbarIcon.set.ProgressValue(Convert.ToInt32(value));
             
        }

        private void event_SDStateChanged(GCodeAnalyzer.SDStateValue old, GCodeAnalyzer.SDStateValue current)
        {
            // print the string debug in repetier-host log
            if (Settings.debugON)
                host.LogInfo("TaskbarPlus DEBUG: SDStateChanged : " + old.ToString() + " to: " + current.ToString());

            // Select the overlay icon and update the Progress Bar status
            switch (current)
            {
                case GCodeAnalyzer.SDStateValue.PRINTING:

                    if (Settings.iconOverlay) 
                        TaskbarIcon.set.OverlayIcon(OverIconType.sdPrinting);                         
                    if (Settings.progressBar && old == GCodeAnalyzer.SDStateValue.PRINT_PAUSED)
                        TaskbarIcon.set.ProgressState(ProgressStateType.Normal);

                    break;

                case GCodeAnalyzer.SDStateValue.PRINT_PAUSED:

                    if (Settings.iconOverlay)
                        TaskbarIcon.set.OverlayIcon(OverIconType.sdPause);
                    if (Settings.progressBar)
                        TaskbarIcon.set.ProgressState(ProgressStateType.Paused);

                    break;

                case GCodeAnalyzer.SDStateValue.NOT_USED:

                    // probable this is the same in host_JobFinishedEvent but just for sure
                    if (old == GCodeAnalyzer.SDStateValue.PRINTING || old == GCodeAnalyzer.SDStateValue.WRITING)
                    {
                        if (Settings.iconOverlay)
                            TaskbarIcon.set.OverlayIcon(OverIconType.stop);
                        if (Settings.progressBar)
                            TaskbarIcon.set.ProgressState(ProgressStateType.NoProgress);
                    }

                    break;
            }
        }

        private void event_JobFinishedEvent()
        {
            // print the string debug in repetier-host log
            if (Settings.debugON)
                host.LogInfo("TaskbarPlus DEBUG: Job finished or stoped, set stop icon in the taskbar");
            
            // stop icon and Progress Bar
            if (Settings.iconOverlay)
                TaskbarIcon.set.OverlayIcon(OverIconType.stop);
            if ( Settings.progressBar)
                TaskbarIcon.set.ProgressState(ProgressStateType.NoProgress);
        }

        #endregion


        // P/Invoke

        /// <summary>
        /// This method must be called during an application's initial startup routine
        /// before the application presents any UI or makes any manipulation of its Jump Lists.
        /// </summary>
        /// <param name="AppID">the AppUserModelID</param>
        [DllImport("shell32.dll")]
        private static extern void SetCurrentProcessExplicitAppUserModelID(
            [MarshalAs(UnmanagedType.LPWStr)] string AppID);


    }
}
