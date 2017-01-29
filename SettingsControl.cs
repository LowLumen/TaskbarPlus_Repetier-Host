using System;
using System.Linq;
using System.Windows.Forms;
using RepetierHostExtender.interfaces;
using RepetierHostExtender.utils;

namespace TaskbarPlus
{

    public partial class SettingsControl : UserControl, IHostComponent, IHostConfig
    {
        private IHost host = null;
        private bool _controlLoaded = false;

        // Costructor
        public SettingsControl()
        {
            InitializeComponent();
        }

        // loading control, called only after plugin tab is selected in the preferences form!!
        private void SettingsControl_Load(object sender, EventArgs e)
        {

            // if windows OS version is less than windows 7 disable all and print "error"
            if (!Settings.supportedOS)
            {
                // Disable all
                groupBox_icon.Enabled = false;
                groupBox_jumpList.Enabled = false;
                button_AddNew.Enabled = false;
                // print error
                label_title.Text = "Only windows 7 OS and higher version are supported";
                label_title.ForeColor = System.Drawing.Color.Red;
                return;
            }

            ///--- LANGUAGE TRANSLATION ---///

            // load translation
            load_Translations();
            // set language changed event
            host.languageChanged += load_Translations;
            // Add the plugin version in the top left label ( add after translation )
            //if language is changed the plugin version is deleted, but after retart repetier-host is ok!!
            // best is to place the version number into separed label but the problem is only for the changing language and user make this only 1 time...
            linkLabel_version.Text += Settings.pluginVersion;


            ///--- FILL ALL WITH DATA FROM USER SETTINGS ---///

            // Settings Taskbar icon
            checkBox_iconOverlay.Checked = Settings.iconOverlay;
            // Settings Progress Bar
            checkBox_progressBar.Checked = Settings.progressBar;

            // load Taskbar jump-list list directly in the listview
            load_List_From_Registry();

            // This param is not saved in the registry, after restarting default setting is used (settings.cs)
            checkBox_debug.Checked = Settings.debugON;
            checkBox_debug.Visible = host.ExpertMode;     
            // Set mode changed event
            host.ExpertModeChanged += expertModeChanged;
       
            _controlLoaded = true;      
        }

        #region IHostComponent implementation

        public void Connect(IHost _host) { host = _host; }
        // Name inside component repository
        public string ComponentName { get { return "TaskbarPlus"; } }
        // Name for tab
        public string ComponentDescription { get { return "TaskbarPlus"; } }
        // Where to add it.
        public PreferredComponentPositions PreferredPosition
        {
            get { return PreferredComponentPositions.CONFIGURATION; }
        }

        //Gets called when the component comes into view. For tabs this means when the tab gets selected.
        // not called for PreferredComponentPositions.CONFIGURATION ?!?
        public void ComponentActivated() { }

        //Associated ThreeDView object to show in the 3d view. Return null for no assiciation. 
        public RepetierHostExtender.geom.ThreeDView Associated3DView { get; set; }

        // Used for positioning the tab.
        public int ComponentOrder { get { return 8000; } }

        // Return the UserControl.
        public Control ComponentControl { get { return this; } }

        #endregion

        #region IHostConfig  implementation

        //Gets called, when the configuration panel gets closed. Use this to store your configuration data. 
        // If preference form is closed befor the plugin tab is selected Connect() is not called.
        public void ComponentDeactivated() { save_AllInRegistry(); }
        //Returns icon for the left list. Size should be 32x32 pixel. Can be null. 
        public ImageList ComponentIconList { get { return imageList_menuIcon; } }

        public bool CanClose { get { return true; } }

        #endregion


        // Debug options
        private void expertModeChanged(bool val)
        {
            checkBox_debug.Visible = val;
        }
        private void checkBox_debug_CheckedChanged(object sender, EventArgs e)
        {
            Settings.debugON = checkBox_debug.Checked;
        }

        /// open plugin Homepage
        private void linkLabel_version_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open http link with repetier api
            host.OpenLink(Settings.homepage);
        }

        // Enable or disable buttons if item in the listview is selected or not
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // check if one item in the listview is selected
            if (listView1.SelectedItems.Count == 0)
            {
                // no item selected, disable all buttons and RETURN!
                button_delete.Enabled = false;
                button_edit.Enabled = false;
                button_down.Enabled = false;
                button_up.Enabled = false;

                return;
            }

            /// At least one item is selected, enable specific buttons

            // Enable delete/Edit buttons
            button_delete.Enabled = true;
            button_edit.Enabled = true;

            // Enable or disable UP/DOWN buttons
            if (listView1.SelectedItems[0].Index == 0)
            {
                //selected item is the first in the list, disable UP button
                button_up.Enabled = false;
                // Check if only 1 item is in the listview
                if (listView1.Items.Count > 1)
                {
                    //multiple items in the list, enable DOWN button
                    button_down.Enabled = true;
                }       
            }
            // selected item is not the first in the list?
            else if (listView1.SelectedItems[0].Index > 0)
            {
                // enable UP button
                button_up.Enabled = true;

                // check if selected item is not the last item
                if (listView1.Items.Count > listView1.SelectedItems[0].Index + 1)
                {
                    // is not the last, enable DOWN button
                    button_down.Enabled = true;
                }
            }

        }
        private void comboBox_automaticList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if is Repetier-Host that opened or updated the control
            if (!_controlLoaded)
                return;
            // if is the user that changed the list update the jumplist
            add_JumpList_to_taskbar();
        }
        private void checkBox_iconOverlay_CheckedChanged(object sender, EventArgs e)
        {
            // Check if is Repetier-Host that opened or updated the control
            if (!_controlLoaded)
                return;
            // update the value in the plugin settings
            Settings.iconOverlay = checkBox_iconOverlay.Checked;

            // if disabled or no connection disable the icon overlay and RETURN
            if (!Settings.iconOverlay || !host.Connection.connector.IsConnected())
            {
                TaskbarIcon.set.OverlayIcon(OverIconType.NULL);
                return;
            }

            // if job is not running set stop icon and RETURN
            if (!host.Connection.connector.IsJobRunning())
            {
                TaskbarIcon.set.OverlayIcon(OverIconType.stop);
                return;
            }

            // if in pause set pause ICON and RETURN
            if (host.Connection.connector.IsPaused)
            {
                TaskbarIcon.set.OverlayIcon(OverIconType.pause);
                return;
            }

            // printer is connected and job is running, add play icon
            TaskbarIcon.set.OverlayIcon(OverIconType.play);

            // SD card icon are not implemented
            // but if SDjob/SDupload are running, are updated in the next ProgressChanged event

        }
        private void checkBox_progressBar_CheckedChanged(object sender, EventArgs e)
        {
            // Check if is Repetier-Host that opened or updated the control
            if (!_controlLoaded)
                return;
            // update plugin setting
            Settings.progressBar = checkBox_progressBar.Checked;
            // update the progress bar in the taskbar
            if (!Settings.progressBar)
                TaskbarIcon.set.ProgressState(ProgressStateType.NoProgress);
            
        }


        #region items list buttons click event

        private void button_AddNew_Click(object sender, EventArgs e)
        {
            // used for pass data between forms
            string[] allParams = null;

            // Open the new form 
            using (addEdit OpenForm = new addEdit())
            {
                OpenForm.ShowDialog();
                allParams = OpenForm.allParams;
            }
            // if no params from form return
            if (allParams == null) return;

            // Add the new item in the listview

            // build listview item
            ListViewItem item = new ListViewItem(allParams[0]);//name
            // build subitems
            string[] subItems = 
            { 
                allParams[1], //description
                allParams[2], //path
                allParams[3], //arguments
                allParams[4]  //icon
            };
            // add subitems do Item
            item.SubItems.AddRange(subItems);
            //add Tooltip to item
            item.ToolTipText = buildItemToolTips(item);
            // add item in the listview
            listView1.Items.Add(item);
            // update buttons status
            listView1_SelectedIndexChanged(null, null);


            // update the jumplist in the taskbar
            add_JumpList_to_taskbar();
        }
        private void button_edit_Click(object sender, EventArgs e)
        {
            // no item selected in the list
            if (listView1.SelectedItems.Count == 0)
                return;

            // build an array with all settings, for sending it to the new form
            // 0: Name; 1:description; 2:path; 3:arguments; 4:icon
            string[] allParams = new string[5];
            allParams[0] = listView1.SelectedItems[0].SubItems[0].Text;
            allParams[1] = listView1.SelectedItems[0].SubItems[1].Text;
            allParams[2] = listView1.SelectedItems[0].SubItems[2].Text;
            allParams[3] = listView1.SelectedItems[0].SubItems[3].Text;
            allParams[4] = listView1.SelectedItems[0].SubItems[4].Text;

            // Open the new form 
            using (addEdit OpenForm = new addEdit(allParams))
            {
                OpenForm.ShowDialog();
                allParams = OpenForm.allParams; //get params from form
            }
            if (allParams == null) return;

            // block listview
            listView1.BeginUpdate();
            // update item subitems with new params
            listView1.SelectedItems[0].SubItems[0].Text = allParams[0];
            listView1.SelectedItems[0].SubItems[1].Text = allParams[1];
            listView1.SelectedItems[0].SubItems[2].Text = allParams[2];
            listView1.SelectedItems[0].SubItems[3].Text = allParams[3];
            listView1.SelectedItems[0].SubItems[4].Text = allParams[4];
            listView1.SelectedItems[0].ToolTipText = buildItemToolTips(listView1.SelectedItems[0]);
            // unblock/update listview
            listView1.EndUpdate();

            // update the jumplist in the taskbar
            add_JumpList_to_taskbar();
        }
        private void button_delete_Click(object sender, EventArgs e)
        {
            // no item selected in the list
            if (listView1.SelectedItems.Count == 0)
                return;

            // Delete item from listview
            listView1.BeginUpdate();
            listView1.Items.Remove(listView1.SelectedItems[0]);
            listView1.EndUpdate();
            // update the jumplist in the taskbar
            add_JumpList_to_taskbar();

        }
        private void button_reload_Click(object sender, EventArgs e)
        {
            // upload the jumplist in the taskbar
            add_JumpList_to_taskbar();
        }
        private void button_up_Click(object sender, EventArgs e)
        {
            int currentIndex = listView1.SelectedItems[0].Index;

            if (currentIndex > 0)
            {
                // move up the item in the list
                ListViewItem item = listView1.Items[currentIndex];
                listView1.BeginUpdate();
                listView1.Items.RemoveAt(currentIndex);
                listView1.Items.Insert(currentIndex - 1, item);
                listView1.EndUpdate();
                // update the jumplist in the taskbar
                add_JumpList_to_taskbar();
            }

        }
        private void button_down_Click(object sender, EventArgs e)
        {
            int currentIndex = listView1.SelectedItems[0].Index;

            if (listView1.Items.Count > currentIndex + 1)
            {
                // move down the item in the list
                ListViewItem item = listView1.Items[currentIndex];
                listView1.BeginUpdate();
                listView1.Items.RemoveAt(currentIndex);
                listView1.Items.Insert(currentIndex + 1, item);
                listView1.EndUpdate();

                // update the jumplist in the taskbar
                add_JumpList_to_taskbar();
            }

        }
        private void button_deleteAuto_Click(object sender, EventArgs e)
        {
            /// Clear automatic list (Recent/Frequent)
            using (JumpListClass _jumpList = new JumpListClass(Settings.AppUserModelIDs))
            {
                _jumpList.ClearAutomaticList();
            }
        }

        #endregion


        /// <summary>
        /// Load jump list data from the registry and add it into listview 
        /// </summary>
        private void load_List_From_Registry()
        {
            IRegMemoryFolder Ireg = host.GetRegistryFolder("TaskbarPlus");

            // update automatic list setting
            comboBox_automaticList.SelectedIndex = Ireg.GetInt("automaticList", 1);
            
            // get jumplist items list
            string[] regItems = Ireg.GetString("linkList", "").Split(new string[] { "@|;" }, StringSplitOptions.None); ;
            string[] allParams;

            // build items and add into listview
            foreach (string regString in regItems)
            {
                allParams = regString.Split(new string[] { ";|@" }, StringSplitOptions.None);
                // check if all params are present
                if (allParams.Count() < 5) 
                    continue;

                // build item
                ListViewItem listItem = new ListViewItem(allParams[0]);
                string[] subItems =  { allParams[1], 
                allParams[2], 
                allParams[3], 
                allParams[4] };

                listItem.SubItems.AddRange(subItems);
                listItem.ToolTipText = buildItemToolTips(listItem);
                // Add item to listview
                listView1.Items.Add(listItem); 

            }

        }

        private string buildItemToolTips(ListViewItem item)
        {
            string toolTips;

            //name
            toolTips = listView1.Columns[0].Text.ToUpper() + ": " + item.SubItems[0].Text;
            //description
            if ( item.SubItems[1].Text != "")
                toolTips += "\r\n" + listView1.Columns[1].Text.ToUpper() + ": " + item.SubItems[1].Text;
            //path
            toolTips += "\r\n" + listView1.Columns[2].Text.ToUpper() + ": " + item.SubItems[2].Text;
            //arguments
            if (item.SubItems[3].Text != "")
                toolTips += "\r\n" + listView1.Columns[3].Text.ToUpper() + ": " + item.SubItems[3].Text;
            //icon
            if (item.SubItems[4].Text != "")
                toolTips += "\r\n" + listView1.Columns[4].Text.ToUpper() + ": " + item.SubItems[4].Text;

            return toolTips;
        }

        /// <summary>
        /// Add custom task in the jumplist taking them from listview1
        /// </summary>
        private void add_JumpList_to_taskbar()
        {
            // if the form is not loaded no change is made...
            if (!_controlLoaded)
                return;
            
            using (JumpListClass _jumpList = new JumpListClass(Settings.AppUserModelIDs))
            {
                // build the link array
                foreach (ListViewItem item in listView1.Items)
                {
                   string[] icon = item.SubItems[4].Text.Split(',');
                    _jumpList.AddUserTask(new ShellLink
                    {
                        Path = item.SubItems[2].Text,
                        Title = item.SubItems[0].Text,
                        Description = item.SubItems[1].Text,
                        Arguments = item.SubItems[3].Text,
                        IconLocation = (icon.Count() >= 1) ? icon[0]: "",
                        IconIndex = (icon.Count() > 1) ? Convert.ToInt32( icon[1]) : 0
                    });

                }
                
                switch (comboBox_automaticList.SelectedIndex)
                {
                    case 1:
                        _jumpList.enableAutomaticList = AutomaticListType.Frequent;
                        break;
                    case 2:
                        _jumpList.enableAutomaticList = AutomaticListType.Recent;
                        break;
                    default:
                        _jumpList.enableAutomaticList = AutomaticListType.NONE;
                        break;
                }

                _jumpList.RefreshJumplist();

            }

        }

        /// <summary>
        /// Save settings in the registry
        /// </summary>
        private void save_AllInRegistry()
        {
            // if the control is not loded no change is made or OS not suported
            if (!_controlLoaded)
                return;
            
            IRegMemoryFolder Ireg = host.GetRegistryFolder("TaskbarPlus");

            // Taskbar icon
            Ireg.SetBool("iconOverlay", checkBox_iconOverlay.Checked);
            Ireg.SetBool("progressBar", checkBox_progressBar.Checked);

            // Jump list
            Ireg.SetInt("automaticList", comboBox_automaticList.SelectedIndex);

            string regString = "";
            foreach (ListViewItem item in listView1.Items)
            {
                regString += "@|;" + item.SubItems[0].Text;
                regString += ";|@" + item.SubItems[1].Text;
                regString += ";|@" + item.SubItems[2].Text;
                regString += ";|@" + item.SubItems[3].Text;
                regString += ";|@" + item.SubItems[4].Text;
            }

            Ireg.SetString("linkList", regString.TrimStart('|'));
        }

        /// <summary>
        /// translate all text in the Control
        /// </summary>
        private void load_Translations()
        {
            // 0.Nothing 1.Frequent 2.Recent
            comboBox_automaticList.Items[0] = Trans.T("TP_NONE");
            comboBox_automaticList.Items[1] = Trans.T("TP_FREQUENT");
            comboBox_automaticList.Items[2] = Trans.T("TP_RECENT");
            toolTip1.SetToolTip(comboBox_automaticList, Trans.T("TP_CB_AUTO_TOOL"));

            //label
            label_title.Text = Trans.T("TP_L_TITLE");
            linkLabel_version.Text = Trans.T("TP_L_VERSION");
            label_taskList.Text = Trans.T("TP_L_TASKLIST");
            label_autoList.Text = Trans.T("TP_L_AUTOLIST");

            //button
            button_AddNew.Text = Trans.T("TP_B_ADD");
            toolTip1.SetToolTip(button_AddNew, Trans.T("TP_B_ADD_TOOL"));
            button_edit.Text = Trans.T("TP_B_EDIT");
            toolTip1.SetToolTip(button_edit, Trans.T("TP_B_EDIT_TOOL"));
            button_delete.Text = Trans.T("TP_B_DELETE");
            toolTip1.SetToolTip(button_delete, Trans.T("TP_B_DELETE_TOOL"));
            button_reload.Text = Trans.T("TP_B_RELOAD");
            toolTip1.SetToolTip(button_reload, Trans.T("TP_B_RELOAD_TOOL"));
            toolTip1.SetToolTip(button_up, Trans.T("TP_B_UP_TOOL"));
            toolTip1.SetToolTip(button_down, Trans.T("TP_B_DOWN_TOOL"));
            button_deleteAuto.Text = Trans.T("TP_B_DELAUTO");
            toolTip1.SetToolTip(button_deleteAuto, Trans.T("TP_B_DELAUTO_TOOL"));

            // group
            groupBox_jumpList.Text = Trans.T("TP_G_JUMPLIST");
            groupBox_icon.Text = Trans.T("TP_G_ICON");

            checkBox_iconOverlay.Text = Trans.T("TP_C_ICONOVER");
            toolTip1.SetToolTip(checkBox_iconOverlay, Trans.T("TP_C_ICONOVER_TOOL"));
            checkBox_progressBar.Text = Trans.T("TP_C_PROGRESSBAR");
            toolTip1.SetToolTip(checkBox_progressBar, Trans.T("TP_C_PROGRESSBAR_TOOL"));


            listView1.Columns[0].Text = Trans.T("TP_COLU_1");
            listView1.Columns[1].Text = Trans.T("TP_COLU_2");
            listView1.Columns[2].Text = Trans.T("TP_COLU_3");
            listView1.Columns[3].Text = Trans.T("TP_COLU_4");
            listView1.Columns[4].Text = Trans.T("TP_COLU_5");

        }

    }
}
