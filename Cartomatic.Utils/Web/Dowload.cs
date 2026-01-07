namespace Cartomatic.Utils.Web
{
    public class Dowload
    {
        /// <summary>
        /// Downloads a remote image from given url
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns>System.Drawing.Bitmap or null</returns>
        public static System.Drawing.Bitmap GrabImageFromWeb(string requestUrl)
        {
            //output image
            System.Drawing.Bitmap outputImage = null;

            System.Net.HttpWebResponse response = requestUrl.CreateHttpWebRequest().ExecuteRequest();

            //check if the data was successfully retrieved
            if (response != null && response.StatusCode.ToString().ToLower() == "ok")
            {
                System.IO.Stream stream = response.GetResponseStream();

                //maje sure it is possible to read response stream to image!
                try
                {
                    outputImage = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(stream, true);
                }
                catch { }
            }

            return outputImage;
        }
    }
}
