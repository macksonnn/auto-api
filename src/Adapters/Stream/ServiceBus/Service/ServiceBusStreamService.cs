using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Becape.Stream.ServiceBus.Service
{
    public class ServiceBusStreamService : IStream
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ILogger _logger;

        public ServiceBusStreamService(ILogger logger, ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
            _logger = logger;
        }

        public Task SendEventAsync<T>(T notification, string queueName)
        {
            return SendEventAsync(notification, queueName, null);
        }

        public Task SendEventAsync<T>(T notification, string queueName, DateTime? scheduleMessage)
        {
            ServiceBusSender serviceBusSender = _serviceBusClient.CreateSender(queueName);

            var serviceBusMessage = new ServiceBusMessage(JsonSerializer.Serialize(notification));

            if (scheduleMessage.HasValue)
                serviceBusMessage.ScheduledEnqueueTime = scheduleMessage.Value.ToUniversalTime();

            //TODO: Should be used if this new message is associated or an answer to another message
            //serviceBusMessage.CorrelationId = ;
            serviceBusMessage.ApplicationProperties.Add("TraceId", System.Diagnostics.Activity.Current?.Baggage?.FirstOrDefault(x => x.Key == "TraceId").Value ?? "");

            return serviceBusSender.SendMessageAsync(serviceBusMessage);
        }
    }
}
