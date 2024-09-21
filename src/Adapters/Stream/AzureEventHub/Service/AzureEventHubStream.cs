using Microsoft.Extensions.Logging;
using Becape.Stream.AzureEventHub.Startup;
using System.Text;
using System.Text.Json;
using Becape.Core.Common.Stream;

namespace Becape.Stream.ServiceBus.Service
{
    public class AzureEventHubStream : IStream
    {
        private readonly EventHubProducerClient _producerClient;
        private readonly ILogger<AzureEventHubStream> _logger;

        public AzureEventHubStream(ILogger<AzureEventHubStream> logger, AzureEventHubSettings settings)
        {
            _logger = logger;
            _producerClient = new EventHubProducerClient(settings.ConnectionString, settings.EventHubName);
        }

        public Task SendEventAsync<T>(T notification, string queueName)
        {
            return SendEventAsync<T>(notification, queueName);
        }

        public async Task SendEventAsync<T>(T notification, string queueName, DateTime? scheduleMessage)
        {
            try
            {
                using EventDataBatch eventBatch = await _producerClient.CreateBatchAsync();

                if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(notification)))))
                    throw new Exception($"The message is too large to fit in the batch.");

                await _producerClient.SendAsync(eventBatch);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message to Event Hub: {Failure}", ex.Message);
            }
        }
    }
}
