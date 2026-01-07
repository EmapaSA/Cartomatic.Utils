using System.Net;

namespace Cartomatic.Utils.ApiClient
{
    /// <summary>
    /// Health check call output data container
    /// </summary>
    public interface IHealthCheckData
    {
        /// <summary>
        /// Raw response object
        /// </summary>
        HttpStatusCode ResponseStatus { get; set; }
    }
    
}
