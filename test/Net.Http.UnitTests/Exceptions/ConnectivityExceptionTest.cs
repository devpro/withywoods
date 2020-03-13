using System;
using FluentAssertions;
using Withywoods.Net.Http.Exceptions;
using Xunit;

namespace Withywoods.Net.Http.UnitTests.Exceptions
{
    [Trait("Category", "UnitTests")]
    public class ConnectivityExceptionTest
    {
        [Fact]
        public void NetHttpConnectivityException_ImplementException()
        {
            var exceptionWithoutMessage = new ConnectivityException();
            exceptionWithoutMessage.Message.Should().Be("Exception of type 'Withywoods.Net.Http.Exceptions.ConnectivityException' was thrown.");
            exceptionWithoutMessage.InnerException.Should().BeNull();

            var exceptionWithMessage = new ConnectivityException("My first message");
            exceptionWithMessage.Message.Should().Be("My first message");
            exceptionWithMessage.InnerException.Should().BeNull();

            var exceptionWithInnerException = new ConnectivityException("My first message", new Exception("Inner exception"));
            exceptionWithInnerException.Message.Should().Be("My first message");
            exceptionWithInnerException.InnerException.Should().NotBeNull();
            exceptionWithInnerException.InnerException.Message.Should().Be("Inner exception");
        }
    }
}
