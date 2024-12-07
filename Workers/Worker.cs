using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Kafka_WEBAPI.Models;

namespace Kafka_WEBAPI.Models
{
    public class Worker : BackgroundService
    {
        private readonly IKafkaFacade _kafkaFacade;

        public Worker(IKafkaFacade kafkaFacade)
        {
            _kafkaFacade = kafkaFacade;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _kafkaFacade.StartConsumingAsync(stoppingToken);
        }
    }
}
