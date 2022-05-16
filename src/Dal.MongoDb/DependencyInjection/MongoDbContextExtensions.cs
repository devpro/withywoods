using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Withywoods.Dal.MongoDb.DependencyInjection
{
    /// <summary>
    /// Extensions methods to configure an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class MongoDbContextExtensions
    {
        /// <summary>
        /// Add <see cref="MongoClientFactory"/> (transcient) and <see cref="DefaultMongoDbContext"/> (scoped) to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoDbContext<T>(this IServiceCollection services)
            where T : class, IMongoDbConfiguration
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddTransient<IMongoDbConfiguration, T>();
            services.TryAddTransient<IMongoClientFactory, MongoClientFactory>();
            services.TryAddScoped<IMongoDbContext, DefaultMongoDbContext>();

            return services;
        }
    }
}
