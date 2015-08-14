using System;
using System.Drawing;

namespace DynDNS_Updater.Logic
{
    class DynDNS
    {
        private static Entities.DDNSProvider CurrentProvider
        {
            get { return Entities.DDNSProviderList.GetProviderById(Settings.AppSettings.ProviderId); }
        }

        private static string GetIPv4URL
        {
            get
            {
                
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                if (rnd.Next(0, 100) > 60)
                    return CurrentProvider.IPv4ResolveURL;
                return "http://ipv4.icanhazip.com/";
            }
        }

        private static string GetIPv6URL
        {
            get
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                if (rnd.Next(0, 100) > 60)
                    return CurrentProvider.IPv6ResolveURL;
                return "http://ipv6.icanhazip.com/";
            }
        }

        private static string UpdateURL
        {
            get
            {
                return ReplaceTags(CurrentProvider.UpdateURL);
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
            if (response.Contains(Language.Static.ResponseBadAuth) || response.Contains(Language.Static.ResponseNoHost)
                 || response.Contains(Language.Static.ResponseAbuse) || response.Contains(Language.Static.ResponseNotYours)
                 || response.Contains(Language.Static.ResponseError))
            {
                color = Color.Red;
            }

            if (response.Contains(CurrentProvider.APIWarning))
            {
                success = true;
                color = Color.OrangeRed;
            }

            if (response.Contains(CurrentProvider.APISuccess))
            {
                success = true;
                color = Color.Green;
            }

            return response;
        }


        private static string ReplaceTags(string message)
        {
            message = message.Replace(Language.Static.TagUsername, Settings.AppSettings.Username);
            message = message.Replace(Language.Static.TagToken, Settings.AppSettings.Token);
            message = message.Replace(Language.Static.TagPassword, Settings.AppSettings.Token);
            message = message.Replace(Language.Static.TagHost, "");    // TODO
            message = message.Replace(Language.Static.TagIP, GetIPv4().Trim());
            message = message.Replace(Language.Static.TagIPv4, GetIPv4().Trim());
            message = message.Replace(Language.Static.TagIPv6, GetIPv6().Trim());

            return message;
        }
    }
}
