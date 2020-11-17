using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Withywoods.AspNetCore.DependencyInjection;
using Withywoods.AspNetCore.UnitTests.Fakes;
using Xunit;

namespace Withywoods.AspNetCore.UnitTests.DependencyInjection
{
    [Trait("Category", "UnitTests")]
    public class SwaggerGenServiceCollectionExtensionsTest
    {
        /// <summary>
        /// Verifies that AddSwaggerGen does not throw an exception.
        /// </summary>
        /// <see cref="https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/src/Swashbuckle.AspNetCore.SwaggerGen/Application/SwaggerGenServiceCollectionExtensions.cs"/>
        [Fact]
        public void AspNetCoreAddSwaggerGen_DoesNotThrowException()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = new FakeConfiguration();

            // Act & Assert
            serviceCollection.AddSwaggerGen(configuration);
            serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Verifies that AddSwaggerGenWithBasicAuthSecurity does not throw an exception.
        /// </summary>
        /// <see cref="https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/src/Swashbuckle.AspNetCore.SwaggerGen/Application/SwaggerGenServiceCollectionExtensions.cs"/>
        [Fact]
        public void AspNetCoreAddSwaggerGenAndConfigureOptions_DoesNotThrowException()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = new FakeConfiguration();

            // Act & Assert
            serviceCollection.AddSwaggerGen(configuration, c =>
            {
                c.AddSecurityDefinition("BasicAuthentication", new OpenApiSecurityScheme() { Scheme = "Basic", In = ParameterLocation.Header, Name = "Authorization", Type = SecuritySchemeType.Http });
            });
            serviceCollection.BuildServiceProvider();
        }
    }
}
