using System;
using System.Drawing;

namespace Cartomatic.Utils.Drawing
{
    public static partial class BitmapExtensions
    {
        /// <summary>
        /// Resizes a bitmap
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="destW"></param>
        /// <param name="destH"></param>
        /// <param name="maintainProportions"></param>
        /// <param name="interpolationMode"></param>
        /// <param name="smoothingMode"></param>
        /// <returns></returns>
        public static Bitmap Resize(this Bitmap bitmap, int destW, int destH, bool maintainProportions = true,
            System.Drawing.Drawing2D.InterpolationMode interpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic,
            System.Drawing.Drawing2D.SmoothingMode smoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias)
        {
            var output = new System.Drawing.Bitmap(destW, destH);

            if (!maintainProportions)
            {
                using (var g = System.Drawing.Graphics.FromImage(output))
                {
                    g.InterpolationMode = interpolationMode;
                    g.SmoothingMode = smoothingMode;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.DrawImage(bitmap, 0, 0, destW, destH);
                }
            }
            else
            {
                //get the scaling ratio
                var scaleRatio = Math.Min((double) destW / (double) bitmap.Width,
                    (double) destH / (double) bitmap.Height);

                //resize image appropriately
                bitmap = Resize(bitmap, (int) (bitmap.Width * scaleRatio), (int) (bitmap.Height * scaleRatio), false,
                    interpolationMode, smoothingMode);

                //and paint it in the middle of destination canvas
                using (var g = System.Drawing.Graphics.FromImage(output))
                {
                    g.InterpolationMode = interpolationMode;
                    g.SmoothingMode = smoothingMode;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.DrawImage(
                        bitmap,
                        (destW - bitmap.Width) / 2,
                        (destH - bitmap.Height) / 2,
                        bitmap.Width,
                        bitmap.Height
                    );
                }
            }

            return output;
        }

    }
}
