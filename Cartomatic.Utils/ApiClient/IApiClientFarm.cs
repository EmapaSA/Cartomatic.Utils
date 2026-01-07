using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Cartomatic.Utils.ApiClient
{
    /// <summary>
    /// Enforces ApiClient farm functionality
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiClientFarm<T>
        where T : IApiClient
    {
        /// <summary>
        /// Client farm configuration
        /// </summary>
        IApiClientFarmConfiguration Config { get; set; }

        /// <summary>
        /// client configs
        /// </summary>
        IEnumerable<IApiClientConfiguration> ClientConfigs { get; set; }
        
        /// <summary>
        /// Gets a client
        /// </summary>
        /// <returns></returns>
        T GetClient();

        /// <summary>
        /// Gets a client
        /// </summary>
        /// <returns></returns>
        Task<T> GetClientAsync();

        /// <summary>
        /// Gets a client with a specified id
        /// </summary>
        /// <param name="endPointId"></param>
        /// <returns></returns>
        T GetClient(string endPointId);

        /// <summary>
        /// Gets status data of a client with the given endpoint id
        /// </summary>
        /// <param name="endPointId"></param>
        /// <returns></returns>
        object GetClientData(string endPointId);

        /// <summary>
        /// Gets status data of all clients
        /// </summary>
        /// <returns></returns>
        IEnumerable<object> GetClientData();

        /// <summary>
        /// Marks client as healthy - this is to bring a client back to live once it went dead and then was fixed
        /// </summary>
        /// <param name="endPointId"></param>
        Task MarkClientAsHealthyAsync(string endPointId);

        /// <summary>
        /// Marks client as dead
        /// </summary>
        /// <param name="endPointId"></param>
        /// <param name="statusCode"></param>
        /// <param name="msg"></param>
        Task MarkClientAsDeadAsync(string endPointId, HttpStatusCode statusCode, string msg);

        /// <summary>
        /// Whether or not client is healthy; always true when farm configuration MonitorHealth is falsy or client is not IApiClientWithHealthCheck
        /// </summary>
        /// <param name="client"></param>
        /// <param name="force">Forces the check regardless internal tracking status</param>
        /// <returns></returns>
        Task<bool> CheckIfClientHealthy(IApiClient client, bool force = false);

        /// <summary>
        /// Returns clients status from the farm perspective
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApiClientFarmStatusInfo> GetFarmStatus();
    }
}
