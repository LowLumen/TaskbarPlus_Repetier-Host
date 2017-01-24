using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RepetierHostExtender.utils;
using System.Diagnostics;

namespace TaskbarPlus
{
    public partial class addEdit : Form
    {
        // link params returned in the main form
        public string[] allParams = null;

        // Costructor new file
        public addEdit()
        {
            InitializeComponent();
        }
        // Costructor edit existing link
        public addEdit(string[] oldParams)
        {
            InitializeComponent();

            textBox_name.Text = oldParams[0];
            textBox_descr.Text = oldParams[1];
            textBox_path.Text = oldParams[2];
            textBox_arg.Text = oldParams[3];
            textBox_icon.Text = oldParams[4];
            update_icon();
        }

        private void addEdit_Load(object sender, EventArgs e)
        {
            // loads the correct translation
            this.Text = Trans.T("TP_AE_FORMTITLE");
            label_name.Text = Trans.T("TP_AE_NAME");
            label_descr.Text = Trans.T("TP_AE_DESCR");
            label_path.Text = Trans.T("TP_AE_PATH");
            label_arg.Text = Trans.T("TP_AE_ARG");
            label_icon.Text = Trans.T("TP_AE_ICON");

            button_save.Text = Trans.T("TP_AE_SAVE");
            button_cancel.Text = Trans.T("TP_AE_CANCEL");
            button_path.Text = button_icon.Text = Trans.T("TP_AE_SELECT");

            toolTip1.SetToolTip(textBox_name, Trans.T("TP_AE_NAMETOOL"));
            toolTip1.SetToolTip(textBox_descr, Trans.T("TP_AE_DESCTOOL"));
            toolTip1.SetToolTip(textBox_path, Trans.T("TP_AE_PATHTOOL"));
            toolTip1.SetToolTip(textBox_arg, Trans.T("TP_AE_ARGTOOL"));
            toolTip1.SetToolTip(textBox_icon, Trans.T("TP_AE_ICONTOOL"));
        }


        #region BUTTON

        //Save data and close the form
        private void button_save_Click(object sender, EventArgs e)
        {
            // Check if name and path are present
            if (textBox_name.Text == "")
            {
                // Insert a name for the link
                MessageBox.Show(Trans.T("TP_ERR_NAME"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox_path.Text == "")
            {
                // Insert a path for the link
                MessageBox.Show(Trans.T("TP_ERR_PATH"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // add data to variable for sending it to main forum
            allParams = new string[5];
            allParams[0] = textBox_name.Text;
            allParams[1] = textBox_descr.Text;
            allParams[2] = textBox_path.Text;
            allParams[3] = textBox_arg.Text;
            allParams[4] = textBox_icon.Text;

            // Close the form
            this.Close();
        }
        // Close the form
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Open the Context menu for select "File or Folder"
        private void button_path_Click(object sender, EventArgs e)
        {
            if (!this.Browse_contextMenu.Visible)
                this.Browse_contextMenu.Show(button_path, button_path.Width, 0);
            else
                this.Browse_contextMenu.Hide();
        }
        // find file,folder,icon path
        private void open_file(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            string selectedFile = openFileDialog1.FileName;


            textBox_path.Text = selectedFile;

            // START ICON

            // Default icon and index ... shell32.dll,0
            string iconPath = Path.Combine(Environment.SystemDirectory, "shell32.dll");
            int iconIndex = 0;

            // if icon is present in the selected file use the first icon (index 0 )
            if (countIcons(selectedFile) > 0)
            {
                iconPath = selectedFile;
            }
            else // Otherwise search the associated icon in the system registry
            {
                string extension = Path.GetExtension(selectedFile);
                if (!String.IsNullOrEmpty(extension))
                {

                    string temp = GetIconFromExtension(extension);
                    if (!String.IsNullOrEmpty(temp))
                    {

                        string[] temps = temp.Split(',');
                        if (temps.Count() > 1)
                        {
                            iconPath = temps[0];
                            iconIndex = Convert.ToInt32(temps[1]);
                        }
                    }
                }
            }

            textBox_icon.Text = iconPath + "," + iconIndex.ToString();

            update_icon();

        }
        private void open_folder(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;

            string folderPath = folderBrowserDialog1.SelectedPath;

            textBox_path.Text = folderPath;

            // START ICON

            string iconPath = "";
            int iconIndex = 0;

            // Search for file desktop.ini, if present search the custom icon path
            iconPath = GetIconFromDesktopIni(folderPath);

            if (iconPath != "")
            {
                string[] temp = iconPath.Split(',');

                iconPath = temp[0];
                iconIndex = Convert.ToInt32(temp[1]);
            }
            else
            {
                //default windows folder icon
                iconPath = Path.Combine(Environment.SystemDirectory, "shell32.dll");
                iconIndex = 4;
            }

            textBox_icon.Text = iconPath + "," + iconIndex.ToString();

            update_icon();

            //END ICON
        }
        private void open_icon(object sender, EventArgs e)
        {
            if (openIconDialog.ShowDialog() != DialogResult.OK)
                return;

            textBox_icon.Text = "";

            string pathIcons = openIconDialog.FileName;


            // START icon find
            int iconIndex = 0;
            int iconCount = countIcons(pathIcons);

            if (iconCount == 0)
            {
                return;
            }
            else if (iconCount > 1)
            {
                StringBuilder sb = new StringBuilder(pathIcons, 500);
                int retVal = PickIconDlg(this.Handle, sb, sb.Capacity, ref iconIndex);

                pathIcons = sb.ToString();

                if (retVal == 0)
                {
                    // annullato dall'utente
                    return;
                }
            }

            textBox_icon.Text = pathIcons + "," + iconIndex.ToString();

            update_icon();
            
        }

        #endregion


        #region PREVIEW LINK

        // icon preview
        private void textBox_icon_KeyDown(object sender, KeyEventArgs e)
        {
            // if ENTER key
            if (e.KeyCode == Keys.Enter)
            {
                update_icon();

                e.SuppressKeyPress = true;
            }
        }
        private void textBox_icon_Leave(object sender, EventArgs e)
        {
            update_icon();
        }
        private void update_icon()
        {      
            string[] iconPath = textBox_icon.Text.Split(',');

            if (iconPath.Count() < 2) 
            {
                pictureBox_icon.Image = null;
                return;
            }

            // convert environment variable to path sting
            iconPath[0] = Environment.ExpandEnvironmentVariables(iconPath[0]);

            // Create the icon object
            Icon icon = ExtractIconFromFile(iconPath[0], Convert.ToInt32(iconPath[1]));

            if (icon == null)
            {
                //MessageBox.Show("error");
                pictureBox_icon.Image = null;
                return;
            }
            
            pictureBox_icon.Image = icon.ToBitmap();
            
        }

        // name preview
        private void textBox_name_TextChanged(object sender, EventArgs e)
        {
            linkLabel_preview.Text = textBox_name.Text;
        }
        // description preview (tooltip)
        private void textBox_descr_TextChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(linkLabel_preview, textBox_descr.Text);
        }
        // open the link preview
        private void linkLabel_preview_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // check if is a valid path
            if (!File.Exists(textBox_path.Text) && !Directory.Exists(textBox_path.Text))
                return;

            Process prs = new Process();            
            try
            {
                prs.StartInfo.FileName = textBox_path.Text;
                prs.StartInfo.Arguments = textBox_arg.Text;
                prs.Start();
            }
            finally
            {
                if (prs != null)
                    prs.Dispose();
            }
        }

        #endregion
    }


}
