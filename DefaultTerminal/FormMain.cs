using System;
using System.Diagnostics;
using System.Windows.Forms;
using DefaultTerminal.Utilities;

namespace DefaultTerminal
{
    public partial class FormMain : Form
    {
        #region Constructor
        public FormMain()
        {
            InitializeComponent();
            HookUtility.HookByProcessName(textProcessNames.Text.Split('|'));
        }
        #endregion

        #region Buttons Events
        private void BtnSave_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LinkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkGitHub.Text);
        }
        #endregion

        #region Tray Events
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            Activate();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            Activate();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Form Events
        private void FormMain_Load(object sender, EventArgs e)
        {
            labelVersion.Text = labelVersion.Text.Replace("%VERSION%", Application.ProductVersion);
            labelVersion.Text = labelVersion.Text.Replace("%YEAR%", DateTime.Now.Year.ToString());
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
                return;

            e.Cancel = true;
            Hide();
        }
        #endregion
    }
}
