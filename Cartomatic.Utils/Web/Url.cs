using System.Collections.Generic;

namespace Cartomatic.Utils.Web
{
    public static class UrlExtensions
    {
        /// <summary>
        /// Extracts query param off the System.Net.HttpWebRequest
        /// </summary>
        /// <param name="pName">Param name</param>
        /// <param name="request">System.Net.HttpWebRequest object</param>
        /// <returns>Param value or null</returns>
        public static string GetParam(this System.Net.HttpWebRequest request, string pName)
        {
            string pValue = null;

            if (request != null)
            {
                pValue = System.Web.HttpUtility.ParseQueryString(request.Address.Query)[pName];
            }

            return pValue;
        }

#if NETFULL
        /// <summary>
        /// Extracts query param off the System.Web.HttpRequest
        /// </summary>
        /// <param name="pName">Param name</param>
        /// <param name="request">System.Web.HttpRequest object</param>
        /// <returns>Param value or null</returns>
        public static string GetParam(this System.Web.HttpRequest request, string pName)
        {
            string pValue = null;

            if (request != null)
            {
                pValue = request.QueryString[pName];
            }

            return pValue;
        }
#endif

        /// <summary>
        /// Encodes url
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Encoded url</returns>
        public static string EncodeUrl(this string url)
        {
            var output = string.Empty;
            if (!string.IsNullOrEmpty(url))
            {
                output = System.Web.HttpUtility.UrlEncode(url);
            }
            return output;
        }

        /// <summary>
        /// Decodes url
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Decoded url</returns>
        public static string DecodeUrl(this string url)
        {
            var output = string.Empty;
            if (!string.IsNullOrEmpty(url))
            {
                output = System.Web.HttpUtility.UrlDecode(url);
            }
            return output;
        }

        /// <summary>
        /// escapes url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string EscapeUrl(this string url)
        {
            var output = string.Empty;
            if (!string.IsNullOrEmpty(url))
            {
                output = System.Uri.EscapeUriString(url);
            }
            return output;
        }

        /// <summary>
        /// unescapes url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UnescapeUrl(this string url)
        {
            var output = string.Empty;
            if (!string.IsNullOrEmpty(url))
            {
                output = System.Uri.UnescapeDataString(url);
            }
            return output;
        }

        /// <summary>
        /// Replaces or appends a url param in the url provided as a string
        /// </summary>
        /// <param name="inUrl"></param>
        /// <param name="pName"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static string ReplaceUrlParam(this string inUrl, string pName, string pValue)
        {
            var newUrl = inUrl;

            //make the param name lowercase
            pName = pName.ToLower();

            //split url on question mark
            var urlParts = inUrl.Split('?');

            //looks like the url did not have any params
            if (urlParts.Length == 1)
            {
                newUrl = inUrl + "?" + pName + "=" + pValue;
                return newUrl;
            }

            //split params on &
            var urlParams = urlParts[1].Split('&');


            //now test the params
            var pFound = false;
            var outParams = new List<string>();
            foreach (var p in urlParams)
            {
                if (p.ToLower().IndexOf(pName + "=") > -1) //pName has been already made lowercase
                {
                    outParams.Add(pName + "=" + pValue);
                    pFound = true;
                }
                else
                {
                    outParams.Add(p);
                }
            }
            if (!pFound)
            {
                outParams.Add(pName.ToLower() + "=" + pValue);
            }

            //now reassemble the url
            newUrl = urlParts[0] + "?" + string.Join("&", outParams.ToArray());

            return newUrl;
        }

        /// <summary>
        /// Extracts a param off and url string
        /// </summary>
        /// <param name="inUrl"></param>
        /// <param name="pName"></param>
        /// <returns></returns>
        public static string GetParam(this string inUrl, string pName)
        {
            string pValue = null;

            //make the param name lowercase
            pName = pName.ToLower();

            //split url on question mark
            var urlParts = inUrl.Split('?');

            //looks like the url did not have any params
            if (urlParts.Length == 1)
            {
                return pValue;
            }

            //split params on &
            var urlParams = urlParts[1].Split('&');

            foreach (var p in urlParams)
            {
                if (p.ToLower().IndexOf(pName + "=") > -1) //pName has been already made lowercase
                {
                    string[] pParts = p.Split('=');

                    //make sure the param is actually the one being searched for 
                    //example: when infoformat is first, then when searching for format, invalid value would be returned
                    if (pParts[0].ToLower() == pName)
                    {
                        pValue = pParts[1];
                        break;
                    }
                }
            }

            return pValue;
        }
    }
}
