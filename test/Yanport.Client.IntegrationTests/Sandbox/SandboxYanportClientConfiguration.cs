using System;

namespace Devpro.Yanport.Client.IntegrationTests.Sandbox
{
    public class SandboxYanportClientConfiguration : IYanportClientConfiguration
    {
        public string BaseUrl => Environment.GetEnvironmentVariable("Yanport__Sandbox__BaseUrl");

        public string Token => Environment.GetEnvironmentVariable("Yanport__Sandbox__Token");

        public string HttpClientName => "Yanport";
    }
}
