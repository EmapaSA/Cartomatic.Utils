using System;
using AwesomeAssertions;
using NUnit.Framework;

namespace Cartomatic.Utils.SafeLink.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void LinSerializesAndDeserializesProperly()
        {
            var link = new Link
            {
                ExpirationTime = DateTime.Now,
                Resource = "some resource"
            };
            var encKey = "some key";
            var encrypted = link.Encrypt(encKey);

            var decrypted = new Link(encrypted, encKey);

            link.ResourceType.Should().Be(decrypted.ResourceType);
            link.ExpirationTime.Should().Be(decrypted.ExpirationTime);
            link.Resource.Should().Be(decrypted.Resource);
        }

        [Test]
        public void IsExpired_WhenExpired_ShouldReturnTrue()
        {
            var link = new Link
            {
                ExpirationTime = DateTime.Now.AddMinutes(-1)
            };

            link.IsExpired.Should().BeTrue();
        }

        [Test]
        public void IsExpired_WhenNotExpired_ShouldReturnFalse()
        {
            var link = new Link
            {
                ExpirationTime = DateTime.Now.AddMinutes(1)
            };

            link.IsExpired.Should().BeFalse();
        }
    }
}
