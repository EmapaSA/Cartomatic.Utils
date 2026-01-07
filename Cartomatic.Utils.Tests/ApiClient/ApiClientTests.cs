using AwesomeAssertions;
using NUnit.Framework;

namespace Cartomatic.Utils.ApiClient.Tests
{
    [TestFixture]
    public class ApiClientTests
    {
        [Test]
        public void ApiClientEndPoint_WhenConfigNotSet_ShouldBeNull()
        {
            var client = new Client();

            client.EndPointId.Should().BeNullOrEmpty();
        }

        [Test]
        public void ApiClientEndPoint_WhenConfigSet_ShouldBeTheSameAsConfigs()
        {
            var client = new Client();
            var config = new ClientConfiguration()
            {
                Host = "host",
                Port = 666
            };
            client.SetConfig(config);

            client.EndPointId.Should().Be(config.EndPointId);
        }
    }
}
