using Confluent.Kafka;
using Kafka_WEBAPI.Models;
using Microsoft.Extensions.Logging;

namespace Kafka_WEBAPI.Services
{
    public class KafkaConsumerService : IKafkaConsumerService, IDisposable
    {
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly IConsumer<Ignore, string> _consumer;

        public KafkaConsumerService(IKafkaSettings kafkaSettings, ILogger<KafkaConsumerService> logger)
        {
            _logger = logger;
            _consumer = CreateConsumer(kafkaSettings);
        }

        private IConsumer<Ignore, string> CreateConsumer(IKafkaSettings kafkaSettings)
        {
            ConsumerConfig config = new ConsumerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers,
                GroupId = "test-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            return new ConsumerBuilder<Ignore, string>(config).Build();
        }

        public async Task ConsumeAsync(CancellationToken cancellationToken)
        {
            try
            {
                _consumer.Subscribe("my-consumer-topic");

                while (!cancellationToken.IsCancellationRequested)
                {
                    ConsumeResult<Ignore, string> consumeResult = _consumer.Consume(cancellationToken);
                    _logger.LogInformation($"Consumed message: {consumeResult.Message.Value}");
                    await Task.Yield();
                }
            }
            catch (ConsumeException e)
            {
                _logger.LogError($"Error while consuming message: {e.Error.Reason}");
            }
        }

        public void Dispose()
        {
            _consumer?.Dispose();
        }
    }
}
