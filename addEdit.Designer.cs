namespace TaskbarPlus
{
    partial class addEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label_name = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_descr = new System.Windows.Forms.TextBox();
            this.label_descr = new System.Windows.Forms.Label();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.label_path = new System.Windows.Forms.Label();
            this.textBox_arg = new System.Windows.Forms.TextBox();
            this.label_arg = new System.Windows.Forms.Label();
            this.textBox_icon = new System.Windows.Forms.TextBox();
            this.label_icon = new System.Windows.Forms.Label();
            this.button_path = new System.Windows.Forms.Button();
            this.button_icon = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_save = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.Browse_contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openIconDialog = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox_icon = new System.Windows.Forms.PictureBox();
            this.label_preview = new System.Windows.Forms.Label();
            this.linkLabel_preview = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Browse_contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(12, 34);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(41, 15);
            this.label_name.TabIndex = 9;
            this.label_name.Text = "Name";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(104, 34);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(223, 20);
            this.textBox_name.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBox_name, "Name for the link, show in the jump list");
            this.textBox_name.TextChanged += new System.EventHandler(this.textBox_name_TextChanged);
            // 
            // textBox_descr
            // 
            this.textBox_descr.Location = new System.Drawing.Point(104, 83);
            this.textBox_descr.Name = "textBox_descr";
            this.textBox_descr.Size = new System.Drawing.Size(223, 20);
            this.textBox_descr.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBox_descr, "The description for the link, shown when the mouse is passed over the link in the" +
        " jump list");
            this.textBox_descr.TextChanged += new System.EventHandler(this.textBox_descr_TextChanged);
            // 
            // label_descr
            // 
            this.label_descr.AutoSize = true;
            this.label_descr.Location = new System.Drawing.Point(12, 83);
            this.label_descr.Name = "label_descr";
            this.label_descr.Size = new System.Drawing.Size(69, 15);
            this.label_descr.TabIndex = 10;
            this.label_descr.Text = "Description";
            // 
            // textBox_path
            // 
            this.textBox_path.Location = new System.Drawing.Point(104, 124);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(223, 20);
            this.textBox_path.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBox_path, "The path for the link, is possible to use file, folder or web link");
            // 
            // label_path
            // 
            this.label_path.AutoSize = true;
            this.label_path.Location = new System.Drawing.Point(13, 127);
            this.label_path.Name = "label_path";
            this.label_path.Size = new System.Drawing.Size(32, 15);
            this.label_path.TabIndex = 11;
            this.label_path.Text = "Path";
            // 
            // textBox_arg
            // 
            this.textBox_arg.Location = new System.Drawing.Point(104, 168);
            this.textBox_arg.Name = "textBox_arg";
            this.textBox_arg.Size = new System.Drawing.Size(223, 20);
            this.textBox_arg.TabIndex = 4;
            this.toolTip1.SetToolTip(this.textBox_arg, "Arguments past with the link");
            // 
            // label_arg
            // 
            this.label_arg.AutoSize = true;
            this.label_arg.Location = new System.Drawing.Point(12, 171);
            this.label_arg.Name = "label_arg";
            this.label_arg.Size = new System.Drawing.Size(66, 15);
            this.label_arg.TabIndex = 12;
            this.label_arg.Text = "Arguments";
            // 
            // textBox_icon
            // 
            this.textBox_icon.Location = new System.Drawing.Point(104, 211);
            this.textBox_icon.Name = "textBox_icon";
            this.textBox_icon.Size = new System.Drawing.Size(223, 20);
            this.textBox_icon.TabIndex = 5;
            this.toolTip1.SetToolTip(this.textBox_icon, "The icon path show near the link, path and index separed by comma link,index");
            this.textBox_icon.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_icon_KeyDown);
            this.textBox_icon.Leave += new System.EventHandler(this.textBox_icon_Leave);
            // 
            // label_icon
            // 
            this.label_icon.AutoSize = true;
            this.label_icon.Location = new System.Drawing.Point(13, 214);
            this.label_icon.Name = "label_icon";
            this.label_icon.Size = new System.Drawing.Size(60, 15);
            this.label_icon.TabIndex = 13;
            this.label_icon.Text = "Icon path:";
            // 
            // button_path
            // 
            this.button_path.Location = new System.Drawing.Point(340, 124);
            this.button_path.Name = "button_path";
            this.button_path.Size = new System.Drawing.Size(75, 23);
            this.button_path.TabIndex = 3;
            this.button_path.Text = "Select...";
            this.button_path.UseVisualStyleBackColor = true;
            this.button_path.Click += new System.EventHandler(this.button_path_Click);
            // 
            // button_icon
            // 
            this.button_icon.Location = new System.Drawing.Point(340, 209);
            this.button_icon.Name = "button_icon";
            this.button_icon.Size = new System.Drawing.Size(75, 23);
            this.button_icon.TabIndex = 6;
            this.button_icon.Text = "Select...";
            this.button_icon.UseVisualStyleBackColor = true;
            this.button_icon.Click += new System.EventHandler(this.open_icon);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(104, 284);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 7;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(243, 284);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 8;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // Browse_contextMenu
            // 
            this.Browse_contextMenu.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.Browse_contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.folderToolStripMenuItem});
            this.Browse_contextMenu.Name = "Browse_contextMenu";
            this.Browse_contextMenu.Size = new System.Drawing.Size(117, 52);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(116, 24);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.open_file);
            // 
            // folderToolStripMenuItem
            // 
            this.folderToolStripMenuItem.Name = "folderToolStripMenuItem";
            this.folderToolStripMenuItem.Size = new System.Drawing.Size(116, 24);
            this.folderToolStripMenuItem.Text = "Folder";
            this.folderToolStripMenuItem.Click += new System.EventHandler(this.open_folder);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "All files (*.*)|*.*";
            this.openFileDialog1.Title = "Select a file";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select a folder";
            // 
            // openIconDialog
            // 
            this.openIconDialog.Filter = "con files (*.ico)|*.ico|Program files (*.exe;*.dll)|*.exe;*.dll|All icon files (*" +
    ".ico;*.exe;*.dll)|*.ico;*.exe;*.dll";
            // 
            // pictureBox_icon
            // 
            this.pictureBox_icon.Location = new System.Drawing.Point(6, 16);
            this.pictureBox_icon.Name = "pictureBox_icon";
            this.pictureBox_icon.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_icon.TabIndex = 14;
            this.pictureBox_icon.TabStop = false;
            // 
            // label_preview
            // 
            this.label_preview.AutoSize = true;
            this.label_preview.Location = new System.Drawing.Point(12, 252);
            this.label_preview.Name = "label_preview";
            this.label_preview.Size = new System.Drawing.Size(78, 15);
            this.label_preview.TabIndex = 15;
            this.label_preview.Text = "Link preview:";
            // 
            // linkLabel_preview
            // 
            this.linkLabel_preview.AutoSize = true;
            this.linkLabel_preview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel_preview.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel_preview.LinkColor = System.Drawing.Color.Black;
            this.linkLabel_preview.Location = new System.Drawing.Point(30, 16);
            this.linkLabel_preview.Name = "linkLabel_preview";
            this.linkLabel_preview.Size = new System.Drawing.Size(0, 17);
            this.linkLabel_preview.TabIndex = 16;
            this.linkLabel_preview.VisitedLinkColor = System.Drawing.Color.Black;
            this.linkLabel_preview.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_preview_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox_icon);
            this.groupBox1.Controls.Add(this.linkLabel_preview);
            this.groupBox1.Location = new System.Drawing.Point(104, 237);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 41);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // addEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 339);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label_preview);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.button_icon);
            this.Controls.Add(this.button_path);
            this.Controls.Add(this.textBox_icon);
            this.Controls.Add(this.label_icon);
            this.Controls.Add(this.textBox_arg);
            this.Controls.Add(this.label_arg);
            this.Controls.Add(this.textBox_path);
            this.Controls.Add(this.label_path);
            this.Controls.Add(this.textBox_descr);
            this.Controls.Add(this.label_descr);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "addEdit";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Link params";
            this.Load += new System.EventHandler(this.addEdit_Load);
            this.Browse_contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.TextBox textBox_descr;
        private System.Windows.Forms.Label label_descr;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.Label label_path;
        private System.Windows.Forms.TextBox textBox_arg;
        private System.Windows.Forms.Label label_arg;
        private System.Windows.Forms.TextBox textBox_icon;
        private System.Windows.Forms.Label label_icon;
        private System.Windows.Forms.Button button_path;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_icon;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.ContextMenuStrip Browse_contextMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openIconDialog;
        private System.Windows.Forms.PictureBox pictureBox_icon;
        private System.Windows.Forms.Label label_preview;
        private System.Windows.Forms.LinkLabel linkLabel_preview;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}