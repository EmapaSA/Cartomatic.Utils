using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartomatic.Utils.Number
{
    /// <summary>
    /// string extensions for numeric utils
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// whether or not string content is numeric
        /// </summary>
        /// <param name="wouldBeANumber"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string wouldBeANumber)
        {
            return double.TryParse(wouldBeANumber, System.Globalization.NumberStyles.Any, NumberFormatInfo.InvariantInfo, out _);
        }

        /// <summary>
        /// Whether or not object content is numeric
        /// </summary>
        /// <param name="wouldBeANumber"></param>
        /// <returns></returns>
        public static bool IsNumeric(this object wouldBeANumber)
        {
            return Convert.ToString(wouldBeANumber, CultureInfo.InvariantCulture).IsNumeric();
        }
    }
}
