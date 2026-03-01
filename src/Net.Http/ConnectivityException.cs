using System;

namespace Withywoods.Net.Http;

[Serializable]
public class ConnectivityException : Exception
{
    public ConnectivityException()
    {
    }

    public ConnectivityException(string message)
        : base(message)
    {
    }

    public ConnectivityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
