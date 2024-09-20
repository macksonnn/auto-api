//using Microsoft.Extensions.Logging;
//using AutoMais.Core.Application.Adapters.Stream;
//using RabbitMQ.Client;
//using System.Text.Json;
//using System.Threading.Channels;

//namespace AutoMais.Stream.Rabbit.Service
//{
//    public class RabbitMQStreamService : IStream
//    {
//        private readonly IConnection _rabbitMQConnection;
//        private readonly ILogger _logger;

//        public RabbitMQStreamService(ILogger<RabbitMQStreamService> logger, IConnection rabbitMQConnection)
//        {
//            _logger = logger;
//            _rabbitMQConnection = rabbitMQConnection;
//        }

//        public Task SendEventAsync<T>(T notification, string queueName)
//        {
//            return SendEventAsync(notification, queueName, null);
//        }

//        public Task SendEventAsync<T>(T notification, string queueName, DateTime? scheduleMessage)
//        {
//            var channel = _rabbitMQConnection.CreateModel();

//            channel.

//            var serviceBusMessage = new ServiceBusMessage(JsonSerializer.Serialize(notification));

//            if (scheduleMessage.HasValue)
//                serviceBusMessage.ScheduledEnqueueTime = scheduleMessage.Value.ToUniversalTime();

//            //TODO: Should be used if this new message is associated or an answer to another message
//            serviceBusMessage.CorrelationId = System.Diagnostics.Activity.Current?.Baggage?.FirstOrDefault(x => x.Key == "InvocationId").Value ?? "";

//            return serviceBusSender.SendMessageAsync(serviceBusMessage);
//        }
//    }
//}
