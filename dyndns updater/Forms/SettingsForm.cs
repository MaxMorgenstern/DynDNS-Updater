﻿using DynDNS_Updater.Logic;
using DynDNS_Updater.Settings;
using System;
using System.Windows.Forms;

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
            ProviderComboBox.DataSource = Entities.DDNSProviderList.List;
            ProviderComboBox.SelectedIndex = AppSettings.ProviderId - 1;
            if(AppSettings.ProviderLock)
                ProviderComboBox.Enabled = false;

            LanguageComboBox.DataSource = Helper.GetAvailableCultures();
            LanguageComboBox.SelectedIndex = LanguageComboBox.FindString(AppSettings.AppLanguage);

            if (!string.IsNullOrEmpty(AppSettings.Username))
                UserName.Text = AppSettings.Username;

            if (!string.IsNullOrEmpty(AppSettings.Token))
                UserToken.Text = AppSettings.Token;

            v4RadioButton.Checked = true;
            if (AppSettings.IPType == Language.Static.IPv6)
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

            if (!AppSettings.ProviderLock)
                AppSettings.ProviderId = ((Entities.DDNSProvider)ProviderComboBox.SelectedItem).Id;

            string newCulture = string.Empty;
            string currentCulture = AppSettings.AppLanguage;
            if (!((System.Globalization.CultureInfo)LanguageComboBox.SelectedItem).IsNeutralCulture)
                newCulture = LanguageComboBox.SelectedValue.ToString();
            
            AppSettings.AppLanguage = newCulture;           

            string ipSetting = Language.Static.IPv4;
            if (v6RadioButton.Checked)
                ipSetting = Language.Static.IPv6;
            AppSettings.IPType = ipSetting;

            bool autostartApp = AppSettings.AutostartEnabled;
            AppSettings.AutostartEnabled = tmpAutostartEnabled;
            if (autostartApp != tmpAutostartEnabled)
            {
                if (tmpAutostartEnabled)
                {
                    AutostartHelper.EnableAutostart();
                    AppSettings.Reference.MainFormReference.AddToLogBoxHandler(Language.Log.App_Autostart_Add);
                }
                else
                {
                    AutostartHelper.DisableAutostart();
                    AppSettings.Reference.MainFormReference.AddToLogBoxHandler(Language.Log.App_Autostart_Remove);
                }
            }

            AppSettings.StartMinimized = MinimizedCheckBox.Checked;
            AppSettings.WriteLogFile = LogfileCheckBox.Checked;

            AppSettings.SaveSettings();


            if (currentCulture != newCulture)
            {
                AppSettings.Reference.MainFormReference.AddToLogBoxHandler(Language.Log.App_Restart_Needed);

                var confirmResult = MessageBox.Show(Language.Window.ConfirmRestart,
                         Language.Window.ConfirmationDialog,
                         MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture =
                        System.Globalization.CultureInfo.CreateSpecificCulture(Settings.AppSettings.AppLanguage);
                    System.Diagnostics.Process.Start(Application.ExecutablePath);
                    Settings.AppSettings.Reference.MainFormReference.Close();
                }
            }
            
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

            AppSettings.Reference.MainFormReference.AddToLogBoxHandler(Language.Log.Setting_Saved);
            AppSettings.Reference.MainFormReference.SystemContinueUpdate();
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
