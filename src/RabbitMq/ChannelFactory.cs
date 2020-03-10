using System;
using RabbitMQ.Client;

namespace Withywoods.RabbitMq
{
    public class ChannelFactory : IChannelFactory
    {
        private readonly IConnection _connection;

        public ChannelFactory(IRabbitMqConfiguration configuration)
        {
            var connectionFactory = new ConnectionFactory { HostName = configuration.Hostname, Port = configuration.Port };
            _connection = connectionFactory.CreateConnection();
        }

        public IModel Create()
        {
            return _connection.CreateModel();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
