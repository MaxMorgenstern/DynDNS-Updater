﻿using System;
using System.Drawing;
using System.Windows.Forms;
using DynDNS_Updater.Entities;
using DynDNS_Updater.Logic;
using DynDNS_Updater.Properties;
#if DEBUG
    using DynDNSSettings = DynDNS_Updater.Properties.Settings;
#else
    using DynDNSSettings = DynDNS_Updater.Properties.Release; 
#endif

// TODO: Icons by - https://icons8.com

namespace DynDNS_Updater
{
    public partial class MainForm : Form
    {
        #region Variables

        Timer time;
        string currentIP;
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

            DynDNSSettings.Default.SystemAutostartEnabled = AutostartHelper.IsStartupItemForCurrentUser();
            DynDNSSettings.Default.Save();
            InitializeContextMenue();

            if (DynDNSSettings.Default.SystemStartMinimized)
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

            if (DynDNSSettings.Default.SystemAutostartEnabled)
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
            if (DynDNSSettings.Default.SystemUpdateSettings)
            {
                DynDNSSettings.Default.Upgrade();
                DynDNSSettings.Default.SystemUpdateSettings = false;
                DynDNSSettings.Default.Save();
            }

            LogBox.DrawItem += LogBox_DrawItem;
            LogBox.Items.Add(new LogBoxItem(Color.Green, "Initialize application"));

            // Initialize on form load
            currentIP = "unknown";
            SystemContinueUpdate();


            // Timer Object
            time = new Timer();
            time.Tick += (periodic_update);
            time.Interval = DynDNSSettings.Default.SystemUpdateInterval;
            time.Start();

            // Initial update
            periodic_update(null, null);

            if (!string.IsNullOrEmpty(DynDNSSettings.Default.Username))
                UserName.Text = DynDNSSettings.Default.Username;
            else
                LogBox.Items.Add(new LogBoxItem(Color.Red, "Provide a username"));

            if (!string.IsNullOrEmpty(DynDNSSettings.Default.Token))
                UserToken.Text = DynDNSSettings.Default.Token;
            else
                LogBox.Items.Add(new LogBoxItem(Color.Red, "Provide a token"));

            UpdateStripStatusLabel.Visible = UpdateHelper.IsUpdateAvailable();
        }

        #endregion


        #region ClickEvents

        // FILE //////////////////////////////
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm s = new SettingsForm(this);
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
                AutostartHelper.EnableAutostart();
                DynDNSSettings.Default.SystemAutostartEnabled = true;
                DynDNSSettings.Default.Save();
                InitializeContextMenue();

                LogBox.Items.Add(new LogBoxItem(Color.Black, "DynDNS Updater added to Autostart"));
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
                AutostartHelper.DisableAutostart();
                DynDNSSettings.Default.SystemAutostartEnabled = false;
                DynDNSSettings.Default.Save();
                InitializeContextMenue();

                LogBox.Items.Add(new LogBoxItem(Color.Black, "DynDNS Updater removed from Autostart"));
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

        #endregion


        #region TimedEvents

        // TICK //////////////////////////////
        private void periodic_update(object s, EventArgs e)
        {
            if (pauseUpdate)
            {
                pauseDelay++;
                if (pauseDelay % 2 == 0)
                    return;
            }

            string tmpIP = string.Empty;
            if (!string.IsNullOrEmpty(DynDNSSettings.Default.IPType)
                && DynDNSSettings.Default.IPType == "IPv6")
            {
                tmpIP = DynDNS.GetIPv6();
            }
            else
            {
                tmpIP = DynDNS.GetIPv4();
            }

            if (!pauseUpdate)
            {
                if (string.IsNullOrEmpty(tmpIP))
                {
                    LogBox.Items.Add(new LogBoxItem(Color.Red, "Can not resolve IP address!"));
                    LogBox.Items.Add(new LogBoxItem(Color.Red, "Update paused"));
                    SystemPauseUpdate();
                }

                if (!string.IsNullOrEmpty(tmpIP)
                    && currentIP != tmpIP
                    && !string.IsNullOrEmpty(DynDNSSettings.Default.Username)
                    && !string.IsNullOrEmpty(DynDNSSettings.Default.Token)
                )
                {
                    string tmpCurrentIP = currentIP;
                    currentIP = tmpIP;
                    string updateResponse = DynDNS.UpdateIP(DynDNSSettings.Default.Username, DynDNSSettings.Default.Token, currentIP);

                    Color updateLogColor = Color.Black;
                    bool updateSuccess = false;
                    DynDNS.ValidateResponse(updateResponse, out updateSuccess, out updateLogColor);

                    LogBox.Items.Add(new LogBoxItem(Color.Black, "Try IP update: " + currentIP));
                    LogBox.Items.Add(new LogBoxItem(updateLogColor, updateResponse));

                    if (!updateSuccess)
                    {
                        SystemPauseUpdate();

                        currentIP = tmpCurrentIP;
                        LogBox.Items.Add(new LogBoxItem(Color.Red, "Update paused"));
                    }

                    LogBox.SelectedIndex = LogBox.Items.Count - 1;
                } // if <all update conditions passed>
            } // if !pauseUpdate

            if (currentIP == "unknown")
            {
                IPTempBox.Text = tmpIP;
            }
            else
            {
                IPTempBox.Text = currentIP;
            }

            if (pauseUpdate && pauseDate.AddHours(1) < DateTime.Now)
            {
                SystemContinueUpdate();
            }
        }

        #endregion


        #region Helper

        private void SystemPauseUpdate()
        {
            pauseUpdate = true;
            pauseDate = DateTime.Now;
            StatusStripStatusLabel.Text = "Paused";
            pauseDelay = 1;
        }

        private void SystemContinueUpdate()
        {
            pauseUpdate = false;
            pauseDate = DateTime.MinValue;
            StatusStripStatusLabel.Text = "Ready";
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
            LogBox.Items.Add(new LogBoxItem(Color.Black, "Save Settings"));

            if (pauseUpdate)
                LogBox.Items.Add(new LogBoxItem(Color.Black, "Update continued"));
            SystemContinueUpdate();

            if (!string.IsNullOrEmpty(DynDNSSettings.Default.Username))
                UserName.Text = DynDNSSettings.Default.Username;
            
            if (!string.IsNullOrEmpty(DynDNSSettings.Default.Token))
                UserToken.Text = DynDNSSettings.Default.Token;
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
