namespace Cartomatic.Utils.ApiClient
{
    public class ApiClientFarmStatusInfo
    {
        /// <summary>
        /// client endpoint id
        /// </summary>
        public string EndpointId { get; set; }

        /// <summary>
        /// client endpoint url
        /// </summary>
        public string EndpointUrl { get; set; }

        /// <summary>
        /// status of a client as farm sees it
        /// </summary>
        public ApiClientFarmStatus ApiClientFarmStatus { get; set; }

        /// <summary>
        /// status of a client as farm sees it in a verbose form
        /// </summary>
        public string ApiClientFarmStatusVerbose => $"{ApiClientFarmStatus}";

        /// <summary>
        /// Extra info as required
        /// </summary>
        public string Message { get; set; }
    }
}
