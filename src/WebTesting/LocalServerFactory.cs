using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Withywoods.WebTesting
{
    /// <summary>
    /// Local server factory to be able to test a web application from an external driver (such as the one provided by Selenium).
    /// </summary>
    /// <typeparam name="TStartup"></typeparam>
    public class LocalServerFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        private IWebHost _host;

        public LocalServerFactory()
        {
            ClientOptions.BaseAddress = new Uri("https://localhost");
        }

        public string RootUri { get; private set; }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            _host = builder.Build();
            _host.Start();
            RootUri = _host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.LastOrDefault();

            // not used but needed in the CreateServer method logic
            return new TestServer(new WebHostBuilder().UseStartup<TStartup>());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _host.Dispose();
            }
        }
    }
}
