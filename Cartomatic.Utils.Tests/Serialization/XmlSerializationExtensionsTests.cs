using System;
using System.Xml.Serialization;
using AwesomeAssertions;
using NUnit.Framework;

namespace Cartomatic.Utils.Serialization.Tests
{
    [TestFixture]
    public class XmlSerializationExtensionsTests
    {
        [Test]
        public void DeserializeFromXml_WhenPassedArbitraryObject_DeserializesItProperly()
        {
            string xml = MakeTestXml();
            var refObj = MakeTestObject();


            var obj = xml.DeserializeFromXml<TestObjectWithSimpleTypes>();

            obj.Should().NotBeNull();
            obj.Should().BeEquivalentTo(refObj);
        }

        [Test]
        public void DeserializeFromXml_WhenPassedInvalidJson_ShouldNotThrowButReturnNullByDefault()
        {
            var invalidXml = MakeInvalidTestXml();
            var obj = invalidXml.DeserializeFromXml<TestObjectWithSimpleTypes>();

            obj.Should().BeNull();
        }

        [Test]
        public void DeserializeFromXml_WhenPassedInvalidJsonAndSilentSetToTrue_ShouldThrow()
        {
            var json = MakeInvalidTestXml();
            Action act = () => json.DeserializeFromXml<TestObjectWithSimpleTypes>(false);

            act.Should().Throw<Exception>();
        }

        [Test]
        public void SerializeToXml_WhenCalledOnArbitraryObject_SerializesItProperly()
        {
            var obj = MakeTestObject();
            var refXml = MakeTestXml();

            string xml = obj.SerializeToXml();

            bool test = refXml == xml;

            xml.Should().Be(refXml);
        }

        [Test]
        public void SerializeToXml_WhenPassedObjectWIthNamespaces_AddsNamespacesToOutputXml()
        {
            var obj = MakeTestObject();
            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add("mynamespace","http://whatever");

            var xml = obj.SerializeToXml(xmlns);

            xml.Should().Contain("xmlns:mynamespace=\"http://whatever\"");
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

        private string MakeTestXml()
        {
            return 
@"<?xml version=""1.0"" encoding=""utf-8""?>
<TestObjectWithSimpleTypes xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <PropBool>true</PropBool>
  <PropEnum>Value2</PropEnum>
  <PropInt>666</PropInt>
  <PropDbl>1.234</PropDbl>
  <PropString>someText</PropString>
</TestObjectWithSimpleTypes>";
        }

        private string MakeInvalidTestXml()
        {
            return
@"<?xml version=""1.0""?>
<TestObjectWithSimpleTypes xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <PropBool>x</PropBool>
  <PropEnum>Value2</PropEnum>
  <PropInt>666</PropInt>
  <PropDbl>1.234</PropDbl>
  <PropString>someText</PropString>
</TestObjectWithSimpleTypes>";
        }
    }
}
