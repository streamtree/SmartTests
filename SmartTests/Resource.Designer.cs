﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartTests {
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
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SmartTests.Resource", typeof(Resource).Assembly);
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
        ///   Looks up a localized string similar to BAD TEST: &apos;{0}&apos; is not an event of type &apos;{1}&apos;.
        /// </summary>
        internal static string BadTest_NotEvent {
            get {
                return ResourceManager.GetString("BadTest_NotEvent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BAD TEST: class &apos;{0}&apos; does not implement INotifyPropertyChanged.
        /// </summary>
        internal static string BadTest_NotINotifyPropertyChanged {
            get {
                return ResourceManager.GetString("BadTest_NotINotifyPropertyChanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BAD TEST: &apos;{0}&apos; is not a property of type &apos;{1}&apos;.
        /// </summary>
        internal static string BadTest_NotProperty {
            get {
                return ResourceManager.GetString("BadTest_NotProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BAD TEST: &apos;{0}&apos; is not a property nor a field of type &apos;{1}&apos;.
        /// </summary>
        internal static string BadTest_NotPropertyNorField {
            get {
                return ResourceManager.GetString("BadTest_NotPropertyNorField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BAD TEST: &apos;{0}&apos; is not a writable property.
        /// </summary>
        internal static string BadTest_NotWritableProperty {
            get {
                return ResourceManager.GetString("BadTest_NotWritableProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Event &apos;{0}&apos; was unexpected.
        /// </summary>
        internal static string ExpectedNotRaisedEvent {
            get {
                return ResourceManager.GetString("ExpectedNotRaisedEvent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Event &apos;{0}&apos; was expected.
        /// </summary>
        internal static string ExpectedRaisedEvent {
            get {
                return ResourceManager.GetString("ExpectedRaisedEvent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field &apos;{0}&apos; has changed.
        /// </summary>
        internal static string FieldChanged {
            get {
                return ResourceManager.GetString("FieldChanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property &apos;{0}&apos; has changed.
        /// </summary>
        internal static string PropertyChanged {
            get {
                return ResourceManager.GetString("PropertyChanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected property name &apos;{0}&apos; when PropertyChanged event was raised.
        /// </summary>
        internal static string UnexpectedPropertyNameWhenPropertyChangedRaised {
            get {
                return ResourceManager.GetString("UnexpectedPropertyNameWhenPropertyChangedRaised", resourceCulture);
            }
        }
    }
}