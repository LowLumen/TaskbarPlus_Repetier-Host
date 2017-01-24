namespace TaskbarPlus
{
    partial class SettingsControl
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsControl));
            this.button_AddNew = new System.Windows.Forms.Button();
            this.imageList_menuIcon = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Descr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Arguments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_icon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_edit = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBox_automaticList = new System.Windows.Forms.ComboBox();
            this.checkBox_iconOverlay = new System.Windows.Forms.CheckBox();
            this.checkBox_progressBar = new System.Windows.Forms.CheckBox();
            this.button_reload = new System.Windows.Forms.Button();
            this.linkLabel_version = new System.Windows.Forms.LinkLabel();
            this.label_autoList = new System.Windows.Forms.Label();
            this.groupBox_icon = new System.Windows.Forms.GroupBox();
            this.groupBox_jumpList = new System.Windows.Forms.GroupBox();
            this.button_down = new System.Windows.Forms.Button();
            this.button_up = new System.Windows.Forms.Button();
            this.button_deleteAuto = new System.Windows.Forms.Button();
            this.label_taskList = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.checkBox_debug = new System.Windows.Forms.CheckBox();
            this.groupBox_icon.SuspendLayout();
            this.groupBox_jumpList.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_AddNew
            // 
            this.button_AddNew.AutoSize = true;
            this.button_AddNew.Location = new System.Drawing.Point(238, 17);
            this.button_AddNew.Name = "button_AddNew";
            this.button_AddNew.Size = new System.Drawing.Size(75, 27);
            this.button_AddNew.TabIndex = 0;
            this.button_AddNew.Text = "Add new";
            this.toolTip1.SetToolTip(this.button_AddNew, "Add new link in the jump list");
            this.button_AddNew.UseVisualStyleBackColor = true;
            this.button_AddNew.Click += new System.EventHandler(this.button_AddNew_Click);
            // 
            // imageList_menuIcon
            // 
            this.imageList_menuIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_menuIcon.ImageStream")));
            this.imageList_menuIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_menuIcon.Images.SetKeyName(0, "MainIcon.ico");
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_name,
            this.columnHeader_Descr,
            this.columnHeader_path,
            this.columnHeader_Arguments,
            this.columnHeader_icon});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(19, 61);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(641, 177);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader_name
            // 
            this.columnHeader_name.Text = "Link name";
            this.columnHeader_name.Width = 94;
            // 
            // columnHeader_Descr
            // 
            this.columnHeader_Descr.Text = "Description";
            this.columnHeader_Descr.Width = 96;
            // 
            // columnHeader_path
            // 
            this.columnHeader_path.Text = "Path";
            this.columnHeader_path.Width = 188;
            // 
            // columnHeader_Arguments
            // 
            this.columnHeader_Arguments.Text = "Arguments";
            this.columnHeader_Arguments.Width = 84;
            // 
            // columnHeader_icon
            // 
            this.columnHeader_icon.Text = "Icon";
            this.columnHeader_icon.Width = 166;
            // 
            // button_edit
            // 
            this.button_edit.AutoSize = true;
            this.button_edit.Enabled = false;
            this.button_edit.Location = new System.Drawing.Point(347, 17);
            this.button_edit.Name = "button_edit";
            this.button_edit.Size = new System.Drawing.Size(75, 27);
            this.button_edit.TabIndex = 2;
            this.button_edit.Text = "Edit";
            this.toolTip1.SetToolTip(this.button_edit, "Edit selected link");
            this.button_edit.UseVisualStyleBackColor = true;
            this.button_edit.Click += new System.EventHandler(this.button_edit_Click);
            // 
            // button_delete
            // 
            this.button_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_delete.AutoSize = true;
            this.button_delete.Enabled = false;
            this.button_delete.Location = new System.Drawing.Point(585, 17);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(75, 27);
            this.button_delete.TabIndex = 3;
            this.button_delete.Text = "Delete";
            this.toolTip1.SetToolTip(this.button_delete, "Delete selected link");
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // comboBox_automaticList
            // 
            this.comboBox_automaticList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_automaticList.FormattingEnabled = true;
            this.comboBox_automaticList.Items.AddRange(new object[] {
            "Nothing",
            "Frequent",
            "Recent"});
            this.comboBox_automaticList.Location = new System.Drawing.Point(137, 260);
            this.comboBox_automaticList.Name = "comboBox_automaticList";
            this.comboBox_automaticList.Size = new System.Drawing.Size(121, 24);
            this.comboBox_automaticList.TabIndex = 4;
            this.toolTip1.SetToolTip(this.comboBox_automaticList, "Select the automatic list type ( recent, frequent)");
            this.comboBox_automaticList.SelectedIndexChanged += new System.EventHandler(this.comboBox_automaticList_SelectedIndexChanged);
            // 
            // checkBox_iconOverlay
            // 
            this.checkBox_iconOverlay.AutoSize = true;
            this.checkBox_iconOverlay.Location = new System.Drawing.Point(19, 38);
            this.checkBox_iconOverlay.Name = "checkBox_iconOverlay";
            this.checkBox_iconOverlay.Size = new System.Drawing.Size(141, 21);
            this.checkBox_iconOverlay.TabIndex = 6;
            this.checkBox_iconOverlay.Text = "Show icon overlay";
            this.toolTip1.SetToolTip(this.checkBox_iconOverlay, "Show the litle icon over the repetier host icon in the taskbar (play, stop, pause" +
        " icon)");
            this.checkBox_iconOverlay.UseVisualStyleBackColor = true;
            this.checkBox_iconOverlay.CheckedChanged += new System.EventHandler(this.checkBox_iconOverlay_CheckedChanged);
            // 
            // checkBox_progressBar
            // 
            this.checkBox_progressBar.AutoSize = true;
            this.checkBox_progressBar.Location = new System.Drawing.Point(19, 73);
            this.checkBox_progressBar.Name = "checkBox_progressBar";
            this.checkBox_progressBar.Size = new System.Drawing.Size(146, 21);
            this.checkBox_progressBar.TabIndex = 7;
            this.checkBox_progressBar.Text = "Show progress bar";
            this.toolTip1.SetToolTip(this.checkBox_progressBar, "Show a progress bar in the taskbar");
            this.checkBox_progressBar.UseVisualStyleBackColor = true;
            this.checkBox_progressBar.CheckedChanged += new System.EventHandler(this.checkBox_progressBar_CheckedChanged);
            // 
            // button_reload
            // 
            this.button_reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_reload.AutoSize = true;
            this.button_reload.Location = new System.Drawing.Point(610, 263);
            this.button_reload.Name = "button_reload";
            this.button_reload.Size = new System.Drawing.Size(75, 27);
            this.button_reload.TabIndex = 7;
            this.button_reload.Text = "Reload";
            this.toolTip1.SetToolTip(this.button_reload, "reload the jump list in the taskbar");
            this.button_reload.UseVisualStyleBackColor = true;
            this.button_reload.Click += new System.EventHandler(this.button_reload_Click);
            // 
            // linkLabel_version
            // 
            this.linkLabel_version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel_version.AutoSize = true;
            this.linkLabel_version.Location = new System.Drawing.Point(612, 18);
            this.linkLabel_version.Name = "linkLabel_version";
            this.linkLabel_version.Size = new System.Drawing.Size(48, 13);
            this.linkLabel_version.TabIndex = 11;
            this.linkLabel_version.TabStop = true;
            this.linkLabel_version.Text = "Version: ";
            this.toolTip1.SetToolTip(this.linkLabel_version, "TaskbarPlus homepage");
            this.linkLabel_version.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_version_LinkClicked);
            // 
            // label_autoList
            // 
            this.label_autoList.AutoSize = true;
            this.label_autoList.Location = new System.Drawing.Point(16, 263);
            this.label_autoList.Name = "label_autoList";
            this.label_autoList.Size = new System.Drawing.Size(95, 17);
            this.label_autoList.TabIndex = 5;
            this.label_autoList.Text = "Automatic list:";
            // 
            // groupBox_icon
            // 
            this.groupBox_icon.AutoSize = true;
            this.groupBox_icon.Controls.Add(this.checkBox_progressBar);
            this.groupBox_icon.Controls.Add(this.checkBox_iconOverlay);
            this.groupBox_icon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.087379F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_icon.Location = new System.Drawing.Point(16, 55);
            this.groupBox_icon.Name = "groupBox_icon";
            this.groupBox_icon.Size = new System.Drawing.Size(207, 115);
            this.groupBox_icon.TabIndex = 7;
            this.groupBox_icon.TabStop = false;
            this.groupBox_icon.Text = "Taskbar icon settings";
            // 
            // groupBox_jumpList
            // 
            this.groupBox_jumpList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_jumpList.Controls.Add(this.button_down);
            this.groupBox_jumpList.Controls.Add(this.button_up);
            this.groupBox_jumpList.Controls.Add(this.button_deleteAuto);
            this.groupBox_jumpList.Controls.Add(this.button_reload);
            this.groupBox_jumpList.Controls.Add(this.label_taskList);
            this.groupBox_jumpList.Controls.Add(this.button_edit);
            this.groupBox_jumpList.Controls.Add(this.button_AddNew);
            this.groupBox_jumpList.Controls.Add(this.label_autoList);
            this.groupBox_jumpList.Controls.Add(this.comboBox_automaticList);
            this.groupBox_jumpList.Controls.Add(this.listView1);
            this.groupBox_jumpList.Controls.Add(this.button_delete);
            this.groupBox_jumpList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.087379F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_jumpList.Location = new System.Drawing.Point(16, 203);
            this.groupBox_jumpList.Name = "groupBox_jumpList";
            this.groupBox_jumpList.Size = new System.Drawing.Size(691, 295);
            this.groupBox_jumpList.TabIndex = 8;
            this.groupBox_jumpList.TabStop = false;
            this.groupBox_jumpList.Text = "Jump list settings";
            // 
            // button_down
            // 
            this.button_down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_down.Enabled = false;
            this.button_down.Image = global::TaskbarPlus.Properties.Resources.giu;
            this.button_down.Location = new System.Drawing.Point(666, 161);
            this.button_down.Name = "button_down";
            this.button_down.Size = new System.Drawing.Size(19, 31);
            this.button_down.TabIndex = 13;
            this.button_down.UseVisualStyleBackColor = true;
            this.button_down.Click += new System.EventHandler(this.button_down_Click);
            // 
            // button_up
            // 
            this.button_up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_up.Enabled = false;
            this.button_up.Image = global::TaskbarPlus.Properties.Resources.su;
            this.button_up.Location = new System.Drawing.Point(666, 96);
            this.button_up.Name = "button_up";
            this.button_up.Size = new System.Drawing.Size(19, 31);
            this.button_up.TabIndex = 12;
            this.button_up.UseVisualStyleBackColor = true;
            this.button_up.Click += new System.EventHandler(this.button_up_Click);
            // 
            // button_deleteAuto
            // 
            this.button_deleteAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_deleteAuto.AutoSize = true;
            this.button_deleteAuto.Location = new System.Drawing.Point(404, 262);
            this.button_deleteAuto.Name = "button_deleteAuto";
            this.button_deleteAuto.Size = new System.Drawing.Size(142, 27);
            this.button_deleteAuto.TabIndex = 8;
            this.button_deleteAuto.Text = "Clear automatic List";
            this.button_deleteAuto.UseVisualStyleBackColor = true;
            this.button_deleteAuto.Click += new System.EventHandler(this.button_deleteAuto_Click);
            // 
            // label_taskList
            // 
            this.label_taskList.AutoSize = true;
            this.label_taskList.Location = new System.Drawing.Point(16, 24);
            this.label_taskList.Name = "label_taskList";
            this.label_taskList.Size = new System.Drawing.Size(182, 17);
            this.label_taskList.TabIndex = 6;
            this.label_taskList.Text = "Add task link in the jump list";
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.18447F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_title.Location = new System.Drawing.Point(226, 11);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(188, 20);
            this.label_title.TabIndex = 9;
            this.label_title.Text = "TaskbarPlus Settings";
            // 
            // checkBox_debug
            // 
            this.checkBox_debug.AutoSize = true;
            this.checkBox_debug.Location = new System.Drawing.Point(551, 55);
            this.checkBox_debug.Name = "checkBox_debug";
            this.checkBox_debug.Size = new System.Drawing.Size(109, 17);
            this.checkBox_debug.TabIndex = 12;
            this.checkBox_debug.Text = "Show debug Msg";
            this.toolTip1.SetToolTip(this.checkBox_debug, "Add plugin debug message in the Repetier Host Log.\r\nWARNING this setting is not s" +
        "aved in the registry, after restarting is reset to default mode (off).");
            this.checkBox_debug.UseVisualStyleBackColor = true;
            this.checkBox_debug.Visible = false;
            this.checkBox_debug.CheckedChanged += new System.EventHandler(this.checkBox_debug_CheckedChanged);
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_debug);
            this.Controls.Add(this.linkLabel_version);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.groupBox_jumpList);
            this.Controls.Add(this.groupBox_icon);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(726, 545);
            this.Load += new System.EventHandler(this.SettingsControl_Load);
            this.groupBox_icon.ResumeLayout(false);
            this.groupBox_icon.PerformLayout();
            this.groupBox_jumpList.ResumeLayout(false);
            this.groupBox_jumpList.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_AddNew;
        private System.Windows.Forms.ImageList imageList_menuIcon;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader_name;
        private System.Windows.Forms.ColumnHeader columnHeader_Descr;
        private System.Windows.Forms.ColumnHeader columnHeader_path;
        private System.Windows.Forms.ColumnHeader columnHeader_Arguments;
        private System.Windows.Forms.Button button_edit;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.ColumnHeader columnHeader_icon;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox comboBox_automaticList;
        private System.Windows.Forms.Label label_autoList;
        private System.Windows.Forms.CheckBox checkBox_iconOverlay;
        private System.Windows.Forms.GroupBox groupBox_icon;
        private System.Windows.Forms.CheckBox checkBox_progressBar;
        private System.Windows.Forms.GroupBox groupBox_jumpList;
        private System.Windows.Forms.Label label_taskList;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Button button_reload;
        private System.Windows.Forms.LinkLabel linkLabel_version;
        private System.Windows.Forms.Button button_deleteAuto;
        private System.Windows.Forms.Button button_down;
        private System.Windows.Forms.Button button_up;
        private System.Windows.Forms.CheckBox checkBox_debug;
    }
}
