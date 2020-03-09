using System;
using FluentAssertions;
using Xunit;

namespace Withywoods.System.UnitTests
{
    [Trait("Category", "UnitTests")]
    public class StringExtensionsTest
    {
        [Fact]
        public void StringExtensionGetSection_WhenSectionExists_ReturnData()
        {
            Assert.Throws<ArgumentNullException>(() => ((string)null).FirstCharToUpper()).Message.Should().Be("Value cannot be null. (Parameter 'input')");
            Assert.Throws<ArgumentException>(() => string.Empty.FirstCharToUpper()).Message.Should().Be("input cannot be empty (Parameter 'input')");
            "my first correct string".FirstCharToUpper().Should().Be("My first correct string");
            "MySecondString".FirstCharToUpper().Should().Be("MySecondString");
        }
    }
}
