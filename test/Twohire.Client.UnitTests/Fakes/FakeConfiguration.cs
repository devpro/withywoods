using System.Diagnostics.CodeAnalysis;

namespace Withywoods.Twohire.Client.UnitTests.Fakes;

public class FakeConfiguration : DefaultTwohireClientConfiguration
{
    [SetsRequiredMembers]
    public FakeConfiguration()
    {
        BaseUrl = "http://does.not.exist";
        Version = "v42";
        ServiceToken = "Greetings";
        Username = "Hello";
        Password = "There";
        HttpClientName = "Fake";
    }
}
