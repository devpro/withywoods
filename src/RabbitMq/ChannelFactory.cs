using RabbitMQ.Client;

namespace Withywoods.RabbitMq
{
    public class ChannelFactory : IChannelFactory
    {
        private readonly IRabbitMqConfiguration _configuration;

        private readonly IConnectionFactory _connectionFactory;

        private readonly IConnection _connection;

        public ChannelFactory(IRabbitMqConfiguration configuration)
        {
            _configuration = configuration;
            _connectionFactory = new ConnectionFactory { HostName = _configuration.Hostname, Port = _configuration.Port };
            _connection = _connectionFactory.CreateConnection();
        }

        public IModel Create()
        {
            return _connection.CreateModel();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection?.Dispose();
            }
        }
    }
}
