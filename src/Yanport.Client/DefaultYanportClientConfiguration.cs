namespace Withywoods.Yanport.Client;

public class DefaultYanportClientConfiguration : IYanportClientConfiguration
{
    public required string BaseUrl { get; set; }

    public required string Token { get; set; }

    public string HttpClientName { get; set; } = "Yanport";
}
