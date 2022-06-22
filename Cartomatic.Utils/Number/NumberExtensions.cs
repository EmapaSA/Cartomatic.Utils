using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartomatic.Utils.Number
{
    /// <summary>
    /// number extensions
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        /// Fits a number in range if it is outside (to the closest bound)
        /// </summary>
        /// <param name="n"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double FitInRange(this double n, double min, double max)
        {
            if (n > max) n = max;
            if (n < min) n = min;

            return n;
        }

        /// <summary>
        /// fits a number in range
        /// </summary>
        /// <param name="n"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float FitInRange(this float n, double min, double max)
        {
            return (float) ((double) n).FitInRange(min, max);
        }

        private const double FLOAT_COMPARISON_PRECISION = 0.000000001;

        /// <summary>
        /// Whether or not 2 doubles can be considered equal
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool CloseEnough(this double number1, double number2, double? tolerance = null)
            => Math.Abs(number1 - number2) <= (tolerance ?? FLOAT_COMPARISON_PRECISION);

        /// <summary>
        /// Whether or not 2 doubles can be considered equal
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool CloseEnough(this double? number1, double? number2, double? tolerance = null)
            => number1.HasValue && number2.HasValue && Math.Abs(number1.Value - number2.Value) <= (tolerance ?? FLOAT_COMPARISON_PRECISION);

        /// <summary>
        /// Whether or not 2 floats can be considered equal
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool CloseEnough(this float number1, float number2, float? tolerance = null)
            => Math.Abs(number1 - number2) <= (tolerance ?? FLOAT_COMPARISON_PRECISION);

        /// <summary>
        /// Whether or not 2 floats can be considered equal
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool CloseEnough(this float? number1, float? number2, float? tolerance = null)
            => number1.HasValue && number2.HasValue && Math.Abs(number1.Value - number2.Value) <= (tolerance ?? FLOAT_COMPARISON_PRECISION);
    }
}
