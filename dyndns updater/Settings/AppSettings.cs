#if DEBUG
    using DynDNSSettings = DynDNS_Updater.Properties.Settings;
#else
    using DynDNSSettings = DynDNS_Updater.Properties.Release; 
#endif


namespace DynDNS_Updater.Settings
{
    static class AppSettings
    {

        public static class Reference
        {
            public static MainForm MainFormReference { get; set; }
            public static SettingsForm SettingsFormReference { get; set; }
        }

        // System //////////////////////////////

        public static bool WriteLogFile
        {
            get { return DynDNSSettings.Default.SystemWriteLogFile; }
            set { DynDNSSettings.Default.SystemWriteLogFile = value; }
        }

        public static bool AutostartEnabled
        {
            get { return DynDNSSettings.Default.SystemAutostartEnabled; }
            set { DynDNSSettings.Default.SystemAutostartEnabled = value; }
        }

        public static bool StartMinimized
        {
            get { return DynDNSSettings.Default.SystemStartMinimized; }
            set { DynDNSSettings.Default.SystemStartMinimized = value; }
        }

        public static bool ProviderLock
        {
            get 
            {
                if (DynDNSSettings.Default.SystemLockProvider > 0)
                    return true;
                return false;
            }
        }


        // User //////////////////////////////

        public static int ProviderId
        {
            get {
                if (DynDNSSettings.Default.SystemLockProvider > 0)
                    return DynDNSSettings.Default.SystemLockProvider;
                if (DynDNSSettings.Default.Provider == 0)
                    return 1;
                return DynDNSSettings.Default.Provider;
            }
            set
            {
                if (DynDNSSettings.Default.SystemLockProvider == 0)
                    DynDNSSettings.Default.Provider = value;
            }
        }

        public static string Username
        {
            get { return DynDNSSettings.Default.Username; }
            set { DynDNSSettings.Default.Username = value; }
        }

        public static string Token
        {
            get { return DynDNSSettings.Default.Token; }
            set { DynDNSSettings.Default.Token = value; }
        }

        public static bool HasUserameAndToken
        {
            get 
            {
                if (string.IsNullOrEmpty(DynDNSSettings.Default.Token)
                    || string.IsNullOrEmpty(DynDNSSettings.Default.Username))
                    return false;
                return true;
            }
        }


        // DYN Settings //////////////////////////////

        public static string IPType
        {
            get 
            {
                if (string.IsNullOrEmpty(DynDNSSettings.Default.IPType))
                {
                    return "IPv4";
                }                
                return DynDNSSettings.Default.IPType; 
            }
            set 
            {
                if (value == "IPv4" || value == "IPv6")
                    DynDNSSettings.Default.IPType = value;
            }
        }

        private static string _currentIP;
        public static string CurrentIP
        {
            get 
            {
                if(string.IsNullOrEmpty(_currentIP))
                    return "unknown";
                return _currentIP;
            }
            set 
            {
                _currentIP = value;
            }
        }

        public static int UpdateInterval
        {
            get { return DynDNSSettings.Default.SystemUpdateInterval; }
        }


        // Functions //////////////////////////////

        public static void SaveSettings()
        {
            DynDNSSettings.Default.Save();
        }

        public static void UpgradeSettings()
        {
            if (DynDNSSettings.Default.SystemUpdateSettings)
            {
                DynDNSSettings.Default.Upgrade();
                DynDNSSettings.Default.SystemUpdateSettings = false;
                DynDNSSettings.Default.Save();
            }
        }


        // Variables //////////////////////////////

        public static string LogFileName
        {
            get { return "log.txt"; }
        }
    }
}
