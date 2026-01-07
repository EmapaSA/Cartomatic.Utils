using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using RestSharp;



namespace Cartomatic.Utils.ApiClient
{
    /// <summary>
    /// base for rest api clients
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RestApiClientWithHealthCheck<T> : ApiClientWithHealthCheck<T>
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
            try
            {
                var response = await RestApiClientUtils.ApiCallAsync<TOut>(url, method, queryParams, data);

                if (response.Response.IsSuccessful)
                    LogLastHealthyResponse();

                return response;
            }
            catch(ApiCallException aex)
            {
                //status to be passed to a custom handler
                HealthStatus? status = null;
                var msg = string.Empty;

                if (
                    aex.ResponseStatus == HttpStatusCode.InternalServerError ||
                    aex.ResponseStatus == HttpStatusCode.ServiceUnavailable ||
                    aex.ResponseStatus == HttpStatusCode.GatewayTimeout ||
                    aex.ResponseStatus == HttpStatusCode.BadGateway
                )
                {
                    status = ApiClient.HealthStatus.Dead; //potentially could mark as unhealthy and let a farm handle marking as dead
                    msg = aex.Message;
                }

                if (
#if NETCOREAPP3_1
                        aex.ResponseStatus == HttpStatusCode.TooManyRequests ||
#else
                    (int) aex.ResponseStatus == 429 || //Too many requests
#endif

                    aex.ResponseStatus == HttpStatusCode.RequestTimeout
                )
                {
                    status = ApiClient.HealthStatus.Unhealthy;
                }
                    

                //other codes ignored, so 400, 404, etc by default will not cause marking a client as unhealthy.

                //custom err handling as required
                var (customStatus, customMessage) = HandleCustomErrors(status, aex);

                if (customStatus.HasValue)
                    status = customStatus;

                if (!string.IsNullOrWhiteSpace(customMessage))
                    msg = customMessage;

                switch (status)
                {
                    case ApiClient.HealthStatus.Unhealthy:
                        MarkAsUnHealthy();
                        break;
                    case ApiClient.HealthStatus.Dead:
                        MarkAsDead(aex.ResponseStatus ?? 0, msg);
                        break;
                }

                throw;
            }
        }

        /// <summary>
        /// Extension hook for handling custom errors based on the exception data; place to decide whether a service is alive or dead;
        /// This is called in the actual api call procedure when a call failed due to some reason and one may need to examine the service output to adjust the status client (for example waive off dead status based on the exception data)
        /// </summary>
        /// <param name="clientStatus">Status of the client decided by the caller</param>
        /// <param name="aex">Api exception</param>
        /// <returns></returns>
        protected internal abstract (ApiClient.HealthStatus? clientStatus, string message) HandleCustomErrors(ApiClient.HealthStatus? clientStatus, ApiCallException aex);
    }
}
