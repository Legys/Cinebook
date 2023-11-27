﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cinebook.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ApplicationErrors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ApplicationErrors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Cinebook.Resources.ApplicationErrors", typeof(ApplicationErrors).Assembly);
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
        ///   Looks up a localized string similar to {0} with provided {1} is already exists..
        /// </summary>
        internal static string AlreadyExistsError_Message {
            get {
                return ResourceManager.GetString("AlreadyExistsError.Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This entity already exists.
        /// </summary>
        internal static string AlreadyExistsError_Title {
            get {
                return ResourceManager.GetString("AlreadyExistsError.Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exception mapper doesn&apos;t support this type of an exception.
        /// </summary>
        internal static string DefaultException_Message {
            get {
                return ResourceManager.GetString("DefaultException.Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to One or more genres weren&apos;t found. Please check all provided ids..
        /// </summary>
        internal static string GenresNotFound_Message {
            get {
                return ResourceManager.GetString("GenresNotFound.Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Something has happened. Contact support..
        /// </summary>
        internal static string InternalServerError_Message {
            get {
                return ResourceManager.GetString("InternalServerError.Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Internal server error occured.
        /// </summary>
        internal static string InternalServerError_Title {
            get {
                return ResourceManager.GetString("InternalServerError.Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The operation could not be completed due to an internal logic error. This may occur if the system is in an unexpected state or if input values do not meet expected criteria..
        /// </summary>
        internal static string LogicErrorException_Message {
            get {
                return ResourceManager.GetString("LogicErrorException.Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Internal Logic Error.
        /// </summary>
        internal static string LogicErrorException_Title {
            get {
                return ResourceManager.GetString("LogicErrorException.Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} with provided ID is not found..
        /// </summary>
        internal static string NotFoundError_Message {
            get {
                return ResourceManager.GetString("NotFoundError.Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not found error.
        /// </summary>
        internal static string NotFoundError_Title {
            get {
                return ResourceManager.GetString("NotFoundError.Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Some of the seats are already booked.
        /// </summary>
        internal static string SeatsAlreadyBooked {
            get {
                return ResourceManager.GetString("SeatsAlreadyBooked", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No tickets found for this customer.
        /// </summary>
        internal static string TicketsNotFound {
            get {
                return ResourceManager.GetString("TicketsNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to One or more field are invalid.
        /// </summary>
        internal static string ValidationError_Message {
            get {
                return ResourceManager.GetString("ValidationError.Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Validation error.
        /// </summary>
        internal static string ValidationError_Title {
            get {
                return ResourceManager.GetString("ValidationError.Title", resourceCulture);
            }
        }
    }
}
