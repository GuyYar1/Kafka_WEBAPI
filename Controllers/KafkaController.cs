using Microsoft.AspNetCore.Mvc;
using Kafka_WEBAPI.Models;

namespace Kafka_WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly IKafkaFacade _kafkaFacade;

        public KafkaController(IKafkaFacade kafkaFacade)
        {
            _kafkaFacade = kafkaFacade;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] KafkaMessageModel model)
        {
            if (string.IsNullOrEmpty(model?.Message))
            {
                return BadRequest("Message cannot be null or empty.");
            }

            await _kafkaFacade.ProduceMessageAsync(model.Message);
            return Ok("Message sent!");
        }
    }
}
