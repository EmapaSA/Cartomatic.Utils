using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cartomatic.Utils.ApiClient.Tests
{
    class ClientConfiguration : ApiClientConfiguration
    {
    }

    class Client : ApiClient<ClientConfiguration>
    {
        /// <summary>
        /// Internal client initialisation procedure
        /// </summary>
        protected internal override void Init()
        {
        }
    }

    class ClientWithHealthChecks : ApiClientWithHealthCheck<ClientConfiguration>
    {
        protected internal override void Init() { }

        public override async Task CheckHealthStatusAsync()
        {
            HealthStatus = ApiClient.HealthStatus.Healthy;
            LastHealthCheckTime = DateTime.Now.Ticks;
        }

        /// <inheritdoc />
        public override object GetLastHealthCheckData()
        {
            throw new NotImplementedException();
        }
    }

    class ClientFarm : ApiClientFarm<Client>
    {
        public static ClientFarm GetTestFarm()
        {
            var cf = new ClientFarm
            {
                ClientConfigs = new List<IApiClientConfiguration>
                {
                    new ClientConfiguration {Host = "host1", Port = 1234},
                    new ClientConfiguration {Host = "host2", Port = 1234},
                    new ClientConfiguration {Host = "host3", Port = 1234}
                }
            };


            return cf;
        }

        protected override bool SkipClientBasedOnHealthCheckData(IApiClientWithHealthCheck client) => false;
        protected override string GetApiName()
        {
            throw new NotImplementedException();
        }
    }
    
}
