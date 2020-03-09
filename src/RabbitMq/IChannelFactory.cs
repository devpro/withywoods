using System;
using RabbitMQ.Client;

namespace Withywoods.RabbitMq
{
    public interface IChannelFactory : IDisposable
    {
        IModel Create();
    }
}
