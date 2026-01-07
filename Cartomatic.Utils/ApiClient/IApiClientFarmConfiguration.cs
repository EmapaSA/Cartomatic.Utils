using Cartomatic.Utils.Email;

namespace Cartomatic.Utils.ApiClient
{
    /// <summary>
    /// IApiClient configuration
    /// </summary>
    public interface IApiClientFarmConfiguration
    {
        public string ApiName { get; set; }

        /// <summary>
        /// Whether or not client farm should monitor health of its clients; client must implement IApiClientWithHealthCheck in order to be health-checked
        /// </summary>
        bool? MonitorHealth { get; set; }

        /// <summary>
        /// How many unhealthy clients are allowed before monitoring endpoint starts returning erroneous response
        /// </summary>
        int? AllowedUnhealthyClients { get; set; }

        /// <summary>
        /// How often should the health check be performed
        /// </summary>
        int? HealthCheckIntervalSeconds { get; set; }

        /// <summary>
        /// How many times should a client be health-checked before it is marked as dead
        /// </summary>
        int? AllowedHealthCheckFailures { get; set; }

        /// <summary>
        /// Emails to report to when a client status changed
        /// </summary>
        string[] ClientStatusNotificationEmails { get; set; }

        /// <summary>
        /// Email sender details for sending out client status notifications
        /// </summary>
        EmailAccount EmailSender { get; set; }

        /// <summary>
        /// Returns a safe copy of the config (without the sensitive information)
        /// </summary>
        /// <returns></returns>
        IApiClientFarmConfiguration GetSafeCopy();
    }
}
