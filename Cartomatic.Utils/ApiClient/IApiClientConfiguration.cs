namespace Cartomatic.Utils.ApiClient
{
    /// <summary>
    /// IApiClient configuration
    /// </summary>
    public interface IApiClientConfiguration
    {
        /// <summary>
        /// Protocol
        /// </summary>
        string Protocol { get; set; }

        /// <summary>
        /// Host
        /// </summary>
        string Host { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        int? Port { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Pass
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Request timeout
        /// </summary>
        int? Timeout { get; set; }

        /// <summary>
        /// Version of the API
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// Returns the configured client endpoint url 
        /// </summary>
        /// <returns></returns>
        string GetUrl();

        /// <summary>
        /// Returns an id uniquelly identifying an endpoint; This is pretty much a server identifier as it is based on the combination of protocol, host and port and this is unique per server.
        /// <para />
        /// Two confogurations with the same settings actually point to the very same endpoint
        /// </summary>
        string EndPointId { get; }

    }
}
