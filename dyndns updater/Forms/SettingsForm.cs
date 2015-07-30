using System;
using System.Windows.Forms;
using DynDNS_Updater.Properties;
#if DEBUG
    using DynDNSSettings = DynDNS_Updater.Properties.Settings;
using DynDNS_Updater.Logic;
#else
    using DynDNSSettings = DynDNS_Updater.Properties.Release; 
#endif

namespace DynDNS_Updater
{
    public partial class SettingsForm : Form
    {
        #region Variables

        private MainForm mainForm = null;
        private bool tmpAutostartEnabled = false;
        
        #endregion


        public SettingsForm(Form callingForm)
        {
            mainForm = callingForm as MainForm;
            InitializeComponent();
        }
        
        private void Settings_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DynDNSSettings.Default.Username))
                UserName.Text = DynDNSSettings.Default.Username;

            if (!string.IsNullOrEmpty(DynDNSSettings.Default.Token))
                UserToken.Text = DynDNSSettings.Default.Token;

            v4RadioButton.Checked = true;
            if (!string.IsNullOrEmpty(DynDNSSettings.Default.IPType))
            {
                if (DynDNSSettings.Default.IPType == "IPv6")
                    v6RadioButton.Checked = true;
            }
            tmpAutostartEnabled = DynDNSSettings.Default.SystemAutostartEnabled;
            InitializeAutostartButtons();
        }


        #region PrivateHelper

        private void InitializeAutostartButtons()
        {
            if (tmpAutostartEnabled)
            {
                enableButton.Enabled = false;
                disableButton.Enabled = true;
            }
            else
            {
                enableButton.Enabled = true;
                disableButton.Enabled = false;
            }
        }

        private void SaveSettings()
        {
            DynDNSSettings.Default.Token = UserToken.Text;
            DynDNSSettings.Default.Username = UserName.Text;

            string ipSetting = "IPv4";
            if (v6RadioButton.Checked)
                ipSetting = "IPv6";
            DynDNSSettings.Default.IPType = ipSetting;

            DynDNSSettings.Default.SystemAutostartEnabled = tmpAutostartEnabled;
            if (tmpAutostartEnabled)
            {
                AutostartHelper.EnableAutostart();
                mainForm.AddToLogBoxHandler("DynDNS Updater added to Autostart");
            }
            else
            {
                AutostartHelper.DisableAutostart();
                mainForm.AddToLogBoxHandler("DynDNS Updater removed from Autostart");
            }

            DynDNSSettings.Default.Save();
        }

        #endregion


        #region ClickEvents

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveSettings();

            mainForm.InitializeContextMenue();
            mainForm.MainFormSaveHandler();

            this.Close();
        }

        private void enableButton_Click(object sender, EventArgs e)
        {
            tmpAutostartEnabled = true;
            InitializeAutostartButtons();
        }

        private void disableButton_Click(object sender, EventArgs e)
        {
            tmpAutostartEnabled = false;
            InitializeAutostartButtons();
        }

        #endregion
    }
}
