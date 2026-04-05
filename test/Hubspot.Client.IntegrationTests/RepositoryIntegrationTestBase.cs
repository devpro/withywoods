using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Withywoods.Hubspot.Abstractions.Providers;
using Withywoods.Hubspot.Client.DependencyInjection;
using Withywoods.Hubspot.Client.Repositories;

namespace Withywoods.Hubspot.Client.IntegrationTests;

public abstract class RepositoryIntegrationTestBase<TConfiguration, TRepository>
    where TConfiguration : class, IHubspotClientConfiguration
    where TRepository : RepositoryBase
{
    private readonly TConfiguration _configuration;

    private readonly Func<IHubspotClientConfiguration, ILogger<TRepository>, IHttpClientFactory, ITokenProvider?, TRepository> _repositoryFactory;

    private readonly ServiceProvider _serviceProvider;

    protected RepositoryIntegrationTestBase(TConfiguration configuration,
        Func<IHubspotClientConfiguration, ILogger<TRepository>, IHttpClientFactory, ITokenProvider?, TRepository> repositoryFactory)
    {
        _configuration = configuration;
        _repositoryFactory = repositoryFactory;

        var services = new ServiceCollection()
            .AddLogging()
            .AddHubspotClient(_configuration);
        _serviceProvider = services.BuildServiceProvider();
    }

    protected TRepository BuildRepository()
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<TRepository>>();
        var httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
        var tokenProvider = _serviceProvider.GetService<ITokenProvider>();

        return _repositoryFactory(_configuration, logger, httpClientFactory, tokenProvider);
    }
}
