using System.Net;
using System.Threading.Tasks;

namespace Cartomatic.Utils.ApiClient
{
    public interface IApiClientWithHealthCheck<T> : IApiClient<T>, IApiClientWithHealthCheck
        where T : IApiClientConfiguration
    {
    }

    public interface IApiClientWithHealthCheck
    {
        /// <summary>
        /// Current health status if provided
        /// </summary>
        HealthStatus? HealthStatus { get; }

        /// <summary>
        /// Last time a health check has been performed (in ticks)
        /// </summary>
        long? LastHealthCheckTime { get; }

        /// <summary>
        /// Last time a healthy response has been retrieved (in ticks)
        /// </summary>
        long? LastHealthyResponseTime { get; }

        /// <summary>
        /// Last time an unhealthy response has been retrieved (in ticks)
        /// </summary>
        long? LastUnHealthyResponseTime { get; }

        /// <summary>
        /// Status code that describes a reason a client went dead
        /// </summary>
        HttpStatusCode? DeadReason { get; }

        /// <summary>
        /// Text message that describes a reason a client went dead
        /// </summary>
        string DeadReasonMessage { get; }

        /// <summary>
        /// Checks health of the client. Should set the <see cref="HealthStatus"/> and <see cref="LastHealthCheckData"/> if required; 
        /// </summary>
        /// <returns></returns>
        Task CheckHealthStatusAsync();

        /// <summary>
        /// Last api health check data if any
        /// </summary>
        /// <returns></returns>
        object GetLastHealthCheckData();

        /// <summary>
        /// Marks client as healthy
        /// </summary>
        void MarkAsHealthy();

        /// <summary>
        /// Marks client as dead
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        void MarkAsDead(HttpStatusCode statusCode, string message);
    }
}
