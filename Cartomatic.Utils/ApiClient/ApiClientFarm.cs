using Cartomatic.Utils.Email;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace Cartomatic.Utils.ApiClient
{
    /// <summary>
    /// An abstract for using API client farms; provides basic functionality for setting up a farm and retrieving clients
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ApiClientFarm<T> : IApiClientFarm<T>
        where T : IApiClient
    {
        /// <summary>
        /// api farm configuration
        /// </summary>
        public IApiClientFarmConfiguration Config { get; set; }


        protected IApiClientConfiguration[] _clientConfigsArr;

        /// <summary>
        /// Client configurations
        /// </summary>
        protected internal IEnumerable<IApiClientConfiguration> ClientConfigs { get; set; }
        IEnumerable<IApiClientConfiguration> IApiClientFarm<T>.ClientConfigs { get => ClientConfigs; set => ClientConfigs = value; }

        /// <summary>
        /// Returns client config by id
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        protected internal IApiClientConfiguration GetClientConfig(int idx)
        {
            _clientConfigsArr ??= ClientConfigs.ToArray();
            return _clientConfigsArr[idx];
        }

        /// <summary>
        /// Counter used to calculate next client to be used
        /// </summary>
        internal long UsageCounter { get; set; }


        /// <summary>
        /// Gets the idx of the next available mc config. uses round robin internally
        /// </summary>
        /// <returns></returns>
        internal int GetNextConfigIdx()
        {
            return (int)(++UsageCounter % ClientConfigs.Count());
        }

        /// <summary>
        /// Gets the idx of the next mc config in row. uses round robin internally
        /// </summary>
        /// <returns></returns>
        internal int GetNextConfigIdx(int idx)
        {
            return ++idx % ClientConfigs.Count();
        }

        internal const string NO_CLIENTS_CONFIGURED_ERR_MSG = "There are no clients configured for this farm.";

        /// <summary>
        /// Tests if can return a client
        /// </summary>
        internal void TestClientAvailability()
        {
            if (ClientConfigs == null || !ClientConfigs.Any())
                throw new InvalidOperationException(NO_CLIENTS_CONFIGURED_ERR_MSG);
        }

        /// <summary>
        /// Gets a next available configured client
        /// </summary>
        /// <returns></returns>
        /// <remarks>when a client implements IApiClientWithHealthCheck then a health check tracking is also performed</remarks>
        public T GetClient()
        {
            var client = default(T);

            Task.WaitAll(Task.Run(async () =>
            {
                client = await GetClientAsync();
            }));

            return client;
        }

        internal const string NO_HEALTHY_CLIENTS_ERR_MSG = "No healthy clients left in the farm.";

        /// <summary>
        /// Gets a next available configured client;
        /// </summary>
        /// <returns></returns>
        /// <remarks>when a client implements IApiClientWithHealthCheck then a health check tracking is also performed</remarks>
        public async Task<T> GetClientAsync()
        {
            TestClientAvailability();

            var clientCfgIdx = GetNextConfigIdx();
            var cfg = GetClientConfig(clientCfgIdx);

            var client = GetClient(cfg);

            var clientHealthy = await CheckIfClientHealthy(client);
            while (!clientHealthy)
            {
                clientCfgIdx = GetNextConfigIdx(clientCfgIdx);
                cfg = GetClientConfig(clientCfgIdx);
                var newClient = GetClient(cfg);

                //spin until client repeats. if all were dead and we come back to the initial client, then BOOM
                if (Equals(newClient, client))
                {
                    throw new Exception(NO_HEALTHY_CLIENTS_ERR_MSG);
                }

                clientHealthy = await CheckIfClientHealthy(newClient);
                if (clientHealthy)
                    client = newClient;
            }

            return client;
        }

        /// <summary>
        /// cached client instances, so can retrieve updated data such as health status
        /// </summary>
        private readonly Dictionary<IApiClientConfiguration, IApiClient> ClientInstances = new Dictionary<IApiClientConfiguration, IApiClient>();

        /// <summary>
        /// Creates a client instance for the provided config
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        private T GetClient(IApiClientConfiguration cfg)
        {
            if (ClientInstances.ContainsKey(cfg))
                return (T)ClientInstances[cfg];

            var client = (T)Activator.CreateInstance(typeof(T));
            client.SetConfig(cfg);
            client.Init();

            try
            {
                //in some scenarios this seems to be uninitialized
                ClientInstances[cfg] = client;
            }
            catch
            {
                //ignore
            }

            return client;
        }

        /// <summary>
        /// Gets client by endpoint id
        /// </summary>
        /// <param name="endPointId"></param>
        /// <returns></returns>
        public T GetClient(string endPointId)
        {
            TestClientAvailability();

            var cfg = ClientConfigs.FirstOrDefault(cf => cf.EndPointId == endPointId);

            if (cfg == null)
                return (T)(object)null;

            return GetClient(cfg);
        }


        /// <inheritdoc />
        public async Task<bool> CheckIfClientHealthy(IApiClient client, bool force = false)
        {
            //monitoring not configured or client does not support health checking, so always ok
            if (Config?.MonitorHealth != true || !(client is IApiClientWithHealthCheck clientWithHealthCheck))
                return true;

            var shouldCheckHealth = force || ShouldCheckHealth(clientWithHealthCheck);

            //no health checks for dead clients, they need to be resurrected first!
            if (clientWithHealthCheck.HealthStatus != HealthStatus.Dead && shouldCheckHealth)
                await clientWithHealthCheck.CheckHealthStatusAsync();

            var clientOk = clientWithHealthCheck.HealthStatus == HealthStatus.Healthy;

            if (clientOk)
            {
                ResetUnhealthyChecksCount(client);
            }

            //allows for temporarily omitting a client based on health check data - for example if client is too busy to handle new requests swiftly
            if (clientOk && SkipClientBasedOnHealthCheckData(clientWithHealthCheck))
                return false;

            if (!clientOk)
            {
                //uhuh, client not healthy, so need to mark it as unhealthy internally and keep track of this
                await HandleUnHealthyClient(clientWithHealthCheck, shouldCheckHealth);
            }

            return clientOk;
        }

        /// <summary>
        /// Whether or not a health check should be performed for given client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected internal bool ShouldCheckHealth(IApiClientWithHealthCheck client)
        {

            var shouldCheckHealth =
                //no need to test dead clients - dead means dead
                client.HealthStatus != HealthStatus.Dead &&

                (
                    //not yet tested
                    !(client.LastHealthCheckTime.HasValue ||
                      client.LastHealthyResponseTime.HasValue) ||

                    //both health checks expired - when one is ok, we're still good
                    !(
                        client.LastHealthCheckTime.HasValue &&
                        new TimeSpan(DateTime.Now.Ticks - client.LastHealthCheckTime.Value).TotalSeconds < Config.HealthCheckIntervalSeconds ||

                        client.LastHealthyResponseTime.HasValue &&
                        new TimeSpan(DateTime.Now.Ticks - client.LastHealthyResponseTime.Value).TotalSeconds < Config.HealthCheckIntervalSeconds
                    )

                );

            return shouldCheckHealth;
        }

        /// <inheritdoc />
        public IEnumerable<ApiClientFarmStatusInfo> GetFarmStatus()
        {
            var output = new List<ApiClientFarmStatusInfo>();

            foreach (var cfg in ClientConfigs)
            {
                var client = GetClient(cfg);
                output.Add(GetApiClientFarmStatusInfo(client));
            }

            return output;
        }

        /// <summary>
        /// Returns a client status from farm perspective
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected ApiClientFarmStatusInfo GetApiClientFarmStatusInfo(IApiClient client)
        {
            var farmStatus = new ApiClientFarmStatusInfo
            {
                EndpointId = client.EndPointId,
                EndpointUrl = client.EndPointUrl,
                ApiClientFarmStatus = ApiClientFarmStatus.Operational
            };

            if (client is IApiClientWithHealthCheck hcClient)
            {

                switch (hcClient.HealthStatus)
                {
                    case HealthStatus.Dead:
                        farmStatus.ApiClientFarmStatus = ApiClientFarmStatus.Disabled;
                        farmStatus.Message = hcClient.DeadReasonMessage;
                        break;

                    case HealthStatus.Unhealthy:
                        var hcCount = Config.AllowedHealthCheckFailures ?? 0;
                        var remaining = hcCount;
                        if (UnhealthyClients.ContainsKey(hcClient))
                        {
                            remaining -= UnhealthyClients[hcClient];
                        }

                        farmStatus.ApiClientFarmStatus = ApiClientFarmStatus.TemporarilyDisabled;
                        farmStatus.Message = $"Temporarily disabled - remaining healthchecks: {remaining} of {hcCount}";
                        break;
                }
            }

            return farmStatus;
        }

        /// <summary>
        /// keeps track of unhealthy clients
        /// </summary>
        protected Dictionary<IApiClientWithHealthCheck, int> UnhealthyClients = new Dictionary<IApiClientWithHealthCheck, int>();

        /// <summary>
        /// Tracks unhealthy client status
        /// </summary>
        /// <param name="client"></param>
        /// <param name="healthChecked"></param>
        protected async Task HandleUnHealthyClient(IApiClientWithHealthCheck client, bool healthChecked)
        {
            //ignore dead clients. once dead, always dead
            if (client.HealthStatus == HealthStatus.Dead)
                return;

            //only handle clients that were just health-checked; health check for other clients is pending
            if (healthChecked)
            {
                if (!UnhealthyClients.ContainsKey(client))
                {
                    UnhealthyClients[client] = 0;
                }

                UnhealthyClients[client]++;

                if (Config.AllowedHealthCheckFailures.HasValue &&
                    UnhealthyClients[client] > Config.AllowedHealthCheckFailures)
                {
                    await MarkClientAsDeadAsync(client, 0, $"Health check failed {UnhealthyClients[client]} times");
                }
            }
        }

        /// <summary>
        /// Whether or not a client should be skipped based on the health check data;
        /// extension hook for allowing extra client tests based on the actual data a health check returned
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected abstract bool SkipClientBasedOnHealthCheckData(IApiClientWithHealthCheck client);

        /// <summary>
        /// Reports client status to a set of configured emails
        /// </summary>
        /// <returns></returns>
        protected async Task ReportClientStatus(IApiClient client, HealthStatus status)
        {
            var emailTpl = GetClienStatustNotificationEmailTitleAndMsg(client, status);

            if (Config.EmailSender == null || Config.ClientStatusNotificationEmails == null ||
                !Config.ClientStatusNotificationEmails.Any())
                return;

            var emailSender = new EmailSender();

            foreach (var email in Config.ClientStatusNotificationEmails)
            {
                //fire 'n' forget
                emailSender.Send(Config.EmailSender, emailTpl, email);
                //await emailSender.SendAsync(Config.EmailSender, emailTpl, email);
            }
        }

        /// <summary>
        /// Returns a title for dead client notification email
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected virtual EmailTemplate GetClienStatustNotificationEmailTitleAndMsg(IApiClient client, HealthStatus status)
        {
            var title = string.Empty;
            var msg = string.Empty;

            switch (status)
            {
                case HealthStatus.Dead:
                    title = "Api client DEAD!";
                    msg = "One of the configured api clients went dead.";
                    break;
                case HealthStatus.Healthy:
                    title = "Api client ALIVE!";
                    msg = "Api client has been reactivated.";
                    break;
            }

            title = $"[{GetApiName()}] {title}";

            msg = $@"{msg}

Client details:
{JsonConvert.SerializeObject(GetClientData(client), Formatting.Indented)}
";

            return new EmailTemplate
            {
                Title = title,
                Body = msg,
                IsBodyHtml = false
            };
        }

        /// <summary>
        /// Gets a user friendly api name for the email communication
        /// </summary>
        /// <returns></returns>
        protected abstract string GetApiName();

        /// <summary>
        /// Returns client specific data
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected virtual Dictionary<string, object> GetClientData(IApiClient client)
        {
            if (client == null)
                return null;

            var clientData = new Dictionary<string, object>
            {
                { nameof(client.EndPointId), client.EndPointId },
                { nameof(client.EndPointUrl), client.EndPointUrl }
            };


            if (client is IApiClientWithHealthCheck hcClient)
            {
                clientData.Add(nameof(hcClient.HealthStatus), hcClient.HealthStatus);
                clientData.Add($"{nameof(hcClient.HealthStatus)}Verbose", $"{hcClient.HealthStatus}");

                clientData.Add($"{nameof(hcClient.LastHealthyResponseTime)}Ticks", hcClient.LastHealthyResponseTime);
                clientData.Add(nameof(hcClient.LastHealthyResponseTime), hcClient.LastHealthyResponseTime.HasValue ? new DateTime(hcClient.LastHealthyResponseTime.Value) : (DateTime?)null);

                clientData.Add($"{nameof(hcClient.LastHealthCheckTime)}Ticks", hcClient.LastHealthCheckTime);
                clientData.Add(nameof(hcClient.LastHealthCheckTime), hcClient.LastHealthCheckTime.HasValue ? new DateTime(hcClient.LastHealthCheckTime.Value) : (DateTime?)null);

                clientData.Add($"{nameof(hcClient.LastUnHealthyResponseTime)}Ticks", hcClient.LastUnHealthyResponseTime);
                clientData.Add(nameof(hcClient.LastUnHealthyResponseTime), hcClient.LastUnHealthyResponseTime.HasValue ? new DateTime(hcClient.LastUnHealthyResponseTime.Value) : (DateTime?)null);

                clientData.Add(nameof(hcClient.DeadReason), hcClient.DeadReason);
                clientData.Add($"{nameof(hcClient.DeadReason)}Verbose", $"{hcClient.DeadReason}");
                clientData.Add(nameof(hcClient.DeadReasonMessage), hcClient.DeadReasonMessage);

                clientData.Add("LastHealthCheckData", hcClient.GetLastHealthCheckData());
            }

            var farmStatus = GetApiClientFarmStatusInfo(client);
            clientData.Add(nameof(farmStatus.ApiClientFarmStatus), farmStatus.ApiClientFarmStatus);
            clientData.Add(nameof(farmStatus.ApiClientFarmStatusVerbose), farmStatus.ApiClientFarmStatusVerbose);
            clientData.Add($"{nameof(farmStatus.ApiClientFarmStatus)}Message", farmStatus.Message);

            return clientData;
        }



        /// <inheritdoc />
        public virtual object GetClientData(string endPointId)
        {
            return GetClientData(GetClient(endPointId)); ;
        }

        /// <inheritdoc />
        public virtual IEnumerable<object> GetClientData()
        {
            return ClientConfigs.Select(cfg => GetClientData(GetClient(cfg)));
        }

        /// <inheritdoc />
        public async Task MarkClientAsHealthyAsync(string endPointId)
        {
            var client = GetClient(endPointId);
            if (client is IApiClientWithHealthCheck clientWithHealthCheck)
            {
                clientWithHealthCheck.MarkAsHealthy();

                //reset client unhealthy status counters
                ResetUnhealthyChecksCount(client);
            }

            await ReportClientStatus(client, HealthStatus.Healthy);
        }

        /// <summary>
        /// Reset failed healthchecks counter
        /// </summary>
        /// <param name="client"></param>
        protected void ResetUnhealthyChecksCount(IApiClient client)
        {
            if (client is IApiClientWithHealthCheck clientWithHealthCheck)
            {
                //reset client unhealthy status counters
                if (UnhealthyClients.ContainsKey(clientWithHealthCheck))
                {
                    UnhealthyClients[clientWithHealthCheck] = 0;
                }
            }
        }

        /// <inheritdoc />
        public async Task MarkClientAsDeadAsync(string endPointId, HttpStatusCode statusCode, string msg)
        {
            var client = GetClient(endPointId);
            if (client is IApiClientWithHealthCheck clientWithHealthCheck)
            {
                await MarkClientAsDeadAsync(clientWithHealthCheck, statusCode, msg);
            }
        }

        /// <summary>
        /// Marks client as dead
        /// </summary>
        /// <param name="client"></param>
        /// <param name="statusCode"></param>
        /// <param name="msg"></param>
        protected async Task MarkClientAsDeadAsync(IApiClientWithHealthCheck client, HttpStatusCode statusCode, string msg)
        {
            client.MarkAsDead(statusCode, msg);
            await ReportClientStatus(client as IApiClient, HealthStatus.Dead);
        }
    }
}
