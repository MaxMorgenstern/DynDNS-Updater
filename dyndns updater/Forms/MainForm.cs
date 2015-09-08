using DynDNS_Updater.Entities;
using DynDNS_Updater.Logic;
using DynDNS_Updater.Settings;
using System;
using System.Drawing;
using System.Windows.Forms;

// TODO: Icons by - https://icons8.com
// TODO: Settings Provider by - https://github.com/crdx/PortableSettingsProvider

namespace DynDNS_Updater
{
    public partial class MainForm : Form
    {
        #region Variables

        Timer timer_periodic_update;
        Timer timer_periodic_logbox;
        
        private NotifyIcon trayIcon;

        #endregion

        public MainForm()
        {

            // Keep settings from older version
            AppSettings.UpgradeSettings();

            InitializeComponent();
            InitializeTrayIcon();
            AppSettings.Reference.MainFormReference = this;
        }

        // FORM AND INIT //////////////////////////////
        private void InitializeTrayIcon()
        {
            trayIcon = TrayHelper.InitializeTrayIcon();

            if (AppSettings.StartMinimized)
            {
                WindowState = FormWindowState.Minimized;
                // On initial load remove explicitly from taskbar
                ShowInTaskbar = false;
            }
        }
    
        #region FormWindowEvents

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                trayIcon.Text = String.Format(Language.Window.TrayText, IPTempBox.Text);
                trayIcon.Visible = true;
                trayIcon.ShowBalloonTip(3000);
                Hide();
            }
            else if (WindowState == FormWindowState.Normal)
            {
                trayIcon.Visible = false;
            }
        }

        public void MainForm_ClickTrayIcon(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            LogBox.DrawItem += LogBox_DrawItem;
            LogBox.Items.Add(new LogBoxItem(Color.Green, Language.Log.App_Init));

            // Initialize on form load
            SystemContinueUpdate();
            
            // Timer Object
            timer_periodic_update = new Timer();
            timer_periodic_update.Tick += (periodic_update);
            timer_periodic_update.Interval = AppSettings.UpdateInterval;
            timer_periodic_update.Start();

            timer_periodic_logbox = new Timer();
            timer_periodic_logbox.Tick += (periodic_log_update);
            timer_periodic_logbox.Interval = AppSettings.UpdateInterval / 4;
            timer_periodic_logbox.Start();

            // Initial update
            periodic_update(null, null);

            if (!AppSettings.HasUserameAndToken)
                SystemPauseUpdate();

            if (!string.IsNullOrEmpty(AppSettings.Username))
                UserName.Text = AppSettings.Username;

            if (!string.IsNullOrEmpty(AppSettings.Token))
                UserToken.Text = AppSettings.Token;

            UpdateStripStatusLabel.Visible = UpdateHelper.IsUpdateAvailable();
        }

        #endregion


        #region ClickEvents

        // FILE //////////////////////////////
        public void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm s = new SettingsForm();
            s.Show();
        }

        public void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Help //////////////////////////////
        private void aboutDynDNSUpdaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox a = new AboutBox();
            a.Show();
        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.OpenWebpage(@"http://ddns.edns.de/?help");
        }

        private void reportABugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.OpenWebpage(@"https://github.com/MaxMorgenstern/DynDNS-Updater/issues");
        }

        // Context Menue //////////////////////////////
        public void ContextMenueEnableAutostart_click(object sender, EventArgs e)
        {
            try
            {
                if (!AppSettings.AutostartEnabled)
                    LogBox.Items.Add(new LogBoxItem(Color.Black, Language.Log.App_Autostart_Add));

                AutostartHelper.EnableAutostart();
                trayIcon.ContextMenu = TrayHelper.UpdateTrayContextMenue();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void ContextMenueDisableAutostart_click(object sender, EventArgs e)
        {
            try
            {
                if (AppSettings.AutostartEnabled)
                    LogBox.Items.Add(new LogBoxItem(Color.Black, Language.Log.App_Autostart_Remove));

                AutostartHelper.DisableAutostart();
                trayIcon.ContextMenu = TrayHelper.UpdateTrayContextMenue();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Update_StripStatusLabel_Click(object sender, EventArgs e)
        {
            Helper.OpenWebpage(@"https://github.com/MaxMorgenstern/DynDNS-Updater/releases/latest");
        }

        private void forceUpdateButton_Click(object sender, EventArgs e)
        {
            LogBox.Items.Add(new LogBoxItem(Color.Black, Language.Log.DNS_Update_Force));

            bool tmpPauseUpdate = PauseObject.IsPaused;
            AppSettings.CurrentIP = string.Empty;
            SystemContinueUpdate();
            periodic_update(null, null);
            if (tmpPauseUpdate)
                SystemPauseUpdate();
        }

        private void pauseStartUpdateButton_Click(object sender, EventArgs e)
        {
            if (PauseObject.IsPaused)
                SystemContinueUpdate();
            else
                SystemPauseUpdate();
        }

        #endregion


        #region TimedEvents

        // TICK //////////////////////////////
        private void periodic_log_update(object s, EventArgs e)
        {
            LogBox.SelectedIndex = LogBox.Items.Count - 1;
        }

        private void periodic_update(object s, EventArgs e)
        {
            if (PauseObject.SkipTick)
                return;
            
            string tmpIP = string.Empty;
            if (AppSettings.IPType == Language.Static.TagIPv6)
            {
                tmpIP = DynDNS.GetIPv6().TrimEnd(Environment.NewLine.ToCharArray());
            }
            else
            {
                tmpIP = DynDNS.GetIPv4().TrimEnd(Environment.NewLine.ToCharArray());
            }

            if (!PauseObject.IsPaused)
            {
                if (string.IsNullOrEmpty(tmpIP))
                {
                    LogBox.Items.Add(new LogBoxItem(Color.Red, Language.Log.DNS_Resolve_Error));
                    SystemPauseUpdate();
                }

                if (!string.IsNullOrEmpty(tmpIP) && AppSettings.HasUserameAndToken )
                {
                    if (!AppSettings.CurrentIP.Equals(tmpIP))
                    {
                        string tmpCurrentIP = AppSettings.CurrentIP;
                        AppSettings.CurrentIP = tmpIP;
                        string updateResponse = DynDNS.UpdateIP(AppSettings.Username, AppSettings.Token, AppSettings.CurrentIP);

                        Color updateLogColor = Color.Black;
                        bool updateSuccess = false;
                        DynDNS.ValidateResponse(updateResponse, out updateSuccess, out updateLogColor);

                        LogBox.Items.Add(new LogBoxItem(Color.Black, String.Format(Language.Log.DNS_Update_Try, AppSettings.CurrentIP)));
                        LogBox.Items.Add(new LogBoxItem(updateLogColor, updateResponse));

                        if (!updateSuccess)
                        {
                            SystemPauseUpdate();
                            AppSettings.CurrentIP = tmpCurrentIP.TrimEnd(Environment.NewLine.ToCharArray());
                        }
                    }
                } // if <all update conditions passed>
                else
                {
                    SystemPauseUpdate();
                }
            } // if !pauseUpdate

            if (AppSettings.CurrentIP == Language.Static.Unknown)
            {
                IPTempBox.Text = tmpIP;
            }
            else
            {
                IPTempBox.Text = AppSettings.CurrentIP;
            }

            if (PauseObject.CheckContinue())
            {
                SystemContinueUpdate();
            }
            periodic_log_update(null, null);
        }

        #endregion


        #region Helper

        public void SystemPauseUpdate()
        {
            if(!PauseObject.SystemPauseUpdate())
                return;

            pauseStartUpdateButton.Text = Language.Window.Start;
            StatusStripStatusLabel.Text = Language.Window.Paused;

            periodic_log_update(null, null);
        }

        public void SystemContinueUpdate()
        {
            if (!PauseObject.SystemContinueUpdate())
                return;

            pauseStartUpdateButton.Text = Language.Window.Pause;
            StatusStripStatusLabel.Text = Language.Window.Ready;

            periodic_log_update(null, null);
        }

        // Print Log //////////////////////////////
        private void LogBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State ^ DrawItemState.Selected,
                                          e.ForeColor, Color.White);
            LogBoxItem item = LogBox.Items[e.Index] as LogBoxItem;
            if (item != null)
            {
                string currentLogEntry = item.Timestamp.ToString(Language.Log.App_Log_Format_Timestamp) + item.Message;

                e.DrawBackground();
                e.Graphics.DrawString(currentLogEntry, LogBox.Font, new SolidBrush(item.ItemColor), e.Bounds);
                Size textSize = TextRenderer.MeasureText(currentLogEntry, LogBox.Font);

                if (textSize.Width > LogBox.HorizontalExtent)
                    LogBox.HorizontalExtent = textSize.Width + 20;

                e.DrawFocusRectangle();
            }
        }

        #endregion


        #region Handler

        // Handler called by other forms //////////////////////////////
        public void MainFormSaveHandler()
        {
            if (!string.IsNullOrEmpty(AppSettings.Username))
                UserName.Text = AppSettings.Username;

            if (!string.IsNullOrEmpty(AppSettings.Token))
                UserToken.Text = AppSettings.Token;

            trayIcon.ContextMenu = TrayHelper.UpdateTrayContextMenue();
        }

        public void AddToLogBoxHandler(string text)
        {
            LogBox.Items.Add(new LogBoxItem(Color.Black, text));
        }

        public void AddToLogBoxHandler(string text, Color color)
        {
            LogBox.Items.Add(new LogBoxItem(color, text));
        }

        #endregion

    }
}
