using System;

namespace Cartomatic.Utils.Drawing
{
    public static class BitmapReadWriteExtensions
    {
        /// <summary>
        /// Gets an image - checks if this is a local path and opens an image; if this is a remote url, it requests an image from the web
        /// </summary>
        /// <param name="src">image source</param>
        /// <returns>Bitmap or null if not possible to read data</returns>
        public static System.Drawing.Bitmap GetImage(this string src)
        {
            System.Drawing.Bitmap output = null;

            //check if this is a url
            if (src.StartsWith("http")) //DM: this is not the smartest check, i know...
            {
                output = Cartomatic.Utils.Web.Dowload.GrabImageFromWeb(src);
                return output;
            }

            //path is not a url
            string path = src;
            if (!src.IsAbsolute())
            {
                path = AppDomain.CurrentDomain.BaseDirectory + src;
            }


            if (System.IO.File.Exists(path))
            {
                try
                {
                    output = new System.Drawing.Bitmap(path);
                }
                catch { }
            }

            return output;
        }

        /// <summary>
        /// converts byte arr to bitmap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap BitmapFromByteArr(this byte[] data)
        {
            System.Drawing.Bitmap output = null;

            System.IO.Stream s = new System.IO.MemoryStream(data);

            output = new System.Drawing.Bitmap(s);

            return output;
        }

        /// <summary>
        /// returns bitmap data as byte arr
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static byte[] BitmapToByteArr(this System.Drawing.Bitmap b)
        {
            byte[] output = null;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                b.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                output = ms.ToArray();
                b.Dispose();
            }

            return output;
        }
    }
}
