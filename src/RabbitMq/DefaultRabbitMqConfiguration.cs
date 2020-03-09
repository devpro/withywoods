namespace Withywoods.RabbitMq
{
    public class DefaultRabbitMqConfiguration : IRabbitMqConfiguration
    {
        public string Hostname { get; set; }

        public int Port { get; set; }
    }
}
