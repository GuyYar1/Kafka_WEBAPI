namespace Kafka_WEBAPI.Models
{
    public class KafkaSettings : IKafkaSettings
    {
        // Properties for Kafka settings with initial values to avoid null reference issues
        public string BootstrapServers { get; set; } = string.Empty;
        public string ProducerTopic { get; set; } = string.Empty;
        public string ConsumerTopic { get; set; } = string.Empty;
        public List<string> Keywords { get; set; } = new List<string>();

        // Optional: Constructor for further initialization if needed
        public KafkaSettings()
        {
            // Keywords is already initialized, no need to do it again here
        }
    }
}
