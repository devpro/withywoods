using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Withywoods.AspNetCore.DependencyInjection;
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
            var openApiInfo = new OpenApiInfo { Version = "42.0", Title = "Fake API", Contact = new OpenApiContact { Name = "John Doe" }, Description = "Fake fake fake" };

            // Act & Assert
            serviceCollection.AddSwaggerGen(openApiInfo);
            serviceCollection.BuildServiceProvider();
        }
    }
}
