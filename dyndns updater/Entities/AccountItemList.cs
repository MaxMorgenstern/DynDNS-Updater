using System;
using System.Collections.Generic;
using System.Configuration;

namespace DynDNS_Updater.Entities
{
    public class AccountItemList : ApplicationSettingsBase
    {
        public List<AccountItem> List;
        public DateTime LastUpdate;

        public AccountItemList() : base()
        {
            this.List = new List<AccountItem>();
        }
    }
}
