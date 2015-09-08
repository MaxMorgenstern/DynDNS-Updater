using DynDNS_Updater.Settings;
using System.Configuration;

namespace DynDNS_Updater.Entities
{
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public class DDNSProvider
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

        public string APISuccess = Language.Static.ResponseGood;
        public string APIWarning = Language.Static.ResponseNoChangeGood;

        public override string ToString()
        {
            return Name;
        }
    }
}
