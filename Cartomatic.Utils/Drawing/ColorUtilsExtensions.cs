using System;
using System.Collections.Generic;
using System.Text;
using Cartomatic.Utils.Number;

namespace Cartomatic.Utils.Drawing
{
    public static class ColorUtilsExtensions
    {
        /// <summary>
        /// Converts opacity to alpha; opacity should be expressed as a value [0, 1]
        /// </summary>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public static int OpacityToAlpha(double opacity)
        {
            opacity = opacity.FitInRange(0, 1);
            return (int)Math.Round(255 * opacity);
        }

        /// <summary>
        /// Converts opacity to alpha; opacity should be expressed as a value [0, 1]
        /// </summary>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public static int OpacityToAlpha(double? opacity)
            => opacity.HasValue
                ? OpacityToAlpha(opacity.Value)
                : 255;

    }
}
