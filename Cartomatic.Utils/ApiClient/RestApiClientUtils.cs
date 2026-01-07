using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RestSharp;



namespace Cartomatic.Utils.ApiClient
{
    /// <summary>
    /// base for rest api clients
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class RestApiClientUtils
    {
        /// <summary>
        /// Calls a specified REST backend at the specified url and using the specified method and params and parses the response to the appropriate output type
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="queryParams"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<RestApiCallOutput<TOut>> ApiCallAsync<TOut>(string url, Method method = Method.GET, Dictionary<string, string> queryParams = null, object data = null)
        {
            var client = new RestClient(url);
            var request = new RestRequest(method);
            request.AddHeader("Content-Type", "application/json");

            //add params if any
            if (queryParams != null && queryParams.Keys.Count > 0)
            {
                foreach (var key in queryParams.Keys)
                {
                    request.AddParameter(key, queryParams[key], ParameterType.QueryString);
                }
            }

            if ((method == Method.POST || method == Method.PUT) && data != null)
            {
                //use custom serializer on output! This is important as the newtonsoft's json stuff is used for the object serialization!
                request.RequestFormat = DataFormat.Json;
                request.JsonSerializer = new Cartomatic.Utils.RestSharpSerializers.NewtonSoftJsonSerializer();
                request.AddJsonBody(data);

#if DEBUG
                var json = request.JsonSerializer.Serialize(data);
#endif
            }


            //because of some reason RestSharp is bitching around when deserializing the arr / list output...
            //using Newtonsoft.Json instead

            var output = default(TOut);



            var resp = await client.ExecutePostTaskAsync(request);
            if (resp.IsSuccessful)
            {
                //an extra for binary output...
                if (resp.ContentType == "application/octet-stream")
                {
                    var respB = resp.RawBytes.Aggregate(string.Empty, (current, t) => current + ((current.Length > 0 ? ";" : "") + t));
                    output = (TOut) System.ComponentModel.TypeDescriptor.GetConverter(typeof(TOut)).ConvertFrom(respB);
                }
                else
                {
                    output = (TOut)Newtonsoft.Json.JsonConvert.DeserializeObject(resp.Content, typeof(TOut));
                }
            }
            else
            {
                var code = resp.StatusCode == 0 ? HttpStatusCode.ServiceUnavailable : resp.StatusCode;
                var msg = resp.ErrorMessage;
#if DEBUG
                if (code == HttpStatusCode.ServiceUnavailable)
                {
                    var ex = resp.ErrorException.InnerException;
                    while (ex != null)
                    {
                        msg += "; " + ex.Message;

                        ex = ex.InnerException;
                    }
                }
#endif
                throw new ApiCallException(code, msg, resp.ErrorException);
            }

            return new RestApiCallOutput<TOut>
            {
                Output = output,
                Response = resp
            };
        }

    }
}
