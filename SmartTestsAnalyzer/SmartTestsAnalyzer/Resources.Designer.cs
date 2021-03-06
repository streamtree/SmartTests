﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartTestsAnalyzer {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SmartTestsAnalyzer.Resources", typeof(Resources).GetTypeInfo().Assembly);
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
        ///   Looks up a localized string similar to Your range is not valid as min value is greater than max value.
        /// </summary>
        internal static string MinShouldBeLessThanMax_Description {
            get {
                return ResourceManager.GetString("MinShouldBeLessThanMax_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Min value ({0}) should be less than max value ({1}).
        /// </summary>
        internal static string MinShouldBeLessThanMax_MessageFormat {
            get {
                return ResourceManager.GetString("MinShouldBeLessThanMax_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Min value should be less than Max value.
        /// </summary>
        internal static string MinShouldBeLessThanMax_Title {
            get {
                return ResourceManager.GetString("MinShouldBeLessThanMax_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Some Test Cases are missing to have complete logical coverage..
        /// </summary>
        internal static string MissingCases_Description {
            get {
                return ResourceManager.GetString("MissingCases_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tests for &apos;{0}&apos; has some missing Test Cases: {1}.
        /// </summary>
        internal static string MissingCases_MessageFormat {
            get {
                return ResourceManager.GetString("MissingCases_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing Test Cases.
        /// </summary>
        internal static string MissingCases_Title {
            get {
                return ResourceManager.GetString("MissingCases_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A Parameter of the tested method has no associated case..
        /// </summary>
        internal static string MissingParameterCase_Description {
            get {
                return ResourceManager.GetString("MissingParameterCase_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Test for &apos;{0}&apos; has no Case for parameter &apos;{1}&apos;..
        /// </summary>
        internal static string MissingParameterCase_MessageFormat {
            get {
                return ResourceManager.GetString("MissingParameterCase_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing Parameter Case.
        /// </summary>
        internal static string MissingParameterCase_Title {
            get {
                return ResourceManager.GetString("MissingParameterCase_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A constant is expected so that the Analyzer can use it.
        /// </summary>
        internal static string NotAConstant_Description {
            get {
                return ResourceManager.GetString("NotAConstant_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A constant is expected.
        /// </summary>
        internal static string NotAConstant_MessageFormat {
            get {
                return ResourceManager.GetString("NotAConstant_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not a Constant.
        /// </summary>
        internal static string NotAConstant_Title {
            get {
                return ResourceManager.GetString("NotAConstant_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A Test Case has a wrong Parameter Name..
        /// </summary>
        internal static string WrongParameterName_Description {
            get {
                return ResourceManager.GetString("WrongParameterName_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Test for &apos;{0}&apos; has some invalid parameter &apos;{1}&apos;..
        /// </summary>
        internal static string WrongParameterName_MessageFormat {
            get {
                return ResourceManager.GetString("WrongParameterName_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wrong Parameter Name.
        /// </summary>
        internal static string WrongParameterName_Title {
            get {
                return ResourceManager.GetString("WrongParameterName_Title", resourceCulture);
            }
        }
    }
}
