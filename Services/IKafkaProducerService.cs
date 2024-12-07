namespace Kafka_WEBAPI.Services
{
    public interface IKafkaProducerService
    {
        Task ProduceAsync(string message);
    }
}
