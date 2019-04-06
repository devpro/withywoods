using Swashbuckle.AspNetCore.Swagger;

namespace Withywoods.AspNetCore.UnitTests.Fakes
{
    public class FakeConfiguration : IWebAppConfiguration
    {
        public Info SwaggerDefinition => new Info { Version = "42.0", Title = "Fake API", Contact = new Contact { Name = "John Doe" }, Description = "Fake fake fake" };

        public string AssemblyName => System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
    }
}
