using Confluent.Kafka;
using Kafka_WEBAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kafka_WEBAPI.Services
{
    public class KafkaProducerService : IKafkaProducerService, IDisposable
    {
        private readonly KafkaSettings _settings;
        private readonly IProducer<Null, string> _producer;
        private readonly ILogger<KafkaProducerService> _logger;

        public KafkaProducerService(IOptions<KafkaSettings> settings, ILogger<KafkaProducerService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
            _producer = CreateProducer(_settings);
        }

        private IProducer<Null, string> CreateProducer(KafkaSettings settings)
        {
            ProducerConfig producerConfig = new ProducerConfig
            {
                BootstrapServers = settings.BootstrapServers,
                // Add more producer configuration options if needed
                // For example:
                // Acks = Acks.All,
                // Retries = 3
            };

            return new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        public async Task ProduceAsync(string message)
        {
            try
            {
                DeliveryResult<Null, string> result = await _producer.ProduceAsync(_settings.ProducerTopic,
                    new Message<Null, string> { Value = message });

                _logger.LogInformation($"Message sent to {result.TopicPartitionOffset}");
            }
            catch (ProduceException<Null, string> e)
            {
                _logger.LogError($"Error producing message: {e.Error.Reason}");
            }
        }

        public void Dispose()
        {
            _producer?.Dispose();
        }
    }
}
