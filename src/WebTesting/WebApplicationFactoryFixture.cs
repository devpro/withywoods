using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace Withywoods.WebTesting
{
    /// <summary>
    /// Web application factory fixture class to enable UI testing of ASP.NET applications.
    /// Works with .NET 6 and minimal API.
    /// </summary>
    /// <typeparam name="TEntryPoint">Program class (Startup class with .NET <= 5)</typeparam>
    /// <remarks>
    /// Solution described at https://steinbach.io/asp-net-core-e2e-tests-with-xunit-and-playwright/
    /// Found by https://www.benday.com/2021/07/19/asp-net-core-integration-tests-with-selenium-webapplicationfactory/
    /// </remarks>
    public class WebApplicationFactoryFixture<TEntryPoint> : WebApplicationFactory<TEntryPoint>
        where TEntryPoint : class
    {
        public string HostUrl { get; set; } = "https://localhost";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseUrls(HostUrl);
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var dummyHost = builder.Build();

            builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());

            var host = builder.Build();
            host.Start();

            return dummyHost;
        }
    }
}
