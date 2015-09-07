using System;

namespace DynDNS_Updater.Entities
{
    class AccountItem
    {
        public string Username;
        public string Token;
        public string Hostname;

        public bool Paused;
        public DateTime PausedDate;

        public string IPType;

        public string LastUpdatedIP;
        public DateTime LastUpdate;

        public DDNSProvider Provider;
    }
}
