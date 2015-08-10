﻿using System;
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
            if (response.Contains("badauth") || response.Contains("nohost")
                 || response.Contains("abuse") || response.Contains("!yours")
                 || response.Contains("dnserr"))
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
            message = message.Replace("<Username>", Settings.AppSettings.Username);
            message = message.Replace("<Token>", Settings.AppSettings.Token);
            message = message.Replace("<Host>", "");    // TODO
            message = message.Replace("<IP>", GetIPv4().Trim());
            message = message.Replace("<IPv4>", GetIPv4().Trim());
            message = message.Replace ("<IPv6>", GetIPv6 ().Trim ());

            return message;
        }
    }
}
