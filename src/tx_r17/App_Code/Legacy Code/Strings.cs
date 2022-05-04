using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LegacyCode
{
    /// <summary>
    /// This class provides string manipulations utilities
    /// </summary>
    public class Strings
    {
        public Strings()
        { }

        // **************************************
        // ConvertToEnum
        // **************************************
        /// <summary>
        /// Convert any possible string-Value of a given enumeration
        /// type to its internal representation.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        // **************************************
        public static object ConvertToEnum(Type t, string Value)
        { return Enum.Parse(t, Value, true); }


        /// <summary>
        /// Returns a safe string
        /// </summary>
        /// <param name="ElementName"></param>
        /// <returns></returns>
        public static string GetSafeString(string elementValue)
        {
            return GetSafeString(elementValue, string.Empty);
        }


        /// <summary>
        /// Returns a safe string
        /// </summary>
        /// <param name="ElementValue"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static string GetSafeString(string elementValue, object defaultValue)
        {
            try
            {
                if (elementValue == null || elementValue.Trim() == string.Empty)
                    return defaultValue.ToString();
                else
                    return elementValue.Trim();
            }
            catch
            {
                return defaultValue.ToString();
            }
        }


        /// <summary>
        /// Returns a safe string
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="stringType"></param>
        /// <returns></returns>
        public static string GetSafeString(string elementName, StringType stringType)
        {
            return GetSafeString(elementName, stringType, string.Empty);
        }


        /// <summary>
        /// Returns a safe string
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="stringType"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static string GetSafeString(string elementName, StringType stringType, object DefaultValue)
        {
            try
            {
                switch (stringType)
                {
                    case StringType.QueryString:
                        return GetSafeString(HttpContext.Current.Request.QueryString[elementName], DefaultValue);
                    case StringType.Form:
                        return GetSafeString(HttpContext.Current.Request.Form[elementName], DefaultValue);
                    case StringType.Cookies:
                        return GetSafeString(HttpContext.Current.Request.Cookies[elementName].Value, DefaultValue);
                    case StringType.ServerVariables:
                        return GetSafeString(HttpContext.Current.Request.ServerVariables[elementName], DefaultValue);
                    case StringType.Params:
                        return GetSafeString(HttpContext.Current.Request.Params[elementName], DefaultValue);
                    case StringType.ApplicationSettings:
                        return GetSafeString(System.Configuration.ConfigurationManager.AppSettings[elementName], DefaultValue);
                    default:
                        return GetSafeString(elementName, DefaultValue);
                }
            }
            catch
            {
                return DefaultValue.ToString();
            }
        }


        /// <summary>
        /// Defines the type of string being requested
        /// </summary>
        public enum StringType
        {
            /// <summary>
            /// Returns the variable given in a safe string
            /// </summary>
            None,
            /// <summary>
            /// Returns a safe string from a querystring
            /// </summary>
            QueryString,
            /// <summary>
            /// Returns a safe string from a form
            /// </summary>
            Form,
            /// <summary>
            /// Returns a safe string from the cookies collection
            /// </summary>
            Cookies,
            /// <summary>
            /// Returns a safe string from the Server Variables collection
            /// </summary>
            ServerVariables,
            /// <summary>
            /// Returns a safe string from the params collection
            /// </summary>
            Params,
            /// <summary>
            /// Returns a safe string from the ConfigurationSettings.AppSettings collection
            /// </summary>
            ApplicationSettings,
        }
    }
}