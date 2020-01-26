using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Withywoods.AspNetCore.UnitTests.Fakes
{
    public class FakeConfiguration : IWebAppConfiguration
    {
        public OpenApiInfo SwaggerDefinition => new OpenApiInfo { Version = "42.0", Title = "Fake API", Contact = new OpenApiContact { Name = "John Doe" }, Description = "Fake fake fake" };

        public string AssemblyName => System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
    }
}
