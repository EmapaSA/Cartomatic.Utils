using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;



namespace Cartomatic.Utils.ApiClient
{
    /// <summary>
    /// base for rest api clients
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RestApiClient<T> : ApiClient<T>
        where T:IApiClientConfiguration
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
        protected internal virtual async Task<RestApiCallOutput<TOut>> ApiCallAsync<TOut>(string url, Method method = Method.GET, Dictionary<string, string> queryParams = null, object data = null)
        {
            return await RestApiClientUtils.ApiCallAsync<TOut>(url, method, queryParams, data);
        }
    }
}
