namespace Cartomatic.Utils.ApiClient
{
    /// <summary>
    /// generic IApiClient
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiClient<T> : IApiClient
        where T : IApiClientConfiguration
    {
    }

    /// <summary>
    /// Enforces ApiClient functionality
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Sets the configuration object
        /// </summary>
        /// <param name="cfg"></param>
        void SetConfig(IApiClientConfiguration cfg);

        /// <summary>
        /// Performs client internal initialization
        /// </summary>
        void Init();

        /// <summary>
        /// Returns an endpoint id of a client
        /// </summary>
        string EndPointId { get; }

        /// <summary>
        /// Returns endpoint's url configured via client configuration object
        /// </summary>
        string EndPointUrl { get; }
    }
}
