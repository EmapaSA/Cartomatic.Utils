using Cartomatic.Utils.Email;

namespace Cartomatic.Utils.ApiClient
{
    public abstract class ApiClientFarmConfiguration : IApiClientFarmConfiguration
    {
        /// <inheritdoc />
        public string ApiName { get; set; }

        /// <inheritdoc />
        public bool? MonitorHealth { get; set; }

        /// <inheritdoc />
        public int? AllowedUnhealthyClients { get; set; }

        /// <inheritdoc />
        public int? HealthCheckIntervalSeconds { get; set; }

        /// <inheritdoc />
        public int? AllowedHealthCheckFailures { get; set; }

        /// <inheritdoc />
        public string[] ClientStatusNotificationEmails { get; set; }

        /// <inheritdoc />
        public EmailAccount EmailSender { get; set; }

        /// <inheritdoc />
        public abstract IApiClientFarmConfiguration GetSafeCopy();
    }
}
