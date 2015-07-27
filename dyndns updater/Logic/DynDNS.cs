using System;
using System.Drawing;

namespace DynDNS_Updater.Logic
{
    class DynDNS
    {
        private static string GetIPv4URL
        {
            get
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                if (rnd.Next(0, 100) > 60)
                    return "http://v4.ddns.edns.de/ip.php";
                return "http://ipv4.icanhazip.com/";
            }
        }

        private static string GetIPv6URL
        {
            get
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                if (rnd.Next(0, 100) > 60)
                    return "http://v6.ddns.edns.de/ip.php";
                return "http://ipv6.icanhazip.com/";
            }
        }

        private static string UpdateURL
        {
            get
            {
                return "http://ddns.edns.de/?user={0}&token={1}&ip={2}";
            }
        }


        public static string GetIPv4()
        {
            return Helper.DoWebRequest(GetIPv4URL);
        }

        public static string GetIPv6()
        {
            return Helper.DoWebRequest(GetIPv6URL);
        }

        public static string UpdateIP(string user, string token, string ip)
        {
            return Helper.DoWebRequest(string.Format (UpdateURL, user, token, ip));
        }

        public static string ValidateResponse(string response, out bool success, out Color color)
        {
            success = false;
            color = Color.Black;
            if (response.Contains("badauth") || response.Contains("nohost")
                 || response.Contains("abuse") || response.Contains("!yours")
                 || response.Contains("dnserr"))
            {
                color = Color.Red;
            }

            if (response.Contains("nochg"))
            {
                success = true;
                color = Color.OrangeRed;
            }

            if (response.Contains("good"))
            {
                success = true;
                color = Color.Green;
            }

            return response;
        }

        // TODO
        private static string replace(string message)
        {
            /*
            message = message.Replace("<Username>", DynDNSSettings.Default["Username"].ToString().Trim());
            message = message.Replace("<Token>", DynDNSSettings.Default["Token"].ToString().Trim());
            message = message.Replace ("<IPv4>", GetIPv4 ().Trim ());
            message = message.Replace ("<IPv6>", GetIPv6 ().Trim ());
             * */
            return message;
        }
    }
}
