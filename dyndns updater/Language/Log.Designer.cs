﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DynDNS_Updater.Language {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Log {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Log() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DynDNS_Updater.Language.Log", typeof(Log).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DynDNS Updater added to Autostart.
        /// </summary>
        internal static string App_Autostart_Add {
            get {
                return ResourceManager.GetString("App_Autostart_Add", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DynDNS Updater removed from Autostart.
        /// </summary>
        internal static string App_Autostart_Remove {
            get {
                return ResourceManager.GetString("App_Autostart_Remove", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Update continued.
        /// </summary>
        internal static string App_Continue {
            get {
                return ResourceManager.GetString("App_Continue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Initialize application.
        /// </summary>
        internal static string App_Init {
            get {
                return ResourceManager.GetString("App_Init", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to dd.MM.yyyy - hh:mm:ss - .
        /// </summary>
        internal static string App_Log_Format_Timestamp {
            get {
                return ResourceManager.GetString("App_Log_Format_Timestamp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Update paused.
        /// </summary>
        internal static string App_Paused {
            get {
                return ResourceManager.GetString("App_Paused", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can not resolve IP address!.
        /// </summary>
        internal static string DNS_Resolve_Error {
            get {
                return ResourceManager.GetString("DNS_Resolve_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Force update.
        /// </summary>
        internal static string DNS_Update_Force {
            get {
                return ResourceManager.GetString("DNS_Update_Force", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Try IP update: {0}.
        /// </summary>
        internal static string DNS_Update_Try {
            get {
                return ResourceManager.GetString("DNS_Update_Try", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Provide username and password.
        /// </summary>
        internal static string Setting_Missing_UsernamePassword {
            get {
                return ResourceManager.GetString("Setting_Missing_UsernamePassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Save Settings.
        /// </summary>
        internal static string Setting_Saved {
            get {
                return ResourceManager.GetString("Setting_Saved", resourceCulture);
            }
        }
    }
}