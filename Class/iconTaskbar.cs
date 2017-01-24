using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;

namespace TaskbarPlus
{

    internal class TaskbarIcon
    {
        // Class instance
        private static TaskbarIcon _instance;

        private  ITaskbarList4 _taskbarList;

        // window handler
        private IntPtr _windowHandle;


        private TaskbarIcon() { }

        /// <summary>
        /// Represents an instance of the TaskbarIcon
        /// </summary>
        public static TaskbarIcon set
        {         
            get
            {
                if (_instance == null)
                {
                    _instance = new TaskbarIcon();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Sets the handle of the main window used for overlay and status.
        /// </summary>
        public IntPtr OwnerHandle
        {
            get
            {
                return _windowHandle;
            }
            set
            {
                _windowHandle = value;
            } 
        }


        #region Icon Overlay, ProgressBar

        /// <summary>
        /// Applies an overlay to a taskbar icon, using OverIconType
        /// </summary>
        /// <param name="iconType">The OverIconType</param>
        public void OverlayIcon(OverIconType iconType)
        {

            switch (iconType)
            {
                case OverIconType.stop:
                    OverlayIcon(Properties.Resources.stop, "Stop");
                    break;
                case OverIconType.play:
                    OverlayIcon(Properties.Resources.play, "Play");
                    break;
                case OverIconType.pause:
                    OverlayIcon(Properties.Resources.pause, "Pause");
                    break;
                case OverIconType.sdPrinting:
                    OverlayIcon(Properties.Resources.sdcardPlay, "SdPlay");
                    break;
                case OverIconType.sdUpload:
                    OverlayIcon(Properties.Resources.sdcardUp, "SdUpload");
                    break;
                case OverIconType.sdPause:
                    OverlayIcon(Properties.Resources.sdcardPause, "SdPause");
                    break;
                default:
                    OverlayIcon(null, "");
                    break;
            }

        }

        /// <summary>
        /// Applies an overlay to a taskbar icon.
        /// </summary>
        /// <param name="icon">The overlay icon</param>
        /// <param name="accessibilityText">String that provides an alt text version of the information conveyed by the overlay, for accessibility purposes</param>
        public void OverlayIcon(Icon icon, string accessibilityText)
        {        
            taskbarList.SetOverlayIcon(
                    OwnerHandle,
                    icon != null ? icon.Handle : IntPtr.Zero,
                    accessibilityText);            
        }

        /// <summary>
        /// Displays or updates a progress bar hosted in a taskbar icon
        /// </summary>
        /// <param name="currentValue"></param>
        public void ProgressValue(int currentValue)
        {
            taskbarList.SetProgressValue(
                OwnerHandle,
                Convert.ToUInt32(currentValue),
                Convert.ToUInt32(100));            
        }

        /// <summary>
        /// Sets the type and state of the progress indicator displayed on a taskbar icon of the main application window.
        /// </summary>
        /// <param name="state">Progress state of the progress icon</param>
        public void ProgressState(ProgressStateType state)
        {
            taskbarList.SetProgressState(OwnerHandle, (ProgressStateType)state);            
        }

        #endregion

        /// <summary>
        /// ITaskbarList4 instance 
        /// </summary>
        private  ITaskbarList4 taskbarList
        {
            get
            {
                if (_taskbarList == null)
                {
                    _taskbarList = (ITaskbarList4)new CTaskbarList();
                    _taskbarList.HrInit();
                }

                return _taskbarList;
            }
        }

    }

    [GuidAttribute("56FDF344-FD6D-11d0-958A-006097C9A090")]
    [ClassInterfaceAttribute(ClassInterfaceType.None)]
    [ComImportAttribute()]
    internal class CTaskbarList { }

    [ComImportAttribute()]
    [GuidAttribute("c43dc798-95d1-4bea-9030-bb99e2983a1a")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ITaskbarList4
    {
        // ITaskbarList
        [PreserveSig]
        void HrInit();
        [PreserveSig]
        void AddTab(IntPtr hwnd);
        [PreserveSig]
        void DeleteTab(IntPtr hwnd);
        [PreserveSig]
        void ActivateTab(IntPtr hwnd);
        [PreserveSig]
        void SetActiveAlt(IntPtr hwnd);
        // ITaskbarList2
        [PreserveSig]
        void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);
        // ITaskbarList3
        [PreserveSig]
        void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);
        [PreserveSig]
        void SetProgressState(IntPtr hwnd, ProgressStateType tbpFlags);
        [PreserveSig]
        void RegisterTab(IntPtr hwndTab, IntPtr hwndMDI);
        [PreserveSig]
        void UnregisterTab(IntPtr hwndTab);
        [PreserveSig]
        void SetTabOrder(IntPtr hwndTab, IntPtr hwndInsertBefore);
        [PreserveSig]
        void SetTabActive(IntPtr hwndTab, IntPtr hwndInsertBefore, uint dwReserved);
        [PreserveSig]
        HResult ThumbBarAddButtons(
            IntPtr hwnd,
            uint cButtons,
            [MarshalAs(UnmanagedType.LPArray)] ThumbButton[] pButtons);
        [PreserveSig]
        HResult ThumbBarUpdateButtons(
            IntPtr hwnd,
            uint cButtons,
            [MarshalAs(UnmanagedType.LPArray)] ThumbButton[] pButtons);       
        [PreserveSig]
        void ThumbBarSetImageList(IntPtr hwnd, IntPtr himl);
        [PreserveSig]
        void SetOverlayIcon(
          IntPtr hwnd,
          IntPtr hIcon,
          [MarshalAs(UnmanagedType.LPWStr)] string pszDescription);
        [PreserveSig]
        void SetThumbnailTooltip(
            IntPtr hwnd,
            [MarshalAs(UnmanagedType.LPWStr)] string pszTip);
        [PreserveSig]
        void SetThumbnailClip(
            IntPtr hwnd,
            IntPtr prcClip);

        // ITaskbarList4
        void SetTabProperties(IntPtr hwndTab, SetTabPropertiesOption stpFlags);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct ThumbButton
    {
        internal const int Clicked = 0x1800;
        [MarshalAs(UnmanagedType.U4)]
        internal ThumbButtonMask Mask;
        internal uint Id;
        internal uint Bitmap;
        internal IntPtr Icon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        internal string Tip;
        [MarshalAs(UnmanagedType.U4)]
        internal ThumbButtonOptions Flags;

        [Flags]
        internal enum ThumbButtonOptions
        {
            Enabled = 0x00000000,
            Disabled = 0x00000001,
            DismissOnClick = 0x00000002,
            NoBackground = 0x00000004,
            Hidden = 0x00000008,
            NonInteractive = 0x00000010
        }
        internal enum ThumbButtonMask
        {
            Bitmap = 0x1,
            Icon = 0x2,
            Tooltip = 0x4,
            THB_FLAGS = 0x8
        }

    }

    internal enum ProgressStateType
    {
        NoProgress = 0,
        Indeterminate = 0x1,
        Normal = 0x2,
        Error = 0x4,
        Paused = 0x8
    }

    internal enum OverIconType
    {
        NULL,
        stop,
        play,
        pause,
        sdPrinting,
        sdUpload,
        sdPause
    }

    internal enum SetTabPropertiesOption
    {
        None = 0x0,
        UseAppThumbnailAlways = 0x1,
        UseAppThumbnailWhenActive = 0x2,
        UseAppPeekAlways = 0x4,
        UseAppPeekWhenActive = 0x8
    }

    internal enum HResult
    {
        Ok = 0x0000,
        False = 0x0001,
        InvalidArguments = unchecked((int)0x80070057),
        OutOfMemory = unchecked((int)0x8007000E),
        NoInterface = unchecked((int)0x80004002),
        Fail = unchecked((int)0x80004005),
        ElementNotFound = unchecked((int)0x80070490),
        TypeElementNotFound = unchecked((int)0x8002802B),
        NoObject = unchecked((int)0x800401E5),
        Win32ErrorCanceled = 1223,
        Canceled = unchecked((int)0x800704C7),
        ResourceInUse = unchecked((int)0x800700AA),
        AccessDenied = unchecked((int)0x80030005)
    }

}
