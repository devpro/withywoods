using System;

namespace Withywoods.RabbitMq
{
    public class DefaultRabbitMqConfiguration : IRabbitMqConfiguration
    {
        public Uri Uri { get; set; }

        public string Hostname { get; set; }

        public int? Port { get; set; }

        public TimeSpan? ContinuationTimeout { get; set; }

        public TimeSpan? RequestedHeartbeat { get; set; }
    }
}
