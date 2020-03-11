using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Withywoods.UnitTesting.Fakes;

namespace Withywoods.UnitTesting
{
    /// <summary>
    /// Abstract class for test classes of HTTP repositories.
    /// This is needed in order to mock HttpClientFactory.
    /// </summary>
    public abstract class HttpRepositoryTestBase
    {
        protected HttpRepositoryTestBase()
        {
            var services = new ServiceCollection()
                .AddLogging();
            ServiceProvider = services.BuildServiceProvider();
            HttpMessageHandlerMock = new Mock<FakeHttpMessageHandler> { CallBase = true };
        }

        protected ServiceProvider ServiceProvider { get; private set; }

        protected Mock<FakeHttpMessageHandler> HttpMessageHandlerMock { get; private set; }

        /// <summary>
        /// Use this method to build a client factory.
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <param name="httpMethod"></param>
        /// <param name="httpClientName"></param>
        /// <param name="absoluteUri"></param>
        /// <returns></returns>
        protected virtual Mock<IHttpClientFactory> BuildHttpClientFactory(
            HttpResponseMessage httpResponseMessage,
            HttpMethod httpMethod,
            string httpClientName,
            string absoluteUri)
        {
            HttpMessageHandlerMock.Setup(f => f.Send(It.Is<HttpRequestMessage>(m =>
                    m.Method == httpMethod
                    && m.RequestUri.AbsoluteUri == absoluteUri)))
                .Returns(httpResponseMessage);

            var httpClient = new HttpClient(HttpMessageHandlerMock.Object);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(x => x.CreateClient(httpClientName))
                .Returns(httpClient);

            return httpClientFactoryMock;
        }
    }
}
