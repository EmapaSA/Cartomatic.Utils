using AwesomeAssertions;
using NUnit.Framework;

namespace Cartomatic.Utils.ApiClient.Tests
{
    [TestFixture]
    public class ApiConfigurationTests
    {
        class ClientConfiguration : ApiClientConfiguration
        {
        }

        [Test]
        public void ClientConfigurationEndPointIdentifier_ShouldBeUniqe_PerEndpointConfig()
        {
            var cfg1 = new ClientConfiguration
            {
                Host = "host1",
                Port = 1234
            };

            var cfg2 = new ClientConfiguration
            {
                Host = "host1",
                Port = 1234
            };

            var cfg3 = new ClientConfiguration
            {
                Host = "host2",
                Port = 1234
            };

            cfg1.EndPointId.Should().Be(cfg2.EndPointId);
            cfg1.EndPointId.Should().NotBe(cfg3.EndPointId);
        }
    }
}
