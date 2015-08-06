﻿using System;
using System.Windows.Forms;
using DynDNS_Updater.Properties;
using DynDNS_Updater.Logic;
using DynDNS_Updater.Settings;

namespace DynDNS_Updater
{
    public partial class SettingsForm : Form
    {
        #region Variables

        private bool tmpAutostartEnabled = false;
        
        #endregion


        public SettingsForm()
        {
            InitializeComponent();
            AppSettings.Reference.SettingsFormReference = this;
        }
        
        private void Settings_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AppSettings.Username))
                UserName.Text = AppSettings.Username;

            if (!string.IsNullOrEmpty(AppSettings.Token))
                UserToken.Text = AppSettings.Token;

            v4RadioButton.Checked = true;
            if(AppSettings.IPType == "IPv6")
                v6RadioButton.Checked = true;

            tmpAutostartEnabled = AppSettings.AutostartEnabled;
            MinimizedCheckBox.Checked = AppSettings.StartMinimized;
            LogfileCheckBox.Checked = AppSettings.WriteLogFile;

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
            AppSettings.Token = UserToken.Text;
            AppSettings.Username = UserName.Text;

            string ipSetting = "IPv4";
            if (v6RadioButton.Checked)
                ipSetting = "IPv6";
            AppSettings.IPType = ipSetting;

            AppSettings.AutostartEnabled = tmpAutostartEnabled;
            if (tmpAutostartEnabled)
            {
                AutostartHelper.EnableAutostart();
                AppSettings.Reference.MainFormReference.AddToLogBoxHandler("DynDNS Updater added to Autostart");
            }
            else
            {
                AutostartHelper.DisableAutostart();
                AppSettings.Reference.MainFormReference.AddToLogBoxHandler("DynDNS Updater removed from Autostart");
            }

            AppSettings.StartMinimized = MinimizedCheckBox.Checked;
            AppSettings.WriteLogFile = LogfileCheckBox.Checked;

            AppSettings.SaveSettings();
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

            AppSettings.Reference.MainFormReference.AddToLogBoxHandler("Save Settings");
            AppSettings.Reference.MainFormReference.SystemContinueUpdate();
            AppSettings.Reference.MainFormReference.InitializeContextMenue();
            AppSettings.Reference.MainFormReference.MainFormSaveHandler();

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
