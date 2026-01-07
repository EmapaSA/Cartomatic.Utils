using System;
using AwesomeAssertions;
using NUnit.Framework;

namespace Cartomatic.Utils.MercatorTests
{
    [TestFixture]
    public class MercatorTests
    {
        
        [TestCase(-77.035974, 38.898717)]
        [TestCase(0, 0)]
        [TestCase(21, 52)]
        public void WhenCalled_ShouldProperlyCpnverBackAndForth(double lon, double lat)
        {
            var converted = Cartomatic.Utils.Mercator.FromLonLat(lon, lat);
            var convertedBack = Cartomatic.Utils.Mercator.ToLonLat(converted.x, converted.y);

            Math.Round(convertedBack.lon, 6).Should().Be(lon);
            Math.Round(convertedBack.lat, 6).Should().Be(lat);
        }
    }
}
