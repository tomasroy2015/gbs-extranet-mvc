using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Helpers.ExtensionMethods
{
    public static class ObjectExtensionMethods
    {

        /// <summary>
        /// Returns the string value that was passed in, or an empty string if the value was null
        /// </summary>
        public static string PaceNullObjectToEmptyString(this object input)
        {

            return GeneralHelper.DenullString(input);

        }

        /// <summary>
        /// Returns the bool value that was passed in, or false if the value was null
        /// </summary>
        public static bool PaceNullObjectToFalse(this object input)
        {

            return GeneralHelper.DenullBoolean(input);

        }

    }
}