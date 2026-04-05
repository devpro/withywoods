namespace Devpro.Yanport.Client
{
    public class DefaultYanportClientConfiguration : IYanportClientConfiguration
    {
        public string BaseUrl { get; set; }

        public string Token { get; set; }

        public string HttpClientName { get; set; } = "Yanport";
    }
}
