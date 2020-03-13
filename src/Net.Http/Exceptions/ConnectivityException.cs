using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Withywoods.Net.Http.Exceptions
{
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

        [ExcludeFromCodeCoverage]
        protected ConnectivityException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
