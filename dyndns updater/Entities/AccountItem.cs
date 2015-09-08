using System;
using System.Configuration;

namespace DynDNS_Updater.Entities
{
    public class AccountItem : ApplicationSettingsBase
    {
        public int Id;

        public string Username;
        public string Token;
        public string Hostname;

        public bool Paused;
        public DateTime PausedDate;

        public string IPType;

        public string LastUpdatedIP;
        public DateTime LastUpdate;

        public bool Hearbeat;
        public DateTime LastHeartbeat;

        public DDNSProvider Provider;
        public int ProviderId;

        public AccountItem() : base()
        {
        }
    }
}
