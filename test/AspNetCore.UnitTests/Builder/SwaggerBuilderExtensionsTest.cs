using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Withywoods.AspNetCore.Builder;
using Withywoods.AspNetCore.DependencyInjection;
using Xunit;

namespace Withywoods.AspNetCore.UnitTests.Builder
{
    [Trait("Category", "UnitTests")]
    public class SwaggerBuilderExtensionsTest
    {
        /// <summary>
        /// Verifies that UseSwagger does not throw an exception.
        /// </summary>
        /// <see cref="https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/src/Swashbuckle.AspNetCore.SwaggerUI/SwaggerUIBuilderExtensions.cs"/>
        [Fact]
        public void AspNetCoreUseSwagger_DoesNotThrowException()
        {
            // Arrange
            var openApiInfo = new OpenApiInfo { Version = "42.0", Title = "Fake API", Contact = new OpenApiContact { Name = "John Doe" }, Description = "Fake fake fake" };
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddControllers();
            serviceCollection.AddSwaggerGen(openApiInfo);
            var services = serviceCollection.BuildServiceProvider();
            var app = new ApplicationBuilder(services);

            // Act & Assert
            app.UseSwagger(openApiInfo);
        }
    }
}
