using DynDNS_Updater.Entities;
using DynDNS_Updater.Properties;
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
        bool pauseUpdate;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LogBox.DrawItem += LogBox_DrawItem;
            LogBox.Items.Add(new LogBoxItem(Color.Green, "Initialize application"));

            // Initialize on form load
            currentIP = "unknown";
            pauseUpdate = false;

            // Timer Object
            time = new Timer();
            time.Tick += (periodic_update);
            time.Interval = 500;
            time.Start();

            // Initial update
            periodic_update(null, null);

            if (!string.IsNullOrEmpty(DynDNSSettings.Default["Username"].ToString()))
                UserName.Text = DynDNSSettings.Default["Username"].ToString();
            else
                LogBox.Items.Add(new LogBoxItem(Color.Red, "Provide a username"));

            if (!string.IsNullOrEmpty(DynDNSSettings.Default["Token"].ToString()))
                UserToken.Text = DynDNSSettings.Default["Token"].ToString();
            else
                LogBox.Items.Add(new LogBoxItem(Color.Red, "Provide a token"));
        }

        // TICK //////////////////////////////
        private void periodic_update(object s, EventArgs e)
        {
            string tmpIPv4 = Logic.DynDNS.GetIPv4();
            string tmpIPv6 = Logic.DynDNS.GetIPv6();

            // TODO: pauseUpdate reset after time

            if (!pauseUpdate && currentIP != tmpIPv4
                && !string.IsNullOrEmpty(DynDNSSettings.Default["Username"].ToString())
                && !string.IsNullOrEmpty(DynDNSSettings.Default["Token"].ToString())
            )
            {
                string tmpCurrentIP = currentIP;
                currentIP = tmpIPv4;
                string updateResponse = Logic.DynDNS.UpdateIP(DynDNSSettings.Default["Username"].ToString(), DynDNSSettings.Default["Token"].ToString(), currentIP);

                Color updateLogColor = Color.Black;
                bool updateSuccess = false;
                Logic.DynDNS.ValidateResponse(updateResponse, out updateSuccess, out updateLogColor);

                LogBox.Items.Add(new LogBoxItem(Color.Black, "Update IP: " + currentIP));
                LogBox.Items.Add(new LogBoxItem(updateLogColor, updateResponse));

                if (!updateSuccess)
                {
                    pauseUpdate = true;
                    currentIP = tmpCurrentIP;
                    LogBox.Items.Add(new LogBoxItem(Color.Red, "Update paused"));
                }

                IPTempBox.Text = currentIP;
                LogBox.SelectedIndex = LogBox.Items.Count - 1;
            }
        }
        


        // FILE //////////////////////////////
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogBox.Items.Add(new LogBoxItem(Color.Black, "Save Username / Token"));

            DynDNSSettings.Default["Token"] = UserToken.Text;
            DynDNSSettings.Default["Username"] = UserName.Text;
            DynDNSSettings.Default.Save();
            if (pauseUpdate)
                LogBox.Items.Add(new LogBoxItem(Color.Black, "Update continued"));
            pauseUpdate = false;
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
            try
            {
            }
            catch
            {
            }
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

        private static string GetStandardBrowserPath()
        {
            string browserPath = string.Empty;
            RegistryKey browserKey = null;

            try
            {
                //Read default browser path from Win XP registry key
                browserKey = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command", false);

                //If browser path wasn't found, try Win Vista (and newer) registry key
                if (browserKey == null)
                {
                    browserKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http", false); ;
                }

                //If browser path was found, clean it
                if (browserKey != null)
                {
                    //Remove quotation marks
                    browserPath = (browserKey.GetValue(null) as string).ToLower().Replace("\"", "");

                    //Cut off optional parameters
                    if (!browserPath.EndsWith("exe"))
                    {
                        browserPath = browserPath.Substring(0, browserPath.LastIndexOf(".exe") + 4);
                    }

                    //Close registry key
                    browserKey.Close();
                }
            }
            catch
            {
                //Return empty string, if no path was found
                return string.Empty;
            }
            //Return default browsers path
            return browserPath;
        }
    }
}
