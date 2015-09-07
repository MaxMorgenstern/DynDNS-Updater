using DynDNS_Updater.Settings;
using Microsoft.Win32;
using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace DynDNS_Updater.Logic
{
    class AutostartHelper
    {
        private static string regKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private static string regAppName = Language.Window.App_Name;
        private static string regAppPath = Application.ExecutablePath.ToString();

        // The path to the key where Windows looks for startup applications
        private static RegistryKey rkAppUser
        {
            get
            {
                return Registry.CurrentUser.OpenSubKey(regKey, true);
            }
        }

        private static RegistryKey rkAppAll
        {
            get
            {
                return Registry.LocalMachine.OpenSubKey(regKey, true);
            }
        }



        // CURRENT USER
        public static void EnableAutostart()
        {
            if (!IsStartupItemForCurrentUser())
                rkAppUser.SetValue(regAppName, regAppPath);
            AppSettings.AutostartEnabled = true;
            AppSettings.SaveSettings();
        }
        
        public static void DisableAutostart()
        {
            if (IsStartupItemForCurrentUser())
                rkAppUser.DeleteValue(regAppName, false);
            AppSettings.AutostartEnabled = false;
            AppSettings.SaveSettings();
        }

        public static bool IsStartupItemForCurrentUser()
        {
            object regKey = rkAppUser.GetValue(regAppName);
            if (regKey != null)
            {
                if (regKey.ToString() == regAppPath)
                    return true;
            }
            // remove if invalid path
            rkAppUser.DeleteValue(regAppName, false);
            return false;
        }



        // ALL USER
        public static void EnableAutostartAll()
        {
            if (!IsStartupItemForAllUser())
                rkAppAll.SetValue(regAppName, regAppPath);
        }

        public static void DisableAutostartAll()
        {
            if (IsStartupItemForAllUser())
                rkAppAll.DeleteValue(regAppName, false);
        }

        public static bool IsStartupItemForAllUser()
        {
            object regKey = rkAppAll.GetValue(regAppName);
            if (regKey != null)
            {
                if (regKey.ToString() == regAppPath)
                    return true;
            }
            // remove if invalid path
            rkAppAll.DeleteValue(regAppName, false);
            return false;
        }



        // Check for Admin Rights
        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex);
                isAdmin = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                isAdmin = false;
            }
            return isAdmin;
        }
    }
}
