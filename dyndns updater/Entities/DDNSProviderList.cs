using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynDNS_Updater.Entities
{
    static class DDNSProviderList
    {
        public static List<DDNSProvider> List;

        public static void PopulateList()
        {
            List = new List<DDNSProvider>();
            
            // eDNS
            DDNSProvider provider1 = new DDNSProvider();
            provider1.Id = 1;
            provider1.Name = "eDNS";
            provider1.UpdateURL = "http://ddns.edns.de/?user={0}&token={1}&ip={2}";
            provider1.IPv4ResolveURL = "http://v4.ddns.edns.de/ip.php";
            provider1.IPv6ResolveURL = "http://v4.ddns.edns.de/ip.php";

            List.Add(provider1);
        }

        public static DDNSProvider GetProviderById(int id)
        {
            if (List == null)
                PopulateList();
            return List.FirstOrDefault(x => x.Id == id);
        }
    }
}
