namespace Withywoods.RabbitMq
{
    /// <summary>
    /// RabbitMQ configuration.
    /// </summary>
    public interface IRabbitMqConfiguration
    {
        /// <summary>
        /// Hostname.
        /// </summary>
        string Hostname { get; }

        /// <summary>
        /// Port.
        /// </summary>
        int Port { get; }
    }
}
