using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Withywoods.Hubspot.Abstractions.Providers;
using Withywoods.Hubspot.Client.Repositories;
using Withywoods.Net.Http.UnitTests;

namespace Withywoods.Hubspot.Client.UnitTests.Repositories;

public abstract class RepositoryTestBase<TRepository>(
    Func<IHubspotClientConfiguration, ILogger<TRepository>, IHttpClientFactory, ITokenProvider?, TRepository> repositoryFactory)
    : HttpRepositoryTestBase
    where TRepository : RepositoryBase
{
    protected DefaultHubspotClientConfiguration Configuration { get; } = new()
    {
        BaseUrl = "https://still.dont.exist",
        HttpClientName = "AlsoFake",
        ApiKey = "AlsoFake",
        ApplicationId = "AlsoFake",
        ClientId = "AlsoFake",
        ClientSecret = "AlsoFake",
        RedirectUrl = "AlsoFake"
    };

    protected Mock<ITokenProvider> TokenProviderMock { get; } = new();

    protected TRepository BuildRepository(HttpResponseMessage httpResponseMessage, HttpMethod httpMethod, string absoluteUri)
    {
        var logger = ServiceProvider.GetRequiredService<ILogger<TRepository>>();
        var httpClientFactoryMock = BuildHttpClientFactory(httpResponseMessage, httpMethod, Configuration.HttpClientName, absoluteUri);

        return repositoryFactory(Configuration, logger, httpClientFactoryMock.Object, TokenProviderMock.Object);
    }
}
