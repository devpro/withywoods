using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQ.Client;

namespace Withywoods.RabbitMq.DependencyInjection
{
    /// <summary>
    /// Service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add required services to use RabbitMQ factories.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMqFactory<T>(this IServiceCollection services, T configuration)
            where T : class, IRabbitMqConfiguration
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.TryAddSingleton<IRabbitMqConfiguration>(configuration);
            services.TryAddScoped<IChannelFactory, ChannelFactory>();
            services.TryAddTransient<IConnectionFactory>(ctx => new ConnectionFactory { HostName = configuration.Hostname, Port = configuration.Port });

            return services;
        }
    }
}
