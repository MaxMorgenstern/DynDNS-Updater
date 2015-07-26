using System;
using System.IO;
using System.Net;
using Microsoft.Win32;

namespace DynDNS_Updater.Logic
{
    class Helper
    {
        public static void OpenWebpage(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch
            {
                System.Diagnostics.Process.Start(GetStandardBrowserPath(), url);
            }
        }

        public static string GetStandardBrowserPath()
        {
            string browserPath = string.Empty;
            RegistryKey browserKey = null;

            try
            {
                //Read default browser path from Win XP registry key
                browserKey = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command", false);

                //If browser path wasn't found, try Win Vista (and newer) registry key
                if (browserKey == null)
                {
                    browserKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http", false); ;
                }

                //If browser path was found, clean it
                if (browserKey != null)
                {
                    //Remove quotation marks
                    browserPath = (browserKey.GetValue(null) as string).ToLower().Replace("\"", "");

                    //Cut off optional parameters
                    if (!browserPath.EndsWith("exe"))
                    {
                        browserPath = browserPath.Substring(0, browserPath.LastIndexOf(".exe") + 4);
                    }

                    //Close registry key
                    browserKey.Close();
                }
            }
            catch
            {
                //Return empty string, if no path was found
                return string.Empty;
            }
            //Return default browsers path
            return browserPath;
        }

        public static string DoWebRequest(string request)
        {
            string data = string.Empty;
            HttpWebResponse WebResp = null;
            try
            {
                try
                {
                    HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(request);
                    WebResp = (HttpWebResponse)WebReq.GetResponse();
                }
                catch (WebException ex)
                {
                    WebResp = ex.Response as HttpWebResponse;
                }

                Stream Answer = WebResp.GetResponseStream();
                StreamReader _Answer = new StreamReader(Answer);
                data = _Answer.ReadToEnd();
            }
            catch (WebException ex)
            {
				Console.WriteLine (ex);
                data = "dnserr";
            }
            return data;
        }
    }
}
