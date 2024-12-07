namespace Kafka_WEBAPI.Models
{
    public interface IKafkaSettings
    {
        string BootstrapServers { get; }
        string ProducerTopic { get; }
        string ConsumerTopic { get; }
        List<string> Keywords { get; }
    }

}
