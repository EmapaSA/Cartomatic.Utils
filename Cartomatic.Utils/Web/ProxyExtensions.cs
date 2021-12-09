using System;
using System.Linq;

#if NETFULL
using System.Web;
#endif



#if NETSTANDARD2_0 || NETCOREAPP3_1 || NET5_0_OR_GREATER || NET6_0_OR_GREATER
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
#endif

namespace Cartomatic.Utils.Web
{
    /// <summary>
    /// web proxy extensions
    /// </summary>
    public static class ProxyExtensions
    {
        /// <summary>
        /// A default param used to glue in a proxied url
        /// </summary>
        public static string DefaultUrlParam { get; } = "url";


        /// <summary>
        /// Extracts proxied url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="urlParam"></param>
        /// <returns></returns>
        public static string ExtractProxiedUrl(this string url, string urlParam = null)
        {
            return new Uri(url).ExtractProxiedUrl(urlParam);
        }

        /// <summary>
        /// Extracts proxied url
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="urlParam"></param>
        /// <returns></returns>
        public static string ExtractProxiedUrl(this Uri uri, string urlParam = null)
        {
            var urlParams = System.Web.HttpUtility.UrlDecode(uri.Query.Replace("?", "&")).Split('&');

            var baseUrl =
                (urlParams.FirstOrDefault(s => s.StartsWith(urlParam ?? DefaultUrlParam)) ?? string.Empty).Replace(
                    $"{urlParam ?? DefaultUrlParam}=", "");
            var prms = string.Join("&",
                urlParams.Where(s => !string.IsNullOrEmpty(s) && !s.StartsWith(urlParam ?? DefaultUrlParam)));

            var url = $"{baseUrl}?{prms}";

            return url;
        }


        /// <summary>
        /// Proxies a given request
        /// </summary>
        /// <param name="context"></param>
        /// <param name="urlParam"></param>
        /// <param name="completeRequest"></param>
        public static void Proxy(this HttpContext context, string urlParam = null, bool completeRequest = true)
        {
#if NETFULL
            context.Request.Url.ExtractProxiedUrl(urlParam).Proxy(context, completeRequest);
#endif
#if NETSTANDARD2_0 || NETCOREAPP3_1 || NET5_0_OR_GREATER || NET6_0_OR_GREATER
            context.Request.GetDisplayUrl().ExtractProxiedUrl(urlParam).Proxy(context, completeRequest);
#endif

        }


        /// <summary>
        /// Executes a request to a specified url using specified context
        /// </summary>
        /// <param name="url"></param>
        /// <param name="context"></param>
        /// <param name="completeRequest"></param>
        public static void Proxy(this string url, HttpContext context, bool completeRequest = true)
        {
            //create a request with proper context
            var request = url.CreateHttpWebRequest(context.Request);

            url.CreateHttpWebRequest();

            var response = request.ExecuteRequest();

            //complete the request if required
            if (completeRequest)
            {
#if NETFULL
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
#endif
#if NETSTANDARD2_0 || NETCOREAPP3_1 || NET5_0_OR_GREATER || NET6_0_OR_GREATER

#endif
            }
        }

    }
}