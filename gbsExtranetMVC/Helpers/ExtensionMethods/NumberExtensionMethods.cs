using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Helpers.ExtensionMethods
{
    public static class NumberExtensionMethods
    {

        /// <summary>
        /// Returns the long value that was passed in, or a zero if the value was null
        /// </summary>
        public static long PaceNullLongToZero(this long? input)
        {

            if (input == null)
            {
                input = 0;
            }

            return (long)input;

        }

        /// <summary>
        /// Returns the int value that was passed in, or a zero if the value was null
        /// </summary>
        public static int PaceNullIntToZero(this int? input)
        {

            if (input == null)
            {
                input = 0;
            }

            return (int)input;

        }

        /// <summary>
        /// Returns the decimal value that was passed in, or a zero if the value was null
        /// </summary>
        public static decimal PaceNullDecimalToZero(this decimal? input)
        {

            if (input == null)
            {
                input = 0;
            }

            return (decimal)input;

        }

        /// <summary>
        /// Returns the double value that was passed in, or a zero if the value was null
        /// </summary>
        public static double PaceNullDoubleToZero(this double? input)
        {

            if (input == null)
            {
                input = 0;
            }

            return (double)input;

        }


    }
}