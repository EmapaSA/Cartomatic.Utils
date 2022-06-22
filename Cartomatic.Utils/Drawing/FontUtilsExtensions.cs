using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace Cartomatic.Utils.Drawing
{
    public static class FontUtilsExtensions
    {
        private const double MM_PER_INCH = 25.4;

        /// <summary>
        /// Works out a font style from string
        /// </summary>
        /// <param name="styleName"></param>
        /// <returns></returns>
        public static System.Drawing.FontStyle GetFontStyle(this string styleName)
            => styleName?.ToLower() switch
            {
                "bold" => System.Drawing.FontStyle.Bold,
                "italic" => System.Drawing.FontStyle.Italic,
                "strikeout" => System.Drawing.FontStyle.Strikeout,
                "underline" => System.Drawing.FontStyle.Underline,
                "regular" => System.Drawing.FontStyle.Regular,
                _ => System.Drawing.FontStyle.Regular
            };

        /// <summary>
        /// Works out a string format position from string
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static System.Drawing.StringFormat GetStringFormat(this string position)
            => position?.ToLower() switch
            {
                "t" => new System.Drawing.StringFormat{Alignment = System.Drawing.StringAlignment.Center , LineAlignment = System.Drawing.StringAlignment.Near},
                "b" => new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, LineAlignment = System.Drawing.StringAlignment.Far },
                "r" => new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Far, LineAlignment = System.Drawing.StringAlignment.Center },
                "tl" => new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Near, LineAlignment = System.Drawing.StringAlignment.Near },
                "tr" => new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Far, LineAlignment = System.Drawing.StringAlignment.Near },
                "bl" => new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Near, LineAlignment = System.Drawing.StringAlignment.Far },
                "br" => new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Far, LineAlignment = System.Drawing.StringAlignment.Far },
                "c" => new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, LineAlignment = System.Drawing.StringAlignment.Center },
                "l" => new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Near, LineAlignment = System.Drawing.StringAlignment.Center },
                _ => new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Near, LineAlignment = System.Drawing.StringAlignment.Center }
            };


        /// <summary>
        /// Returns a font size for given resolution
        /// </summary>
        /// <param name="fSize"></param>
        /// <param name="dpi"></param>
        /// <returns></returns>
        public static float GetFontSizeForResolution(this float fSize, int dpi)
        {
            var fSizeInMM = (MM_PER_INCH / 72) * fSize;

            return fSizeInMM.MmToPix(dpi);

        }
    }
}
