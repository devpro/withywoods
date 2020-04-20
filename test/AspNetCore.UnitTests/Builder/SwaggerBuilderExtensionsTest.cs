using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Withywoods.AspNetCore.Builder;
using Withywoods.AspNetCore.DependencyInjection;
using Withywoods.AspNetCore.UnitTests.Fakes;
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
            var configuration = new FakeConfiguration();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddControllers();
            serviceCollection.AddSwaggerGen(configuration);
            var services = serviceCollection.BuildServiceProvider();
            var app = new ApplicationBuilder(services);

            // Act & Assert
            app.UseSwagger(configuration);
        }
    }
}
