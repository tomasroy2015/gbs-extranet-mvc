using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Helpers.ExtensionMethods
{
    public static class BooleanExtensionMethods
    {

        /// <summary>
        /// Returns the bool value that was passed in, or false if the value was null
        /// </summary>
        public static bool PaceNullBoolToFalse(this bool? input)
        {

            return GeneralHelper.DenullBoolean(input);

        }

        /// <summary>
        /// Converts a boolean to Yes or No string
        /// </summary>
        public static string PaceBoolToYesNo(this bool input)
        {

            string yesNoString = "";

            if (input)
            {
                yesNoString = "Yes";
            }
            else
            {
                yesNoString = "No";
            }

            return yesNoString;

        }

        /// <summary>
        /// Converts a boolean to Y or N string
        /// </summary>
        public static string PaceBoolToYorN(this bool input)
        {

            string yesNoString = "";

            if (input)
            {
                yesNoString = "Y";
            }
            else
            {
                yesNoString = "N";
            }

            return yesNoString;

        }

        /// <summary>
        /// Converts a nullable boolean to Yes or No string, returning null if null was passed in
        /// </summary>
        public static string PaceNullableBoolToYesNo(this bool? input)
        {
            if (input == null)
            {
                return null;
            }

            string yesNoString = "";

            if ((bool)input)
            {
                yesNoString = "Yes";
            }
            else
            {
                yesNoString = "No";
            }

            return yesNoString;

        }

        /// <summary>
        /// Converts a nullable boolean to Y or N string, returning null if null was passed in
        /// </summary>
        public static string PaceNullableBoolToYorN(this bool? input)
        {

            string yesNoString = "";

            if ((bool)input)
            {
                yesNoString = "Y";
            }
            else
            {
                yesNoString = "N";
            }

            return yesNoString;

        }
    }
}