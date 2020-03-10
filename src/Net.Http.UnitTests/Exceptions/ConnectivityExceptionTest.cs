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

            var exceptionWithMessage = new ConnectivityException("My first message");
            exceptionWithMessage.Message.Should().Be("My first message");

            // TODO: inner exception, serializationInfo & streamingContext
        }
    }
}
