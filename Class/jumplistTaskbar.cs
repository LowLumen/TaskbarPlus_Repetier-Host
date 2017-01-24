using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TaskbarPlus
{


    internal sealed class JumpListClass : IDisposable
    {

        private List<IJumpListTask> _tasks;

        private AutomaticListType _enableAutoList = AutomaticListType.Frequent;

        private ICustomDestinationList _customDestinationList;

        private uint _maxItemsInList;

        private Guid IID_IObjectArray = new Guid("92CA9DCD-5622-4BBA-A805-5E9F541BD8C9");

        public AutomaticListType enableAutomaticList { set { _enableAutoList = value; } }


        // COSTRUCTOR
        public JumpListClass(string appUserModelID)
        {

            _tasks = new List<IJumpListTask>();

            _customDestinationList = (ICustomDestinationList)new CDestinationList();

            _customDestinationList.SetAppID(appUserModelID);

            RefreshMaxItems();
        }


        /// <summary>
        /// Adds a new task to the jump list.
        /// </summary>
        /// <param name="task">JumpListTask item</param>
        public void AddUserTask(IJumpListTask task)
        {
            _tasks.Add(task);
        }

        /// <summary>
        /// Clear the Recent/Frequent list from the jump list
        /// </summary>
        public void ClearAutomaticList()
        {
            IApplicationDestinations destinations = (IApplicationDestinations) new CApplicationDestinations();
            try
            {
                //This does not remove pinned items.
                destinations.RemoveAllDestinations();
            }
            catch (System.IO.FileNotFoundException)
            {
            }
        }

        /// <summary>
        /// Refresh the jump list in the taskbar
        /// </summary>
        /// <returns></returns>
        public bool RefreshJumplist()
        {
            if (!BeginList())
                return false;

            RefreshTasks(_customDestinationList);

            switch (_enableAutoList)
            {
                case AutomaticListType.Frequent:
                    _customDestinationList.AppendKnownCategory(KNOWNDESTCATEGORY.KDC_FREQUENT);
                    break;
                case AutomaticListType.Recent:
                    _customDestinationList.AppendKnownCategory(KNOWNDESTCATEGORY.KDC_RECENT);
                    break;
            }

            _customDestinationList.CommitList();
            
            return true;
        }

        private bool BeginList()
        {
            object obj;
            _customDestinationList.BeginList
                (
                out _maxItemsInList,
                ref IID_IObjectArray,
                out obj
                );

            return true;
        }


        private void RefreshTasks(ICustomDestinationList destinationList)
        {
            if (_tasks.Count == 0)
                return;

            IObjectCollection taskCollection = (IObjectCollection)new CEnumerableObjectCollection();
            foreach (IJumpListTask task in _tasks)
            {
                taskCollection.AddObject(task.GetShellRepresentation());
            }
            destinationList.AddUserTasks((IObjectArray)taskCollection);
        }

        private void RefreshMaxItems()
        {
            object obj;
            _customDestinationList.BeginList
                (
                out _maxItemsInList,
                ref IID_IObjectArray,
                out obj
                );
            _customDestinationList.AbortList();
        }


        public void Dispose()
        {
            if (_customDestinationList != null)
                Marshal.ReleaseComObject(_customDestinationList);
        }

    }


    // ENUM
    internal enum KNOWNDESTCATEGORY
    {
        KDC_FREQUENT = 1,
        KDC_RECENT
    }
    internal enum AutomaticListType
    {
        NONE = -1,
        Recent = 0,
        Frequent
    }



    internal sealed class ShellLink : IJumpListTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Path { get; set; }
        public string Arguments { get; set; }
        public string IconLocation { get; set; }
        public int IconIndex { get; set; }
        /// <summary>
        /// Gets or sets menu separator flag.
        /// If this flag is set, all other properties are ignored.
        /// </summary>
        public bool IsSeparator { get; set; }

        public object GetShellRepresentation()
        {
            IShellLinkW shellLink = (IShellLinkW)new CShellLink();
            IPropertyStore propertyStore = (IPropertyStore)shellLink;
            PropVariant propVariant = new PropVariant();

            if (IsSeparator)
            {
                propVariant.SetValue(true);
                propertyStore.SetValue(ref PropertyKey.PKEY_AppUserModel_IsDestListSeparator, ref propVariant);
                propVariant.Clear();
            }
            else
            {
                shellLink.SetPath(Path);
                if (!string.IsNullOrEmpty(Description))
                    shellLink.SetDescription(Description);
                if (!String.IsNullOrEmpty(IconLocation))
                    shellLink.SetIconLocation(IconLocation, IconIndex);
                if (!String.IsNullOrEmpty(Arguments))
                    shellLink.SetArguments(Arguments);

                propVariant.SetValue(Title);
                propertyStore.SetValue(ref PropertyKey.PKEY_Title, ref propVariant);
                propVariant.Clear();
            }

            propertyStore.Commit();

            return shellLink;
        }
        public override bool Equals(object target)
        {
            if (target == null || target.GetType() != typeof(ShellLink))
                return false;
            ShellLink ca = (ShellLink)target;
            return ca.Path.Equals(this.Path);
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }
    }

    internal interface IJumpListTask
    {
        string Title { get; }
        string Path { get; }
        string Arguments { get; }
        object GetShellRepresentation();
    }

    [ComImport,
    GuidAttribute("000214F9-0000-0000-C000-000000000046"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellLinkW
    {
        // http://www.pinvoke.net/default.aspx/Interfaces/IShellLinkW.html
        void GetPath(
            [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
            int cchMaxPath,
            IntPtr pfd,
            uint fFlags);
        void GetIDList(out IntPtr ppidl);
        void SetIDList(IntPtr pidl);
        void GetDescription(
            [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
            int cchMaxName);
        void SetDescription(
            [MarshalAs(UnmanagedType.LPWStr)] string pszName);
        void GetWorkingDirectory(
            [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir,
            int cchMaxPath);
        void SetWorkingDirectory(
            [MarshalAs(UnmanagedType.LPWStr)] string pszDir);
        void GetArguments(
            [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs,
            int cchMaxPath);
        void SetArguments(
            [MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
        void GetHotKey(out short wHotKey);
        void SetHotKey(short wHotKey);
        void GetShowCmd(out uint iShowCmd);
        void SetShowCmd(uint iShowCmd);
        void GetIconLocation(
            [Out(), MarshalAs(UnmanagedType.LPWStr)] out StringBuilder pszIconPath,
            int cchIconPath,
            out int iIcon);
        void SetIconLocation(
            [MarshalAs(UnmanagedType.LPWStr)] string pszIconPath,
            int iIcon);
        void SetRelativePath(
            [MarshalAs(UnmanagedType.LPWStr)] string pszPathRel,
            uint dwReserved);
        void Resolve(IntPtr hwnd, uint fFlags);
        void SetPath(
            [MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }

    [GuidAttribute("00021401-0000-0000-C000-000000000046")]
    [ClassInterfaceAttribute(ClassInterfaceType.None)]
    [ComImportAttribute()]
    internal class CShellLink { }

    [ComImportAttribute()]
    [GuidAttribute("92CA9DCD-5622-4BBA-A805-5E9F541BD8C9")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IObjectArray
    {
        void GetCount(out uint cObjects);
        void GetAt(
            uint iIndex,
            ref Guid riid,
            [Out(), MarshalAs(UnmanagedType.Interface)] out object ppvObject);
    }

    [ComImportAttribute()]
    [GuidAttribute("5632B1A4-E38A-400A-928A-D4CD63230295")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IObjectCollection
    {
        // IObjectArray
        [PreserveSig]
        void GetCount(out uint cObjects);
        [PreserveSig]
        void GetAt(
            uint iIndex,
            ref Guid riid,
            [Out(), MarshalAs(UnmanagedType.Interface)] out object ppvObject);

        // IObjectCollection
        void AddObject(
            [MarshalAs(UnmanagedType.Interface)] object pvObject);
        void AddFromArray(
            [MarshalAs(UnmanagedType.Interface)] IObjectArray poaSource);
        void RemoveObject(uint uiIndex);
        void Clear();
    }

    [ComImport,
    Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertyStore
    {
        void GetCount(out UInt32 cProps);
        void GetAt(
            UInt32 iProp,
            [MarshalAs(UnmanagedType.Struct)] out PropertyKey pkey);
        void GetValue(
            [In, MarshalAs(UnmanagedType.Struct)] ref PropertyKey pkey,
            [Out(), MarshalAs(UnmanagedType.Struct)] out PropVariant pv);
        void SetValue(
            [In, MarshalAs(UnmanagedType.Struct)] ref PropertyKey pkey,
            [In, MarshalAs(UnmanagedType.Struct)] ref PropVariant pv);
        void Commit();
    }

    [GuidAttribute("2D3468C1-36A7-43B6-AC24-D3F02FD9607A")]
    [ClassInterfaceAttribute(ClassInterfaceType.None)]
    [ComImportAttribute()]
    internal class CEnumerableObjectCollection { }

    [ComImportAttribute()]
    [GuidAttribute("6332DEBF-87B5-4670-90C0-5E57B408A49E")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ICustomDestinationList
    {
        void SetAppID(
            [MarshalAs(UnmanagedType.LPWStr)] string pszAppID);
        [PreserveSig]
        void BeginList(
            out uint cMaxSlots,
            ref Guid riid,
            [Out(), MarshalAs(UnmanagedType.Interface)] out object ppvObject);

        [PreserveSig]
        void AppendCategory(
            [MarshalAs(UnmanagedType.LPWStr)] string pszCategory,
            [MarshalAs(UnmanagedType.Interface)] IObjectArray poa);

        [PreserveSig]
        void AppendKnownCategory([MarshalAs(UnmanagedType.I4)] KNOWNDESTCATEGORY category);

        [PreserveSig]
        void AddUserTasks([MarshalAs(UnmanagedType.Interface)] IObjectArray poa);

        void CommitList();

        void GetRemovedDestinations(
            ref Guid riid,
            [Out(), MarshalAs(UnmanagedType.Interface)] out object ppvObject);
        void DeleteList(
            [MarshalAs(UnmanagedType.LPWStr)] string pszAppID);
        void AbortList();
    }

    [GuidAttribute("77F10CF0-3DB5-4966-B520-B7C54FD35ED6")]
    [ClassInterfaceAttribute(ClassInterfaceType.None)]
    [ComImportAttribute()]
    internal class CDestinationList { }

    [ComImportAttribute()]
    [GuidAttribute("12337D35-94C6-48A0-BCE7-6A9C69D4D600")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IApplicationDestinations
    {
        void SetAppID(
            [MarshalAs(UnmanagedType.LPWStr)] string pszAppID);
        void RemoveDestination(
            [MarshalAs(UnmanagedType.Interface)] object pvObject);
        void RemoveAllDestinations();
    }

    [GuidAttribute("86C14003-4D6B-4EF3-A7B4-0506663B2E68")]
    [ClassInterfaceAttribute(ClassInterfaceType.None)]
    [ComImportAttribute()]
    internal class CApplicationDestinations { }


    // STRUCT

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct PropertyKey
    {
        public Guid fmtid;
        public uint pid;

        public PropertyKey(Guid fmtid, uint pid)
        {
            this.fmtid = fmtid;
            this.pid = pid;
        }

        public static PropertyKey PKEY_Title = new PropertyKey(new Guid("F29F85E0-4FF9-1068-AB91-08002B27B3D9"), 2);
        public static PropertyKey PKEY_AppUserModel_IsDestListSeparator = new PropertyKey(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 6);
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct PropVariant : IDisposable
    {
        [FieldOffset(0)]
        private ushort vt;
        [FieldOffset(8)]
        private IntPtr pointerValue;
        [FieldOffset(8)]
        private byte byteValue;
        [FieldOffset(8)]
        private long longValue;
        [FieldOffset(8)]
        private short boolValue;
        [MarshalAs(UnmanagedType.Struct)]
        [FieldOffset(8)]
        private CALPWSTR calpwstr;


        public VarEnum VarType
        {
            get { return (VarEnum)vt; }
        }
        public string GetValue()
        {
            return Marshal.PtrToStringUni(this.pointerValue);
        }



        public void SetValue(String val)
        {
            this.Clear();
            this.vt = (ushort)VarEnum.VT_LPWSTR;
            this.pointerValue = Marshal.StringToCoTaskMemUni(val);
        }
        public void SetValue(bool val)
        {
            this.Clear();
            this.vt = (ushort)VarEnum.VT_BOOL;
            this.boolValue = val ? (short)-1 : (short)0;
        }


        public void Clear()
        {
            PropVariantClear(ref this);
        }

        public void Dispose()
        {
            this.Clear();

            GC.SuppressFinalize(this);
        }

        // p/invoke

        [DllImport("ole32.dll")]
        private static extern int PropVariantClear(ref PropVariant pvar);
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct CALPWSTR
    {
        [FieldOffset(0)]
        internal uint cElems;
        [FieldOffset(4)]
        internal IntPtr pElems;
    }

}
