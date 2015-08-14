using DynDNS_Updater.Properties;
using DynDNS_Updater.Settings;
using System;
using System.Windows.Forms;

namespace DynDNS_Updater.Logic
{
    static class TrayHelper
    {
        private static NotifyIcon trayIcon;
        private static ContextMenu trayMenue;

        public static NotifyIcon InitializeTrayIcon()
        {
            trayIcon = new NotifyIcon();
            trayIcon.Visible = false;
            trayIcon.Icon = Resources.World;

            trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            trayIcon.BalloonTipText = Language.Window.App_Running_Background;
            trayIcon.BalloonTipTitle = Language.Window.App_Name;
            trayIcon.Text = Language.Window.App_Name;

            trayIcon.DoubleClick += NotifyIcon_DoubleClick;

            AppSettings.AutostartEnabled = AutostartHelper.IsStartupItemForCurrentUser();
            AppSettings.SaveSettings();
            trayIcon.ContextMenu = UpdateTrayContextMenue();

            return trayIcon;
        }
        
        public static ContextMenu UpdateTrayContextMenue()
        {
            MainForm MainReference = AppSettings.Reference.MainFormReference;
            trayMenue = new ContextMenu();
            trayMenue.MenuItems.Add(0, new MenuItem(Language.Window.ShowForm, new System.EventHandler(NotifyIcon_DoubleClick)));
            trayMenue.MenuItems.Add(1, new MenuItem(Language.Window.Settings, new System.EventHandler(NotifyIcon_SettingsClick)));

            if (AppSettings.AutostartEnabled)
            {
                trayMenue.MenuItems.Add(2, new MenuItem(Language.Window.Autostart_Disable, new System.EventHandler(NotifyIcon_DisableAutostartClick)));
            }
            else
            {
                trayMenue.MenuItems.Add(2, new MenuItem(Language.Window.Autostart_Enable, new System.EventHandler(NotifyIcon_EnableAutostartClick)));
            }

            trayMenue.MenuItems.Add(3, new MenuItem(Language.Window.Exit, new System.EventHandler(NotifyIcon_ExitClick)));

            return trayMenue;
        }



        private static void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            AppSettings.Reference.MainFormReference.MainForm_ClickTrayIcon(null, null);
        }

        private static void NotifyIcon_SettingsClick(object sender, EventArgs e)
        {
            AppSettings.Reference.MainFormReference.SettingsToolStripMenuItem_Click(null, null);
        }

        private static void NotifyIcon_DisableAutostartClick(object sender, EventArgs e)
        {
            AppSettings.Reference.MainFormReference.ContextMenueDisableAutostart_click(null, null);
        }

        private static void NotifyIcon_EnableAutostartClick(object sender, EventArgs e)
        {
            AppSettings.Reference.MainFormReference.ContextMenueEnableAutostart_click(null, null);
        }

        private static void NotifyIcon_ExitClick(object sender, EventArgs e)
        {
            AppSettings.Reference.MainFormReference.ExitToolStripMenuItem_Click(null, null);
        }
    }
}
