using NUnit.Framework;
using AwesomeAssertions;

using Cartomatic.Utils.Number;

namespace Number.Tests
{
    [TestFixture]
    public class NumberExtensionsTests
    {
        [Test]
        public void FitInRange_WhenNumberOk_ShouldNotModifyIt()
        {
            var num = 10d;

            var changed = num.FitInRange(0, 100);

            changed.Should().Be(num);
        }

        [Test]
        public void FitInRange_IfNumberLessThanLowerBounds_ShouldMakeItLowerBounds()
        {
            var num = 1000d;

            var changed = num.FitInRange(0, 100);

            changed.Should().Be(100);
        }

        [Test]
        public void FitInRange_IfNumberMoreThanUpperBounds_ShouldMakeItIUpperBounds()
        {
            var num = -10d;

            var changed = num.FitInRange(0, 100);

            changed.Should().Be(0);
        }
    }
}
