using System;
using RabbitMQ.Client;

namespace Withywoods.RabbitMq
{
    public class ChannelFactory : IChannelFactory
    {
        private readonly IConnection _connection;

        public ChannelFactory(IConnectionFactory connectionFactory)
        {
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
