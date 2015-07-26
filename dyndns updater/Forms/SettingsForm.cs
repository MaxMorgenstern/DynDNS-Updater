using System;
using System.Windows.Forms;
using DynDNS_Updater.Properties;
#if DEBUG
    using DynDNSSettings = DynDNS_Updater.Properties.Settings;
#else
    using DynDNSSettings = DynDNS_Updater.Properties.Release; 
#endif

namespace DynDNS_Updater
{
    public partial class SettingsForm : Form
    {
        private MainForm mainForm = null;

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
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DynDNSSettings.Default.Token = UserToken.Text;
            DynDNSSettings.Default.Username = UserName.Text;

            string ipSetting = "IPv4";
            if (v6RadioButton.Checked)
                ipSetting = "IPv6";
            DynDNSSettings.Default.IPType = ipSetting;
            
            DynDNSSettings.Default.Save();

            mainForm.MainFormSaveHandler();

            this.Close();
        }

    }
}
