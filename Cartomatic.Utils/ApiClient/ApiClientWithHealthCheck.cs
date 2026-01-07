using System;
using System.Net;
using System.Threading.Tasks;

namespace Cartomatic.Utils.ApiClient
{
    /// <summary>
    /// Api client base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ApiClientWithHealthCheck<T> : ApiClient<T>, IApiClientWithHealthCheck<T>
        where T : IApiClientConfiguration
    {
        /// <summary>
        /// current health check status
        /// </summary>
        public HealthStatus? HealthStatus { get; protected internal set; }

        /// <summary>
        /// Last time a health check has been performed (in ticks)
        /// </summary>
        public long? LastHealthCheckTime { get; protected internal set; }

        /// <inheritdoc />
        public abstract Task CheckHealthStatusAsync();

        /// <inheritdoc />
        public abstract object GetLastHealthCheckData();
        

        /// <summary>
        /// Last time a healthy response has been retrieved (in ticks)
        /// </summary>
        public long? LastHealthyResponseTime { get; protected internal set; }

        /// <summary>
        /// Logs time of a last healthy response - should be called when a healthy response was received
        /// </summary>
        protected internal virtual void LogLastHealthyResponse()
        {
            HealthStatus = ApiClient.HealthStatus.Healthy;
            LastHealthyResponseTime = DateTime.Now.Ticks;
        }

        /// <summary>
        /// Last time an unhealthy response has been retrieved (in ticks)
        /// </summary>
        public long? LastUnHealthyResponseTime { get; protected internal set; }

        /// <inheritdoc />
        public HttpStatusCode? DeadReason { get; protected internal set; }

        /// <inheritdoc />
        public string DeadReasonMessage { get; protected internal set; }

        /// <summary>
        /// Marks client as dead
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public virtual void MarkAsDead(HttpStatusCode statusCode, string message)
        {
            LastUnHealthyResponseTime = DateTime.Now.Ticks;
            HealthStatus = ApiClient.HealthStatus.Dead;
            DeadReason = statusCode;
            DeadReasonMessage = message;
        }

        /// <summary>
        /// Marks client as unhealthy
        /// </summary>
        protected internal virtual void MarkAsUnHealthy()
        {
            LastUnHealthyResponseTime = DateTime.Now.Ticks;
            HealthStatus = ApiClient.HealthStatus.Unhealthy;
        }

        /// <summary>
        /// Marks client as healthy
        /// </summary>
        public virtual void MarkAsHealthy()
        {
            LastHealthyResponseTime = null;
            HealthStatus = ApiClient.HealthStatus.Healthy;
            DeadReason = null;
            DeadReasonMessage = null;
        }
    }
}
