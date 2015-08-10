using DynDNS_Updater.Settings;

namespace DynDNS_Updater.Entities
{
    class DDNSProvider
    {
        public int Id;
        public string Name;
        public string UpdateURL;

        public bool AutoResolveIP = false;
        public string IPv4ResolveURL;
        public string IPv6ResolveURL;

        public bool UsePassword = false;

        public bool HostRequired = false;
        public bool AuthenticationRequired = true;

        public bool IPv6Available = true;

        public string APISuccess = "good";
        public string APIWarning = "nochg";

        public override string ToString()
        {
            return Name;
        }
    }
}
