using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Withywoods.RabbitMq;
using Withywoods.RabbitMq.DependencyInjection;

namespace Withywoods.RabbitMqConsumerSample
{
    internal static class Program
    {
        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            var hostname = args.Length > 0 ? args[0] : "localhost";
            var port = args.Length > 1 ? int.Parse(args[1]) : 5672;
            var configuration = new DefaultRabbitMqConfiguration { Hostname = hostname, Port = port };

            var services = new ServiceCollection()
                .AddLogging()
                .AddRabbitMqFactory(configuration);
            using var serviceProvider = services.BuildServiceProvider();

            var channelFactory = serviceProvider.GetRequiredService<IChannelFactory>();

            using var channel = channelFactory.Create();

            channel.QueueDeclare(queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.Span;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Received {message}");
            };
            channel.BasicConsume(queue: "hello",
                autoAck: true,
                consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
