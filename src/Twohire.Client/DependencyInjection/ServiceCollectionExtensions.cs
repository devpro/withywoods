using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Withywoods.Twohire.Abstractions.Providers;
using Withywoods.Twohire.Abstractions.Repositories;
using Withywoods.Twohire.Client.Providers;
using Withywoods.Twohire.Client.Repositories;

namespace Withywoods.Twohire.Client.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add the dependency injection configuration.
    /// </summary>
    /// <typeparam name="T">Instance of <see cref="ITwohireClientConfiguration"/></typeparam>
    /// <param name="services">Collection of services that will be completed</param>
    /// <param name="configuration">Configuration</param>
    /// <returns></returns>
    public static IServiceCollection AddTwohireClient<T>(this IServiceCollection services, T configuration)
        where T : class, ITwohireClientConfiguration
    {
        services.TryAddSingleton<ITwohireClientConfiguration>(configuration);
        services.TryAddSingleton<ITokenProvider, TokenProvider>();
        services.TryAddTransient<IPersonalVehicleRepository, PersonalVehicleRepository>();
        services.TryAddTransient<ITokenRepository, TokenRepository>();

        services
            .AddHttpClient(configuration.HttpClientName, client =>
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

        return services;
    }
}
