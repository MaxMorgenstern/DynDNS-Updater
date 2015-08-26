using DynDNS_Updater.Settings;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;


namespace DynDNS_Updater.Logic
{
	class Helper
	{
		public static void WriteLogFile(string logLine)
		{
			if (AppSettings.WriteLogFile)
				SaveToFile(logLine, AppSettings.LogFileName);
		}

		public static void SaveToFile(string logLine, string path)
		{
			try
			{
				StreamWriter SaveFile = File.AppendText(path);
				SaveFile.WriteLine(logLine);
				SaveFile.Close();
			} catch { }
		}


		// Open Webpage //////////////////////////////
		
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


		// Web Requests //////////////////////////////

		public static string DoWebRequest(string request)
		{
			Console.WriteLine(Language.Static.ConsoleLog_DoWebRequest + Language.Static.Colon + request);
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
			catch (Exception ex)
			{
				Console.WriteLine (ex);
				data = Language.Static.ResponseError;
			}
			return data;
		}


		// Open Webpage //////////////////////////////

		public static ReadOnlyCollection<CultureInfo> GetAvailableCultures()
		{
			return GetAvailableCultures(true);
		}

		public static ReadOnlyCollection<CultureInfo> GetAvailableCultures(bool addEN)
		{
			List<CultureInfo> list = new List<CultureInfo>();

			string startupDir = Application.StartupPath;
			Assembly asm = Assembly.GetEntryAssembly();

			CultureInfo neutralCulture = CultureInfo.InvariantCulture;
			if (asm != null)
			{
				NeutralResourcesLanguageAttribute attr = Attribute.GetCustomAttribute(asm, typeof(NeutralResourcesLanguageAttribute)) as NeutralResourcesLanguageAttribute;
				if (attr != null)
					neutralCulture = CultureInfo.GetCultureInfo(attr.CultureName);
			}
			list.Add(neutralCulture);

			if(addEN)
				list.Add(CultureInfo.GetCultureInfo("en-US"));

			if (asm != null)
			{
				string baseName = asm.GetName().Name;
				foreach (string dir in Directory.GetDirectories(startupDir))
				{
					// Check that the directory name is a valid culture
					DirectoryInfo dirinfo = new DirectoryInfo(dir);
					CultureInfo tCulture = null;
					try
					{
						tCulture = CultureInfo.GetCultureInfo(dirinfo.Name);
					}
					// Not a valid culture : skip that directory
					catch (ArgumentException)
					{
						continue;
					}

					// Check that the directory contains satellite assemblies
					if (dirinfo.GetFiles(baseName + ".resources.dll").Length > 0)
					{
						list.Add(tCulture);
					}

				}
			}
			return list.AsReadOnly();
		}
	
	}
}
