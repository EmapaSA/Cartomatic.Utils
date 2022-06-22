using System;
using FluentAssertions;
using NUnit.Framework;

namespace Cartomatic.Utils.Ef.Tests
{
    [TestFixture]
    public class StringExtensions_PropertyNameToColumnNameTests
    {
        [TestCase("Word1Word2", "word1_word2")]
        [TestCase("WordOneWordTwo", "word_one_word_two")]
        [TestCase("WWWordOOOne", "wwword_ooone")]
        [TestCase("SomeId", "some_id")]
        [TestCase("SomeX", "some_x")]
        [TestCase("Some", "some")]
        [TestCase("XXX", "xxx")]
        [TestCase("XXXxYYY", "xxxx_yyy")]
        public void InvalidPath_WhenCheckedIfAbsolute_ShouldThrow(string input, string expected)
        {
            var output = input.ToColumnName();
            output.Should().Be(expected);
        }
    }
}