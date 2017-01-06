using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace gbsExtranetMVC.Helpers.ExtensionMethods
{
    public static class GenericExtensionMethods
    {
        /// <summary>
        /// From http://stackoverflow.com/questions/2178080/linq-to-sql-copy-original-entity-to-new-one-and-save
        /// Clones any object and returns the new cloned object.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="source">The original object.</param>
        /// <returns>The clone of the object.</returns>
        public static T PaceClone<T>(this T source)
        {
            var dcs = new DataContractSerializer(typeof(T));
            using (var ms = new System.IO.MemoryStream())
            {
                dcs.WriteObject(ms, source);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return (T)dcs.ReadObject(ms);
            }
        }

    }
}