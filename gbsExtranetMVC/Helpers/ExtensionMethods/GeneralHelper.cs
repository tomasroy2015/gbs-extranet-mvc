using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Transactions;
using System.Text.RegularExpressions;

namespace gbsExtranetMVC.Helpers.ExtensionMethods
{
    public class GeneralHelper
    {


        /// <summary>
        /// returns true or false, and converts null values into false.  Also converts "True" or "False" strings into boolean
        /// </summary>
        public static bool DenullBoolean(object Value)
        {

            bool Result;

            try
            {
                if (Value == null)
                {
                    Result = false;
                }
                else
                {
                    Result = Convert.ToBoolean(Value);

                }
            }
            catch
            {
                //if ( we can/t convert the value into a boolean, return false

                Result = false;

            }

            return Result;
        }

        /// <summary>
        /// returns true or false, and converts null values into false.  Also converts "True" or "False" strings into boolean
        /// </summary>
        public static bool DenullBooleanString(string value)
        {

            bool Result;

            try
            {
                if (value == null)
                {
                    Result = false;
                }
                else
                {
                    Result = Convert.ToBoolean(value);

                }
            }

            catch
            {
                //if ( we can/t convert the value into a boolean, return false

                Result = false;

            }

            return Result;

        }

        /// <summary>
        /// returns a string, and converts null values into an empty string
        /// </summary>
        public static string DenullString(Object Value)
        {

            String Result;

            try
            {

                if (Value == null)
                {
                    Result = "";
                }
                else
                {
                    Result = Value.ToString();
                }
            }
            catch
            {
                //if ( we can/t convert the value into a string, return an empty string

                Result = "";

            }

            return Result;

        }

        /// <summary>
        /// returns an integer, and converts null values into zero
        /// </summary>
        public static int DenullInt(int? Value)
        {
            int Result;

            try
            {

                if (Value == null)
                {
                    Result = 0;
                }
                else
                {
                    Result = Convert.ToInt32(Value);
                }

            }
            catch
            //if ( value isn/t an int, throw exception
            {
                throw;

            }

            return Result;

        }


        /// <summary>
        /// returns an integer, and converts zero values into nulls
        /// </summary>
        public static Int32? NullIfZeroInt32(Int32? Value)
        {
            Int32? result = null;

            try
            {

                if (Value != null)
                {
                    if (Value == 0)
                    {
                        result = null;
                    }
                    else
                    {
                        result = Convert.ToInt32(Value);
                    }
                }
            }

            catch
            //if ( value isn/t an int, throw exception
            {
                throw;

            }

            return result;

        }



        /// <summary>
        /// returns a short integer, and converts zero values into nulls
        /// </summary>
        public static Int16? NullIfZeroInt16(Int16? Value)
        {
            Int16? result = null;

            try
            {

                if (Value != null)
                {
                    if (Value == 0)
                    {
                        result = null;
                    }
                    else
                    {
                        result = Convert.ToInt16(Value);
                    }
                }
            }

            catch
            //if ( value isn/t an int, throw exception
            {
                throw;

            }

            return result;

        }

        //public static TransactionOptions SetTransactionTimeoutForDebugging(HttpContext httpContext)
        //{

        //    TransactionOptions transOptions = new TransactionOptions();

        //    if (httpContext.IsDebuggingEnabled)
        //    {
        //        //If we're debugging then allow a long time before the transaction times out, to prevent errors when stepping through the code
        //        transOptions.Timeout = TransactionManager.MaximumTimeout;

        //    }

        //    return transOptions;

        //}


        /// <summary>
        /// takes a url and adds http:// at the front if it doesn't already exist, so that hyperlinks to external sites work correctly
        /// </summary>
        public static string FormatWebsiteAddress(string siteAddress)
        {

            String result = "";
            String newSiteAddress = siteAddress;

            try
            {

                if (siteAddress == null)
                {
                    result = "";
                }
                else
                {
                    //if (!(siteAddress.ToUpper().StartsWith("HTTP://") || siteAddress.ToUpper().StartsWith("HTTPS://")))
                    //{
                    //    newSiteAddress = "http://" + siteAddress;
                    //}
                    //else
                    //{
                    //    newSiteAddress = siteAddress;
                    //}

                    ////Now check the url that we have created 
                    //Uri uriResult;
                    //bool success = Uri.TryCreate(newSiteAddress, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;

                    //Append the http part if necessary, and check it's a valid address format
                    bool success = IsValidUriWithHttp(ref newSiteAddress);

                    if (success)
                    {
                        result = newSiteAddress;
                    }
                    else
                    {
                        //The address can't be formatted correctly, but there's nothing we can do about this, so just return the original address
                        result = siteAddress;
                    }
                }
            }
            catch
            {
                throw;
            }

            return result;

        }


        /// <summary>
        /// works out if a website url is in a valid format.  This allows for addresses with or without http:// or https://
        /// </summary>
        public static bool CheckWebsiteAddressFormat(string siteAddress)
        {

            bool success;
            //String newSiteAddress = "";

            try
            {

                if (siteAddress == null)
                {
                    success = false;
                }
                else
                {

                    ////Uri.TryCreate allows some things that don't look like real websites, but it's better than nothing.
                    ////As an alternative we could use regex, but it's very complex and none of the existing examples on the web seem to be 100% correct.

                    ////Do the comparison on an absolute url, with http or https at the start
                    //if (!(siteAddress.ToUpper().StartsWith("HTTP://") || siteAddress.ToUpper().StartsWith("HTTPS://")))
                    //{
                    //    newSiteAddress = "http://" + siteAddress;
                    //}
                    //else
                    //{
                    //    newSiteAddress = siteAddress;
                    //}

                    //Uri uriResult;
                    //success = Uri.TryCreate(newSiteAddress, UriKind.Absolute, out uriResult);   // && uriResult.Scheme == Uri.UriSchemeHttp;

                    success = IsValidUriWithHttp(ref siteAddress);

                }
            }
            catch
            {
                success = false;
            }

            return success;
        }

        //Counts the number of occurrences of one string (innerString) inside another string (outerString)
        public static int GetCountOfStringInString(string outerString, string innerString)
        {
            int count = outerString.Length - outerString.Replace(innerString, "").Length;

            return count;

        }


        #region Private Functions

        //Checks if the address is in a valid format, and returns it with http:// added if it wasn't already there
        private static bool IsValidUriWithHttp(ref string siteAddress)
        {

            bool success;

            //Uri.TryCreate allows some things that don't look like real websites, but it's better than nothing.
            //As an alternative we could use regex, but it's very complex and none of the existing examples on the web seem to be 100% correct.

            //Do the comparison on an absolute url, with http or https at the start
            if (!(siteAddress.ToUpper().StartsWith("HTTP://") || siteAddress.ToUpper().StartsWith("HTTPS://")))
            {
                siteAddress = "http://" + siteAddress;
            }

            Uri uriResult;
            success = Uri.TryCreate(siteAddress, UriKind.Absolute, out uriResult);   // && uriResult.Scheme == Uri.UriSchemeHttp;

            return success;

        }

        #endregion



    }
}