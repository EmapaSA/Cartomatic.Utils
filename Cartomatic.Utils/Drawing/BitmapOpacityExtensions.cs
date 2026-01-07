using System.Drawing;
using System.Drawing.Imaging;
using Cartomatic.Utils.Number;



namespace Cartomatic.Utils.Drawing
{
    /// <summary>
    /// bitmap extensions
    /// </summary>
    public static partial class BitmapExtensions
    {
        /// <summary>
        /// Sets opacity of a bitmap
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public static Bitmap SetBitmapOpacity(this Bitmap bitmap, double opacity)
        {
            return bitmap.SetBitmapOpacity((float) opacity);
        }

        /// <summary>
        /// Sets opacity of a bitmap
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public static Bitmap SetBitmapOpacity(this Bitmap bitmap, float opacity)
        {
            float op = opacity.FitInRange(0, 1);

            if (op == 0)
            {
                op = 0.03F;
            }

            //bitmap that will make a base for the semi opaque raster
            Bitmap output = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            //color matrix
            ColorMatrix cm = new ColorMatrix();
            cm.Matrix33 = op; //this is a bot of the color matrix responsible for opacity

            //cm.Matrix03 = 0.3F;
            //cm.Matrix13 = 0.3F;
            //cm.Matrix23 = 0.3F;
            //cm.Matrix43 = 0.3F;

            //image attributes
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            //paint the opaque bitmap on top of the bottom bitmap with given image attributes
            using (Graphics g = Graphics.FromImage(output))
            {
                g.DrawImage(bitmap, new Rectangle(0, 0, output.Width, output.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, ia);
            }

            return output;
        }

        /// <summary>
        /// Overlays a bitmap over the other one with a specified opacity
        /// </summary>
        /// <param name="baseBitmap"></param>
        /// <param name="overlayBitmap"></param>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public static Bitmap OverlayBitmapWithOpacity(this Bitmap baseBitmap, Bitmap overlayBitmap, double opacity)
        {
            return baseBitmap.OverlayBitmapWithOpacity(overlayBitmap, (float) opacity);
        }

        /// <summary>
        /// Overlays a bitmap over the other one with a specified opacity
        /// </summary>
        /// <param name="baseBitmap"></param>
        /// <param name="overlayBitmap"></param>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public static Bitmap OverlayBitmapWithOpacity(this Bitmap baseBitmap, Bitmap overlayBitmap, float opacity)
        {
            Bitmap output = new Bitmap(baseBitmap.Width, baseBitmap.Height, PixelFormat.Format32bppArgb);

            //color matrix
            ColorMatrix cm = new ColorMatrix();
            cm.Matrix33 = opacity; //this is a bot of the color matrix responsible for opacity

            //image attributes
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            //paint the opaque bitmap on top of the bottom bitmap with given image attributes
            using (Graphics g = Graphics.FromImage(output))
            {
                //first paint the incoming bitmap on the output
                g.DrawImage(baseBitmap, new Rectangle(0, 0, baseBitmap.Width, baseBitmap.Height), new Rectangle(0, 0, baseBitmap.Width, baseBitmap.Height), GraphicsUnit.Pixel);

                //then the transparent one
                g.DrawImage(overlayBitmap, new Rectangle(0, 0, baseBitmap.Width, baseBitmap.Height), 0, 0, overlayBitmap.Width, overlayBitmap.Height, GraphicsUnit.Pixel, ia);
            }

            return output;
        }
    }
}
