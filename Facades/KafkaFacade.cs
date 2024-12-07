using Kafka_WEBAPI.Models;
using Kafka_WEBAPI.Services;

namespace Kafka_WEBAPI.Models
{
    public class KafkaFacade : IKafkaFacade
    {
        private readonly IKafkaProducerService _producerService;
        private readonly IKafkaConsumerService _consumerService;

        public KafkaFacade(IKafkaProducerService producerService, IKafkaConsumerService consumerService)
        {
            _producerService = producerService;
            _consumerService = consumerService;
        }

        public async Task ProduceMessageAsync(string message)
        {
            await _producerService.ProduceAsync(message);
        }

        public async Task StartConsumingAsync(CancellationToken cancellationToken)
        {
            await _consumerService.ConsumeAsync(cancellationToken);
        }
    }
}
