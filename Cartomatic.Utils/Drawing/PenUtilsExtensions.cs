using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace Cartomatic.Utils.Drawing
{
    public static class PenUtilsExtensions
    {
        /// <summary>
        /// Gets a line join from a string
        /// </summary>
        /// <param name="lineJoin"></param>
        /// <returns></returns>
        public static LineJoin GetLineJoin(this string lineJoin)
            => lineJoin?.ToLower() switch
            {
                "bevel" => LineJoin.Bevel,
                "miter" => LineJoin.Miter,
                "miterclipped" => LineJoin.MiterClipped,
                "round" => LineJoin.Round,
                _ => LineJoin.Round
            };
    }
}
