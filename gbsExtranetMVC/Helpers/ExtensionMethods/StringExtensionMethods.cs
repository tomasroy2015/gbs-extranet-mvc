
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
//using SocialPrescribingOrgs.Helpers;

namespace gbsExtranetMVC.Helpers.ExtensionMethods
{

    public static class StringExtensionMethods
    {

        //[Extension()]
        //public static string ConvertRowVersionToString(byte[] rowVersion)
        //{
        //    return Convert.ToBase64String(rowVersion.   ToArray());


        //}


        public static byte[] PaceStringToRowVersion(this string rowVersion)
        {

            dynamic b = Convert.FromBase64String(rowVersion);
            return b;

        }

        /// <summary>
        /// If a string has a value, the same value is returned.  If the string is empty, it converts to nothing
        /// </summary>
        public static string PaceEmptyStringToNothing(this string input)
        {

            if (string.IsNullOrEmpty(input))
            {
                input = null;
            }

            return input;

        }

        /// <summary>
        /// Returns the string value that was passed in, or an empty string if the value was null
        /// </summary>
        public static string PaceNullToEmptyString(this string input)
        {

            return GeneralHelper.DenullString(input);

        }

        /// <summary>
        /// Return true or false, and return false if the string is null
        /// </summary>
        public static bool PaceStringToBool(this string input)
        {

            return GeneralHelper.DenullBooleanString(input);

        }

        /// <summary>
        /// Converts a Yes / No to true / false, returning False if Null or empty string passed in, and throwing an exception if anything else was passed in.
        /// </summary>
        public static bool PaceYesNoStringToBool(this string input)
        {

            bool output = false;

            var str = GeneralHelper.DenullString(input);

            if (str.ToUpper() == "YES")
            {
                output = true;
            }
            else if (str.ToUpper() == "NO" || str == "")
            {
                output = false;
            }
            else
            {
                throw new Exception("Invalid Input String");
            }

            return output;

        }

        /// <summary>
        /// Converts a Y / N to true / false, returning Null if Y or N not passed in
        /// </summary>
        public static bool? PaceYNStringToNullableBool(this string input)
        {

            bool? output = null;

            var str = GeneralHelper.DenullString(input);

            if (str.ToUpper() == "Y")
            {
                output = true;
            }
            else if (str.ToUpper() == "N")
            {
                output = false;
            }

            return output;

        }



        /// <summary>
        /// Converts a Y / N to true / false, returning False if Null or empty string passed in, and throwing an exception if anything else was passed in.
        /// </summary>
        public static bool PaceYNStringToBool(this string input)
        {

            bool output = false;

            var str = GeneralHelper.DenullString(input);

            if (str.ToUpper() == "Y")
            {
                output = true;
            }
            else if (str.ToUpper() == "N" || str == "")
            {
                output = false;
            }
            else
            {
                throw new Exception("Invalid Input String");
            }

            return output;

        }



        /// <summary>
        /// Denulls the string and converts line breaks into <br />
        /// </summary>
        public static string PaceLineBreaksToHtml(this string input)
        {

            input = GeneralHelper.DenullString(input);

            input = input.Replace(Environment.NewLine, "<br />");

            //In case there are any line breaks without carriage returns, replace "\n" as well

            input = input.Replace("\n", "<br />");

            return input;

        }

        /// <summary>
        /// Denulls the string and converts <br> line breaks into c# line breaks />
        /// </summary>
        public static string PaceHtmlLineBreaksToCSharp(this string input)
        {

            input = GeneralHelper.DenullString(input);

            input = input.Replace("<br />", Environment.NewLine);
            input = input.Replace("<br/>", Environment.NewLine);
            input = input.Replace("<br>", Environment.NewLine);

            return input;

        }


        /// <summary>
        /// If a non-null string is input, trim spaces from it. If the string is null, return null.
        /// </summary>
        public static string PaceTrimStringIfNotNull(this string input)
        {

            if (input != null)
            {
                return input.Trim();
            }

            return null;

        }

        /// <summary>
        /// If a non-null string is input, convert it to upper case. If the string is null, return null.
        /// </summary>
        public static string PaceUpperStringIfNotNull(this string input)
        {

            if (input != null)
            {
                return input.ToUpper();
            }

            return null;

        }


        /// <summary>
        /// If the string is numeric, converts it to a long.
        /// If it's empty, converts it to 0
        /// If anything else, an error will occur
        /// </summary>
        public static long PaceStringToLong(this string input)
        {

            long output = 0;

            if (input != "")
            {
                output = Convert.ToInt64(input);
            }

            return output;

        }



    }

}