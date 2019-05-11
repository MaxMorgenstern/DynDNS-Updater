using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynDNS_Updater.Entities
{
    static class DDNSProviderList
    {
        private static List<DDNSProvider> _list;
        public static List<DDNSProvider> List 
        { 
            get 
            {
                if (_list == null)
                    PopulateList();
                return _list; 
            } 
        }

        public static void PopulateList()
        {
            _list = new List<DDNSProvider>();

            // eDNS
            DDNSProvider provider1 = new DDNSProvider();
            provider1.Id = 1;
            provider1.Name = "eDNS";
            provider1.UpdateURL = "http://ddns.edns.de/?user={0}&token={1}&ip={2}";
            provider1.IPv4ResolveURL = "http://v4.ddns.edns.de/ip.php";
            provider1.IPv6ResolveURL = "http://v6.ddns.edns.de/ip.php";

            _list.Add(provider1);
            
            //Fake DNS
            /*
            DDNSProvider provider2 = new DDNSProvider();
            provider2.Id = 2;
            provider2.Name = "FakeDNS";
            provider2.UpdateURL = "http://ddns.fakedns.de/?user={0}&token={1}&ip={2}";
            provider2.IPv4ResolveURL = "http://v4.ddns.fakedns.de/ip.php";
            provider2.IPv6ResolveURL = "http://v6.ddns.fakedns.de/ip.php";

            _list.Add(provider2);
            */
        }

        public static DDNSProvider GetProviderById(int id)
        {
            if (_list == null)
                PopulateList();
            return _list.FirstOrDefault(x => x.Id == id);
        }
    }
}
