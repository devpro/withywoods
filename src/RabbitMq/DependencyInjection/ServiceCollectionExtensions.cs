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
            services.TryAddTransient<IConnectionFactory>(ctx => BuildConnectionFactory(configuration));
            services.TryAddScoped<IChannelFactory, ChannelFactory>();

            return services;
        }

        private static ConnectionFactory BuildConnectionFactory(IRabbitMqConfiguration configuration)
        {
            var factory = new ConnectionFactory();

            if (configuration.Uri != null)
            {
                factory.Uri = configuration.Uri;
            }
            else
            {
                factory.HostName = configuration.Hostname;
                factory.Port = configuration.Port ?? 5672;
            }

            if (configuration.ContinuationTimeout.HasValue)
            {
                factory.ContinuationTimeout = configuration.ContinuationTimeout.Value;
            }

            if (configuration.RequestedHeartbeat.HasValue)
            {
                factory.RequestedHeartbeat = configuration.RequestedHeartbeat.Value;
            }

            return factory;
        }
    }
}
