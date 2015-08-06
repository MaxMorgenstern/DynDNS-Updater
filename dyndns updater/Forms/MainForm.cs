using System;
using System.Drawing;
using System.Windows.Forms;
using DynDNS_Updater.Entities;
using DynDNS_Updater.Logic;
using DynDNS_Updater.Properties;
using DynDNS_Updater.Settings;

// TODO: Icons by - https://icons8.com

namespace DynDNS_Updater
{
    public partial class MainForm : Form
    {
        #region Variables

        Timer timer_periodic_update;
        Timer timer_periodic_logbox;

        public bool pauseUpdate;
        public DateTime pauseDate;
        public int pauseDelay;

        private NotifyIcon trayIcon;
        private ContextMenu trayMenue; 

        #endregion


        public MainForm()
        {
            InitializeComponent();
            InitializeTrayIcon();
            AppSettings.Reference.MainFormReference = this;
        }

        // FORM AND INIT //////////////////////////////
        private void InitializeTrayIcon()
        {
            trayIcon = new NotifyIcon();
            trayIcon.Visible = false;
            trayIcon.Icon = Resources.World;

            trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            trayIcon.BalloonTipText = "Application running in Background";
            trayIcon.BalloonTipTitle = "DynDNS Updater";
            trayIcon.Text = "DynDNS Updater";

            trayIcon.DoubleClick += MainForm_ClickTrayIcon;

            AppSettings.AutostartEnabled = AutostartHelper.IsStartupItemForCurrentUser();
            AppSettings.SaveSettings();
            InitializeContextMenue();

            if (AppSettings.StartMinimized)
            {
                WindowState = FormWindowState.Minimized;
                // On initial load remove explicitly from taskbar
                ShowInTaskbar = false;
            }
        }

        public void InitializeContextMenue()
        {
            trayMenue = new ContextMenu();
            trayMenue.MenuItems.Add(0, new MenuItem("Show", new System.EventHandler(MainForm_ClickTrayIcon)));
            trayMenue.MenuItems.Add(1, new MenuItem("Settings", new System.EventHandler(settingsToolStripMenuItem_Click)));

            if (AppSettings.AutostartEnabled)
            {
                trayMenue.MenuItems.Add(2, new MenuItem("Disable Autostart", new System.EventHandler(contextMenueDisableAutostart_click)));
            }
            else
            {
                trayMenue.MenuItems.Add(2, new MenuItem("Enable Autostart", new System.EventHandler(contextMenueEnableAutostart_click)));
            }

            trayMenue.MenuItems.Add(3, new MenuItem("Exit", new System.EventHandler(exitToolStripMenuItem_Click)));

            trayIcon.ContextMenu = trayMenue;
        }


        #region FormWindowEvents

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                trayIcon.Text = "DynDNS Updater. Current IP: " + IPTempBox.Text;
                trayIcon.Visible = true;
                trayIcon.ShowBalloonTip(3000);
                Hide();
            }
            else if (WindowState == FormWindowState.Normal)
            {
                trayIcon.Visible = false;
            }
        }

        private void MainForm_ClickTrayIcon(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Keep settings from older version
            AppSettings.UpgradeSettings();

            LogBox.DrawItem += LogBox_DrawItem;
            LogBox.Items.Add(new LogBoxItem(Color.Green, "Initialize application"));

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
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm s = new SettingsForm();
            s.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
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




        // Context Menue //////////////////////////////
        private void contextMenueEnableAutostart_click(object sender, EventArgs e)
        {
            try
            {
                if (!AppSettings.AutostartEnabled)
                    LogBox.Items.Add(new LogBoxItem(Color.Black, "DynDNS Updater added to Autostart"));

                AutostartHelper.EnableAutostart();
                InitializeContextMenue();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void contextMenueDisableAutostart_click(object sender, EventArgs e)
        {
            try
            {
                if (AppSettings.AutostartEnabled)
                    LogBox.Items.Add(new LogBoxItem(Color.Black, "DynDNS Updater removed from Autostart"));

                AutostartHelper.DisableAutostart();
                InitializeContextMenue();
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
            LogBox.Items.Add(new LogBoxItem(Color.Black, "Force update"));

            bool tmpPauseUpdate = pauseUpdate;
            AppSettings.CurrentIP = string.Empty;
            SystemContinueUpdate();
            periodic_update(null, null);
            if (tmpPauseUpdate)
                SystemPauseUpdate();
        }

        private void pauseStartUpdateButton_Click(object sender, EventArgs e)
        {
            if (pauseUpdate)
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
            if (pauseUpdate)
            {
                pauseDelay++;
                if (pauseDelay % 2 == 0)
                    return;
            }

            string tmpIP = string.Empty;
            if (AppSettings.IPType == "IPv6")
            {
                tmpIP = DynDNS.GetIPv6().TrimEnd(Environment.NewLine.ToCharArray());
            }
            else
            {
                tmpIP = DynDNS.GetIPv4().TrimEnd(Environment.NewLine.ToCharArray());
            }

            if (!pauseUpdate)
            {
                if (string.IsNullOrEmpty(tmpIP))
                {
                    LogBox.Items.Add(new LogBoxItem(Color.Red, "Can not resolve IP address!"));
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

                        LogBox.Items.Add(new LogBoxItem(Color.Black, "Try IP update: " + AppSettings.CurrentIP));
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

            if (AppSettings.CurrentIP == "unknown")
            {
                IPTempBox.Text = tmpIP;
            }
            else
            {
                IPTempBox.Text = AppSettings.CurrentIP;
            }

            if (pauseUpdate && pauseDate.AddHours(1) < DateTime.Now)
            {
                SystemContinueUpdate();
            }
            periodic_log_update(null, null);
        }

        #endregion


        #region Helper

        public void SystemPauseUpdate()
        {
            if (pauseUpdate)
                return;

            if (!AppSettings.HasUserameAndToken)
                LogBox.Items.Add(new LogBoxItem(Color.Red, "Provide username and password"));

            pauseUpdate = true;
            pauseDate = DateTime.Now;
            pauseStartUpdateButton.Text = "Start";
            StatusStripStatusLabel.Text = "Paused";
            pauseDelay = 1;
            LogBox.Items.Add(new LogBoxItem(Color.Black, "Update paused"));
            periodic_log_update(null, null);
        }

        public void SystemContinueUpdate()
        {
            if (!pauseUpdate)
                return;

            pauseUpdate = false;
            pauseDate = DateTime.MinValue;
            pauseStartUpdateButton.Text = "Pause";
            StatusStripStatusLabel.Text = "Ready";

            LogBox.Items.Add(new LogBoxItem(Color.Black, "Update continued"));
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
                string currentLogEntry = item.Timestamp.ToString("dd.MM.yyyy - hh:mm:ss - ") + item.Message;

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
