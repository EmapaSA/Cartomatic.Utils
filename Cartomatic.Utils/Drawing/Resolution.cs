using System;
using System.Collections.Generic;
using System.Text;

namespace Cartomatic.Utils.Drawing
{
    public static class ResolutionExtensions
    {
        private const double MM_PER_INCH = 25.4;

        private const double CM_PER_INCH = MM_PER_INCH / 10;

        /// <summary>
        /// Converts dpi to dots per mm
        /// </summary>
        /// <param name="dpi"></param>
        /// <returns></returns>
        public static int DotsPerInchToDotsPerMm(this int dpi)
            => (int) Math.Round(dpi / MM_PER_INCH);

        /// <summary>
        /// Converts dpi to dots per cm
        /// </summary>
        /// <param name="dpi"></param>
        /// <returns></returns>
        public static int DotsPerInchToDotsPerCm(this int dpi)
            => (int)Math.Round(dpi / CM_PER_INCH);

        /// <summary>
        /// Converts a value expressed in mm to a value expressed in pixels at specified dpi
        /// </summary>
        /// <param name="mm"></param>
        /// <param name="dpi"></param>
        /// <returns></returns>
        public static int MmToPix(this double mm, int dpi)
            => (int)(dpi.DotsPerInchToDotsPerMm() * mm);

        /// <summary>
        /// Converts size expressed in pixels at given resolution to mm
        /// </summary>
        /// <param name="pix"></param>
        /// <param name="dpi"></param>
        /// <returns></returns>
        public static double PixToMm(this int pix, int dpi)
            => (MM_PER_INCH / dpi) * pix;
    }
}
