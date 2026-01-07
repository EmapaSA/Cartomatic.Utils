using System.IO;
using System.Threading.Tasks;

namespace Cartomatic.Utils
{
    /// <summary>
    /// stream extensions
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads stream into a byte arr
        /// </summary>
        /// <param name="input"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public static byte[] ReadStream(this Stream input, int? bufferSize = null)
        {
            byte[] buffer = new byte[bufferSize ?? 16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// copies stream to a new memory stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public static Stream CopyStream(this Stream stream, int? bufferSize = null)
        {
            var buffer = new byte[bufferSize ?? 16 * 1024];

            var ms = new MemoryStream();
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            if (ms.CanSeek)
                ms.Position = 0;

            return ms;
        }

        /// <summary>
        /// copies stream to a new memory stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public static async Task<Stream> CopyStreamAsync(this Stream stream, int? bufferSize = null)
        {
            var buffer = new byte[bufferSize ?? 16 * 1024];

            var ms = new MemoryStream();
            int read;
            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            if (ms.CanSeek)
                ms.Position = 0;

            return ms;
        }

    }
}
