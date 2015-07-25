using DynDNS_Updater.Entities;
using DynDNS_Updater.Properties;
using DynDNSSettings = DynDNS_Updater.Properties.Settings;
using DynDNS_Updater.Logic;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DynDNS_Updater
{
    public partial class MainForm : Form
    {
        Timer time;
        string currentIP;
        public bool pauseUpdate;
        public DateTime pauseDate;

        NotifyIcon trayIcon;

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
            trayIcon.Icon = DynDNS_Updater.Properties.Resources.World;

            trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            trayIcon.BalloonTipText = "Application running in Background";
            trayIcon.BalloonTipTitle = "DynDNS Updater";
            trayIcon.Text = "DynDNS Updater";

            trayIcon.DoubleClick += MainForm_ClickTrayIcon;
        }

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
            if (Properties.Settings.Default.UpdateSettings)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpdateSettings = false;
                Properties.Settings.Default.Save();
            }

            LogBox.DrawItem += LogBox_DrawItem;
            LogBox.Items.Add(new LogBoxItem(Color.Green, "Initialize application"));

            // Initialize on form load
            currentIP = "unknown";
            pauseUpdate = false;
            pauseDate = DateTime.MinValue;


            // Timer Object
            time = new Timer();
            time.Tick += (periodic_update);
            time.Interval = DynDNSSettings.Default.SystemUpdateInterval;
            time.Start();


            Console.WriteLine(time.Interval);

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
        }



        // TICK //////////////////////////////
        private void periodic_update(object s, EventArgs e)
        {
            if (!pauseUpdate)
            {
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

                if (string.IsNullOrEmpty(tmpIP))
                {
                    LogBox.Items.Add(new LogBoxItem(Color.Red, "Can not resolve IP address!"));
                    LogBox.Items.Add(new LogBoxItem(Color.Red, "Update paused"));
                    pauseUpdate = true;
                    pauseDate = DateTime.Now;
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
                        pauseUpdate = true;
                        pauseDate = DateTime.Now;

                        currentIP = tmpCurrentIP;
                        LogBox.Items.Add(new LogBoxItem(Color.Red, "Update paused"));
                    }

                    if (currentIP == "unknown")
                    {
                        IPTempBox.Text = tmpIP;
                    }
                    else
                    {
                        IPTempBox.Text = currentIP;
                    }

                    LogBox.SelectedIndex = LogBox.Items.Count - 1;
                }
            }

            if(pauseUpdate && pauseDate.AddHours(1) < DateTime.Now)
            {
                pauseUpdate = false;
                pauseDate = DateTime.MinValue;
            }
        }
        


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



        // Print Log //////////////////////////////
        private void LogBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State ^ DrawItemState.Selected,
                                          e.ForeColor, Color.White);
            LogBoxItem item = LogBox.Items[e.Index] as LogBoxItem;
            if (item != null)
            {
                e.DrawBackground();
                e.Graphics.DrawString(item.Message, LogBox.Font, new SolidBrush(item.ItemColor), e.Bounds);
                e.DrawFocusRectangle();
            }
        }



        // Handler called by other forms //////////////////////////////
        public void MainFormSaveHandler()
        {
            LogBox.Items.Add(new LogBoxItem(Color.Black, "Save Username / Token"));

            if (pauseUpdate)
                LogBox.Items.Add(new LogBoxItem(Color.Black, "Update continued"));
            pauseUpdate = false;
            pauseDate = DateTime.MinValue;

            if (!string.IsNullOrEmpty(DynDNSSettings.Default.Username))
                UserName.Text = DynDNSSettings.Default.Username;
            
            if (!string.IsNullOrEmpty(DynDNSSettings.Default.Token))
                UserToken.Text = DynDNSSettings.Default.Token;
        }
    }
}
