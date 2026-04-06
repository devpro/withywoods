using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Withywoods.Hubspot.Client.Providers;
using Withywoods.Hubspot.Client.Repositories;

namespace Withywoods.Hubspot.Client.DependencyInjection;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add the dependency injection configuration.
    /// </summary>
    /// <typeparam name="T">Instance of <see cref="IHubspotClientConfiguration"/></typeparam>
    /// <param name="services">Collection of services that will be completed</param>
    /// <param name="configuration">Configuration</param>
    /// <returns></returns>
    public static IServiceCollection AddHubspotClient<T>(this IServiceCollection services, T configuration)
        where T : class, IHubspotClientConfiguration
    {
        services.TryAddSingleton<IHubspotClientConfiguration>(configuration);
        services.TryAddScoped<Abstractions.Providers.ITokenProvider, TokenProvider>();
        services.TryAddTransient<Abstractions.Repositories.IContactRepository, ContactRepository>();
        services.TryAddTransient<Abstractions.Repositories.ITokenRepository, TokenRepository>();

        services
            .AddHttpClient(configuration.HttpClientName, client =>
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

        return services;
    }
}
