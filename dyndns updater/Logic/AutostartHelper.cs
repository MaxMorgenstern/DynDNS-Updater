﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: Untested

namespace DynDNS_Updater.Logic
{
    class AutostartHelper
    {
        private static string regKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private static string regAppName = "DynDNSUpdater";

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
                rkAppUser.SetValue(regAppName, Application.ExecutablePath.ToString());
        }
        
        public static void DisableAutostart()
        {
            if (IsStartupItemForCurrentUser())
                rkAppUser.DeleteValue(regAppName, false);
        }

        private static bool IsStartupItemForCurrentUser()
        {
            // if doesn't exist or not set to run at startup
            if (rkAppUser.GetValue(regAppName) == null)
                return false;
            else
                return true;
        }



        // ALL USER
        public static void EnableAutostartAll()
        {
            if (!IsStartupItemForAllUser())
                rkAppAll.SetValue(regAppName, Application.ExecutablePath.ToString());
        }

        public static void DisableAutostartAll()
        {
            if (IsStartupItemForAllUser())
                rkAppAll.DeleteValue(regAppName, false);
        }

        private static bool IsStartupItemForAllUser()
        {
            // if doesn't exist or not set to run at startup
            if (rkAppAll.GetValue(regAppName) == null)
                return false;
            else
                return true;
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