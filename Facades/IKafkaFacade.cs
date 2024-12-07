namespace Kafka_WEBAPI.Models
{
    public interface IKafkaFacade
    {
        Task ProduceMessageAsync(string message);
        Task StartConsumingAsync(CancellationToken cancellationToken);
    }
}
