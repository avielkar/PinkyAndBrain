﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PinkyAndBrain.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.7.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DrawTrialMovementGraph {
            get {
                return ((bool)(this["DrawTrialMovementGraph"]));
            }
            set {
                this["DrawTrialMovementGraph"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10.5")]
        public double NoldusRatReponseSampleRate {
            get {
                return ((double)(this["NoldusRatReponseSampleRate"]));
            }
            set {
                this["NoldusRatReponseSampleRate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("440")]
        public int WaterBottleEmptyTime {
            get {
                return ((int)(this["WaterBottleEmptyTime"]));
            }
            set {
                this["WaterBottleEmptyTime"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50")]
        public int Frequency {
            get {
                return ((int)(this["Frequency"]));
            }
            set {
                this["Frequency"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>0 - Test no rat</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection RatNames {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["RatNames"]));
            }
            set {
                this["RatNames"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>Adam</string>\r\n  <string>Lior</string>\r\n  <string>Elad</string>\r\n</ArrayOf" +
            "String>")]
        public global::System.Collections.Specialized.StringCollection StudentsName {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["StudentsName"]));
            }
            set {
                this["StudentsName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\User\\Desktop\\protocols")]
        public string ProtocolsDirPath {
            get {
                return ((string)(this["ProtocolsDirPath"]));
            }
            set {
                this["ProtocolsDirPath"] = value;
            }
        }
    }
}
