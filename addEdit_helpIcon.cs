using System.Windows.Forms;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Drawing;


// This code is used for extract icon for the preview in the addEdit form

namespace TaskbarPlus
{
    public partial class addEdit : Form
    {

        #region icon p/invoke

        [DllImport("Shlwapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern uint AssocQueryString(AssocF flags, AssocStr str, string pszAssoc, string pszExtra,
        [Out] StringBuilder pszOut, [In][Out] ref uint pcchOut);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int PickIconDlg(IntPtr hwndOwner, StringBuilder lpstrFile, int nMaxFile, ref int lpdwIconIndex);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern uint ExtractIconEx(string szFileName, int nIconIndex,
           IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW",
            SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int GetPrivateProfileString(
          string lpSection,
          string lpKey,
          string lpDefault,
          StringBuilder lpReturnString,
          int nSize,
          string lpFileName);

        [DllImport("user32")]
        private static extern int DestroyIcon(IntPtr hIcon);

        #endregion

        #region enumeration

        [Flags]
        private enum AssocF
        {
            Init_NoRemapCLSID = 0x1,
            Init_ByExeName = 0x2,
            Open_ByExeName = 0x2,
            Init_DefaultToStar = 0x4,
            Init_DefaultToFolder = 0x8,
            NoUserSettings = 0x10,
            NoTruncate = 0x20,
            Verify = 0x40,
            RemapRunDll = 0x80,
            NoFixUps = 0x100,
            IgnoreBaseClass = 0x200
        }

        private enum AssocStr
        {
            Command = 1,
            Executable,
            FriendlyDocName,
            FriendlyAppName,
            NoOpen,
            ShellNewValue,
            DDECommand,
            DDEIfExec,
            DDEApplication,
            DDETopic
        }

        #endregion

        /// <summary>
        /// Counts the icons contained in a file
        /// </summary>
        /// <param name="filePath">the file path</param>
        /// <returns>the number of icon </returns>
        private int countIcons(string filePath)
        {
            if (!File.Exists(filePath))
                return 0;

            return (int)ExtractIconEx(filePath, -1, null, null, 0);
        }

        /// <summary>
        /// Extract the Icon from a specified file
        /// </summary>
        /// <param name="iconPath"></param>
        /// <param name="Index"></param>
        /// <returns>return the Drawing.Icon</returns>
        private Icon ExtractIconFromFile(String iconPath, Int32 Index)
        {
            if (!File.Exists(iconPath))
                return null;

            Icon tmpIcon = null;
            IntPtr iconHandle;

            iconHandle = ExtractIcon(IntPtr.Zero, iconPath, Index);

            if (iconHandle != IntPtr.Zero)
            {
                tmpIcon = Icon.FromHandle(iconHandle).Clone() as Icon;
                DestroyIcon(iconHandle);
            }

            return tmpIcon;
        }

        /// <summary>
        /// Search custom folder icon 
        /// Get the icon path from a specified folder searching in the desktop.ini file.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns>the path and index that contain the icon path,index return "" if not found</returns>
        private string GetIconFromDesktopIni(string folderPath)
        {
            folderPath = Path.Combine(folderPath, "desktop.ini");

            if (!File.Exists(folderPath))
                return "";

            StringBuilder result = new StringBuilder(1023);

            GetPrivateProfileString(".ShellClassInfo", "IconResource", "", result, 1023, folderPath);
            return result.ToString();
        }

        /// <summary>
        /// Get the icon path associated with a specified extension file.
        /// </summary>
        /// <param name="extension">the extension, es: .stl .rar .txt</param>
        /// <returns>the path and index that contain the icon, return "" if not found</returns>
        private string GetIconFromExtension(string extension)
        {
            extension = extension.ToLower();

            string iconPath = "";

            using (RegistryKey rkRoot = Registry.ClassesRoot)
            {
                //Get sub keys
                string[] keyNames = rkRoot.GetSubKeyNames();

                //Find the file icon.
                foreach (string keyName in keyNames)
                {
                    if (String.IsNullOrEmpty(keyName))
                        continue;

                    int indexOfPoint = keyName.IndexOf(extension);

                    //If this key is not a file extension(eg, .stl), skip it.
                    if (indexOfPoint != 0)
                        continue;

                    using (RegistryKey rkFileType = rkRoot.OpenSubKey(keyName))
                    {
                        if (rkFileType == null)
                            continue;

                        //Gets the default value of this key that contains the information of file type.
                        object defaultValue = rkFileType.GetValue("");
                        if (defaultValue == null)
                            continue;

                        //Go to the key that specifies the default icon associates with this file type.
                        string defaultIcon = defaultValue.ToString() + "\\DefaultIcon";
                        using (RegistryKey rkFileIcon = rkRoot.OpenSubKey(defaultIcon))
                        {
                            if (rkFileIcon != null)
                            {
                                //Get the file contains the icon and the index of the icon in that file.
                                object value = rkFileIcon.GetValue("");
                                if (value != null)
                                {
                                    //Clear all unecessary " sign in the string to avoid error.
                                    iconPath = value.ToString().Replace("\"", "");
                                    //return fileParam;

                                }
                            }
                        }
                    }
                }

            }


            string[] split = iconPath.Split(',');

            if (string.IsNullOrEmpty(iconPath) || split.Count() < 2)
            {

                iconPath = GetAssociateProgram(extension);

                if (!string.IsNullOrEmpty(iconPath))
                    iconPath = iconPath + ",0";

            }

            return iconPath;
        }

        private string GetAssociateProgram(string extension)
        {

            uint pcchOut = 0;

            AssocQueryString(AssocF.Verify, AssocStr.Executable, extension, null, null, ref pcchOut);

            StringBuilder pszOut = new StringBuilder((int)pcchOut);

            AssocQueryString(AssocF.Verify, AssocStr.Executable, extension, null, pszOut, ref pcchOut);


            string doc = pszOut.ToString();

            return doc;

        }
    }
}
