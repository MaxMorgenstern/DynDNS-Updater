using System;
using System.Text.RegularExpressions;

namespace DynDNS_Updater.Logic
{
    class UpdateHelper
    {
        private static string Repository = "https://raw.githubusercontent.com/MaxMorgenstern/DynDNS-Updater/master/dyndns%20updater/Properties/AssemblyInfo.cs";//"https://api.github.com/repos/ShareX/ShareX/releases";

        
        public static bool IsUpdateAvailable()
        {
            Version o, a;
            return IsUpdateAvailable(out o, out a);
        }

        public static bool IsUpdateAvailable(out Version onlineVersion, out Version appVersion)
        {
            appVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string details = Helper.DoWebRequest(Repository);

            Regex regex = new Regex(@"Assembly: AssemblyVersion\(""(\d+)\.(\d+)\.(\d+)\.(\d+)""\)", RegexOptions.IgnoreCase);

            Match match = regex.Match(details);
            if (match.Success)
            {
                onlineVersion = new Version(Int32.Parse(match.Groups[1].Value),
                                            Int32.Parse(match.Groups[2].Value),
                                            Int32.Parse(match.Groups[3].Value),
                                            Int32.Parse(match.Groups[4].Value));
                if (onlineVersion.CompareTo(appVersion) > 0)
                    return true;
            }
            else
            {
                onlineVersion = appVersion;
            }

            return false;
        }
    }
}
