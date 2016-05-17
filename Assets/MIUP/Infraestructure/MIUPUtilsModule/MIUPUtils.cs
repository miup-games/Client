using System;
using System.Collections.Generic;

namespace MIUP.Infraestructure.MIUPUtilsModule
{
    public static class MIUPUtils
    {
        /// <summary>
        /// Gets an enum values.
        /// </summary>
        /// <returns>The enum values.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T[] GetEnumValues<T>() {
            return (T[])Enum.GetValues(typeof(T));
        }
    }
}
