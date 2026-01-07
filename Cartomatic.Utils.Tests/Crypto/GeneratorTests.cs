using NUnit.Framework;
using AwesomeAssertions;

namespace Cartomatic.Utils.Crypto.Tests
{
    [TestFixture]
    public class GeneratorTests
    {
        [Test]
        public void StringGenerator_ShouldGenerate_StringOfProperLength()
        {
            var len = 15;
            var rndStr = Generator.GenerateRandomString(len);
            rndStr.Length.Should().Be(len);
        }
    }
}
