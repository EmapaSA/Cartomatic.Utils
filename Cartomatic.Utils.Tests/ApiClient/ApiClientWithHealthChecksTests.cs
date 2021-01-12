﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Cartomatic.Utils.ApiClient.Tests
{
    [TestFixture]
    public class ApiClientWithHealthChecksTests
    {
        [Test]
        public void ApiClientEndPoint_WhenConfigNotSet_ShouldBeNull()
        {
            var client = new ClientWithHealthChecks();

            client.EndPointId.Should().BeNullOrEmpty();
        }

        [Test]
        public void ApiClientEndPoint_WhenConfigSet_ShouldBeTheSameAsConfigs()
        {
            var client = new ClientWithHealthChecks();
            var config = new ClientConfiguration()
            {
                Host = "host",
                Port = 666
            };
            client.SetConfig(config);

            client.EndPointId.Should().Be(config.EndPointId);
        }

        [Test]
        public void ApiClient_WhenNewInstance_HealthStatusIsUnset()
        {
            var client = new ClientWithHealthChecks();

            client.HealthStatus.Should().BeNull();
            client.LastHealthCheckTime.Should().BeNull();

            client.LastHealthyResponseTime.Should().BeNull();
            client.LastUnHealthyResponseTime.Should().BeNull();
        }

        [Test]
        public static async Task ApiClient_CheckHealthStatus_ShouldProperlySetStatusAndCheckTime()
        {
            var client = new ClientWithHealthChecks();

            var start = DateTime.Now.Ticks;

            await client.CheckHealthStatusAsync();

            client.HealthStatus.Should().Be(HealthStatus.Healthy);
            client.LastHealthCheckTime.Should().BeGreaterOrEqualTo(start);
            client.LastHealthCheckTime.Should().BeLessOrEqualTo(DateTime.Now.Ticks);

        }

        [Test]
        public static void ApiClient_LogLastHealthyResponse_ShouldProperlySetStatusAndCheckTime()
        {
            var client = new ClientWithHealthChecks();

            var start = DateTime.Now.Ticks;

            client.LogLastHealthyResponse();

            client.HealthStatus.Should().Be(HealthStatus.Healthy);
            client.LastHealthyResponseTime.Should().BeGreaterOrEqualTo(start);
            client.LastHealthyResponseTime.Should().BeLessOrEqualTo(DateTime.Now.Ticks);

        }

        [Test]
        public static void ApiClient_MarkServiceAsDead_ShouldProperlySetStatusAndCheckTime()
        {
            var client = new ClientWithHealthChecks();

            var start = DateTime.Now.Ticks;

            client.MarkAsDead(0, "test");

            client.HealthStatus.Should().Be(HealthStatus.Dead);
            client.LastUnHealthyResponseTime.Should().BeGreaterOrEqualTo(start);
            client.LastUnHealthyResponseTime.Should().BeLessOrEqualTo(DateTime.Now.Ticks);

        }

        [Test]
        public static void ApiClient_MarkServiceAsUnhealthy_ShouldProperlySetStatusAndCheckTime()
        {
            var client = new ClientWithHealthChecks();

            var start = DateTime.Now.Ticks;

            client.MarkAsUnHealthy();

            client.HealthStatus.Should().Be(HealthStatus.Unhealthy);
            client.LastUnHealthyResponseTime.Should().BeGreaterOrEqualTo(start);
            client.LastUnHealthyResponseTime.Should().BeLessOrEqualTo(DateTime.Now.Ticks);

        }

        [Test]
        public static void ApiClient_MarkServiceAsHealthy_ShouldProperlySetStatusAndCheckTime()
        {
            var client = new ClientWithHealthChecks();

            client.MarkAsHealthy();

            client.HealthStatus.Should().Be(HealthStatus.Healthy);
            client.LastHealthyResponseTime.Should().BeNull();
        }
    }
}
