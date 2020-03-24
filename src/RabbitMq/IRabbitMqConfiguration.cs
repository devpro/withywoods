using System;

namespace Withywoods.RabbitMq
{
    /// <summary>
    /// RabbitMQ configuration.
    /// </summary>
    public interface IRabbitMqConfiguration
    {
        /// <summary>
        /// Uri.
        /// </summary>
        Uri Uri { get; }

        /// <summary>
        /// Continuation timeout.
        /// </summary>
        ushort? RequestedHeartbeat { get; }

        /// <summary>
        /// Hostname.
        /// Used if Uri is not defined.
        /// </summary>
        string Hostname { get; }

        /// <summary>
        /// Port.
        /// Used if Uri is not defined.
        /// </summary>
        int? Port { get; }

        /// <summary>
        /// Continuation timeout.
        /// </summary>
        TimeSpan ContinuationTimeout { get; }
    }
}
