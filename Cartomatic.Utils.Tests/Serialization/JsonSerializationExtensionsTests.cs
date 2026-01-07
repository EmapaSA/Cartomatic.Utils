using System;
using AwesomeAssertions;
using NUnit.Framework;

namespace Cartomatic.Utils.Serialization.Tests
{
    [TestFixture]
    public class JsonSerializationExtensionsTests
    {
        [Test]
        public void DeserializeFromJson_WhenPassedArbitraryObject_DeserializesItProperly()
        {
            string json = MakeTestJson();
            var refObj = MakeTestObject();

            var obj = json.DeserializeFromJson<TestObjectWithSimpleTypes>();

            obj.Should().NotBeNull();
            obj.Should().BeEquivalentTo(refObj);
        }

        [Test]
        public void DeserializeFromJson_WhenPassedInvalidJson_ShouldNotThrowButReturnNullByDefault()
        {
            var invalidJson = MakeInvalidTestJson();
            var obj = invalidJson.DeserializeFromJson<TestObjectWithSimpleTypes>();

            obj.Should().BeNull();
        }

        [Test]
        public void DeserializeFromJson_WhenPassedInvalidJsonAndSilentSetToTrue_ShouldThrow()
        {
            var json = MakeInvalidTestJson();
            Action act = () => json.DeserializeFromJson<TestObjectWithSimpleTypes>(false);

            act.Should().Throw<Exception>();
        }

        [Test]
        public void SerializeToJson_WhenCalledOnArbitraryObject_SerializesItProperly()
        {
            var obj = MakeTestObject();
            var refJson = MakeTestJson();

            string json = obj.SerializeToJson();

            json.Should().BeEquivalentTo(refJson);
        }

        private TestObjectWithSimpleTypes MakeTestObject()
        {
            return new TestObjectWithSimpleTypes()
            {
                PropBool = true,
                PropEnum = TestEnum.Value2,
                PropInt = 666,
                PropDbl = 1.234,
                PropString = "someText"
            };
        }

        private string MakeTestJson()
        {
            return "{\"PropBool\":true,\"PropEnum\":1,\"PropInt\":666,\"PropDbl\":1.234,\"PropString\":\"someText\"}";
        }

        private string MakeInvalidTestJson()
        {
            return "{PropBool: 'x', PropEnum: 1, PropInt: 666, PropDbl: 1.234, PropString: 'someText'}";
        }

    }
}
