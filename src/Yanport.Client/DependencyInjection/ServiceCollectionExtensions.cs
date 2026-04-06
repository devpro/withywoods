using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Withywoods.Yanport.Abstractions.Repositories;
using Withywoods.Yanport.Client.Repositories;

namespace Withywoods.Yanport.Client.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add the dependency injection configuration.
    /// </summary>
    /// <typeparam name="T">Instance of <see cref="IYanportClientConfiguration"/></typeparam>
    /// <param name="services">Collection of services that will be completed</param>
    /// <param name="configuration">Configuration</param>
    /// <returns></returns>
    public static IServiceCollection AddYanportClient<T>(this IServiceCollection services, T configuration)
        where T : class, IYanportClientConfiguration
    {
        services.TryAddSingleton<IYanportClientConfiguration>(configuration);
        services.TryAddTransient<IPropertyRepository, PropertyRepository>();

        services
            .AddHttpClient(configuration.HttpClientName, client =>
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration.Token);
            });

        return services;
    }
}
