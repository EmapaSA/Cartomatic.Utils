using System.Drawing.Imaging;
using System.Linq;

namespace Cartomatic.Utils.Drawing
{
    /// <summary>
    /// imaging utils
    /// </summary>
    public static class ImagingExtensions
    {
        /// <summary>
        /// returns ImageCodecInfo for given mime; used for setting up output format of image file
        /// </summary>
        public static ImageCodecInfo GetEncoderInfo(this string mimeType)
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(encoder => encoder.MimeType == mimeType || encoder.MimeType.Replace("image/", "") == mimeType);
        }
    }
}
