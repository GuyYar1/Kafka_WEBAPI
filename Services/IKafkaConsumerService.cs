namespace Kafka_WEBAPI.Services
{
    public interface IKafkaConsumerService
    {
        Task ConsumeAsync(CancellationToken cancellationToken);
    }
}
