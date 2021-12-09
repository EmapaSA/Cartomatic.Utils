#if NETSTANDARD2_0 || NETCOREAPP3_1 || NET5_0_OR_GREATER || NET6_0_OR_GREATER
using Microsoft.AspNetCore.Http;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cartomatic.Utils
{
    public partial class RestApi
    {
        /// <summary>
        /// Extracts auth token off a request
        /// </summary>
        /// <returns></returns>
        protected internal static string ExtractAuthorizationHeader(HttpRequest request)
        {
            if (request == null)
                return null;

            //grab the auth token used by the requestee
            var authToken = string.Empty;

            if (request.Headers.ContainsKey("Authorization"))
            {
                authToken = request.Headers["Authorization"].First();
            }

            return authToken;
        }


        private static string[] _headersToIgnoreWhenTransferring = new[]
        {
            //ignore auth; it is transferred separately
            "authorization",
            //always avoid passing host header as it means when DNS resolves url to ip and request hits the sever it will resolve back to the domain thie request has originated from - the initial api
            //it would be pretty much ok, if the endpoints used different web servers or machines, but will cause problems when deployed from a single IIS!
            "host",

            //ignore content type as it may have been compressed on input and this call will send just json serialized data
            "content-type",

            //ignore content length! After all when new content is added for POST/PUT it will be worked out appropriately during data serialization and / or compression!
            //also for scenarios, when original request that initiated another request being performed in the background and being of different type (be it whatever, GET, PUT) and different length than the initiator, passing an invalid content length will make RestSharp request take ages to complete (likely because either client or server will be waiting for data me thinks ;)
            "content-length",

            //remove encoding, as incoming may be different than outgoing (for example gzipped input)
            "encoding"
        };

        /// <summary>
        /// Transfers request headers
        /// </summary>
        /// <param name="origHttpRequest">orig request to transfer headers from</param>
        /// <param name="restRequest">restsharp request to transfer the headers onto</param>
        /// <param name="customHdrs"></param>
        /// <param name="headersToSkip"></param>
        /// <param name="transferHdrs">Whether or not should transfer the headers already in the request</param>
        public static void TransferRequestHeaders(HttpRequest origHttpRequest, RestRequest restRequest, Dictionary<string, string> customHdrs, List<string> headersToSkip, bool transferHdrs)
        {
            if (origHttpRequest == null)
                return;

            foreach (var hdr in origHttpRequest.Headers)
            {
                if (_headersToIgnoreWhenTransferring.Contains(hdr.Key.ToLower()))
                    continue;

                //assume that custom headers overwrite whatever are the incoming headers
                //this is done outside of this method
                if (customHdrs?.ContainsKey(hdr.Key) == true)
                    continue;

                //any explicit headers to skip?
                if (headersToSkip != null && headersToSkip.Contains(hdr.Key))
                    continue;

                //finally check if remaining headers should be transferred
                if (!transferHdrs)
                    continue;

                //avoid headers starting with ':'
                //looks like they may be added to a request by the framework when processing it
                if (hdr.Key.StartsWith(":"))
                    continue;


                foreach (var hdrValue in hdr.Value)
                {
                    restRequest.AddHeader(hdr.Key, hdrValue);
                }
            }
        }

    }
}
#endif
