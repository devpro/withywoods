using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Withywoods.AspNetCoreApiSample.IntegrationTests.Resources
{
    public class AspNetSiteServerFixture : IDisposable
    {
        public Uri RootUri => _rootUriInitializer.Value;

        public IWebHost Host { get; set; }

        private readonly Lazy<Uri> _rootUriInitializer;

        public AspNetSiteServerFixture()
        {
            _rootUriInitializer = new Lazy<Uri>(() => new Uri(StartAndGetRootUri()));
        }

        private static string FindClosestDirectoryContaining( string filename, string startDirectory)
        {
            var dir = startDirectory;
            while (true)
            {
                if (File.Exists(Path.Combine(dir, filename)))
                {
                    return dir;
                }

                dir = Directory.GetParent(dir)?.FullName;
                if (string.IsNullOrEmpty(dir))
                {
                    throw new FileNotFoundException(
                        $"Could not locate a file called '{filename}' in " +
                        $"directory '{startDirectory}' or any parent directory.");
                }
            }
        }

        protected static void RunInBackgroundThread(Action action)
        {
            var isDone = new ManualResetEvent(false);

            new Thread(() =>
            {
                action();
                isDone.Set();
            }).Start();

            isDone.WaitOne();
        }

        protected string StartAndGetRootUri()
        {
            Host = CreateWebHost();
            RunInBackgroundThread(Host.Start);
            return Host.ServerFeatures
                .Get<IServerAddressesFeature>()
                .Addresses.Last();
        }

        public void Dispose()
        {
            Host?.StopAsync();
        }

        protected IWebHost CreateWebHost()
        {
            return WebHost.CreateDefaultBuilder(Array.Empty<string>())
                .UseStartup<Startup>()
                .UseContentRoot(@"C:\Users\bertrand\workspace\withywoods\samples\AspNetCoreApiSample")
                .UseEnvironment("Development")
                .Build();
        }
    }
}
