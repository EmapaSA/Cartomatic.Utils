using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace Cartomatic.Utils.Drawing
{
    public static partial class BitmapExtensions
    {

        private const int ExifOrientationId = 0x112; //274


        /// <summary>
        /// Applies img rotation as specified in an exif tag
        /// </summary>
        /// <param name="bitmap"></param>
        /// <remarks>Borrowed from https://stackoverflow.com/a/48347653 </remarks>
        /// <returns></returns>
        public static Bitmap ApplyExifRotation(this Bitmap bitmap)
        {
            if (!bitmap.PropertyIdList.Contains(ExifOrientationId))
                return bitmap;

            var prop = bitmap.GetPropertyItem(ExifOrientationId);
            int val = BitConverter.ToUInt16(prop.Value, 0);
            var rot = RotateFlipType.RotateNoneFlipNone;

            if (val == 3 || val == 4)
                rot = RotateFlipType.Rotate180FlipNone;
            else if (val == 5 || val == 6)
                rot = RotateFlipType.Rotate90FlipNone;
            else if (val == 7 || val == 8)
                rot = RotateFlipType.Rotate270FlipNone;

            if (val == 2 || val == 4 || val == 5 || val == 7)
                rot |= RotateFlipType.RotateNoneFlipX;

            if (rot != RotateFlipType.RotateNoneFlipNone)
                bitmap.RotateFlip(rot);

            return bitmap;
        }

        /// <summary>
        /// Returns a bitmap rotated by a specified angle
        /// </summary>
        /// <param name="b"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Bitmap Rotate(this Bitmap b, float angle)
        {
            var returnBitmap = new Bitmap(b.Width, b.Height, PixelFormat.Format32bppArgb);
            returnBitmap.SetResolution(b.HorizontalResolution, b.VerticalResolution);
            using (
                var g = Graphics.FromImage(returnBitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                g.TranslateTransform((float) b.Width / 2, (float) b.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-(float) b.Width / 2, -(float) b.Height / 2);
                g.DrawImage(b, new Point(0, 0));
            }

            return returnBitmap;
        }

    }
}
