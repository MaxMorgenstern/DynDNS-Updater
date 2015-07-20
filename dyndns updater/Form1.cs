using DynDNS_Updater.Entities;
using DynDNS_Updater.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DynDNS_Updater
{
    public partial class Form1 : Form
    {
        Timer time;
        string currentIP;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LogBox.DrawItem += LogBox_DrawItem;
            LogBox.Items.Add(new LogBoxItem(Color.Green, "Initialize application"));

            // Initialize on form load
            currentIP = "unknown";

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
            string tmpIP = Logic.DynDNS.GetIPv4();
            if(currentIP != tmpIP
                && !string.IsNullOrEmpty(DynDNSSettings.Default["Username"].ToString())
                && !string.IsNullOrEmpty(DynDNSSettings.Default["Token"].ToString())
            )
            {
                currentIP = tmpIP;
                string updateResponse = Logic.DynDNS.UpdateIP(DynDNSSettings.Default["Username"].ToString(), DynDNSSettings.Default["Token"].ToString(), currentIP);

                Color updateLogColor = Color.Black;
// TODO - pause or wait for new settings
                bool updateSuccess = false;
                Logic.DynDNS.ValidateResponse(updateResponse, out updateSuccess, out updateLogColor);

                LogBox.Items.Add(new LogBoxItem(Color.Black, "Update IP: " + currentIP));
                LogBox.Items.Add(new LogBoxItem(updateLogColor, updateResponse));

                IPTempBox.Text = currentIP;
                LogBox.SelectedIndex = LogBox.Items.Count - 1;
            }
        }
        


        // FILE //////////////////////////////
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DynDNSSettings.Default["Token"] = UserToken.Text;
            DynDNSSettings.Default["Username"] = UserName.Text;
            DynDNSSettings.Default.Save();
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
            System.Diagnostics.Process.Start("http://ddns.edns.de/?help");
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
    }
}
