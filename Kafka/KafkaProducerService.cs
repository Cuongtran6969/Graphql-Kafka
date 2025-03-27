
using Confluent.Kafka;
using System.Text.Json;

namespace GraphQLPractive.Kafka
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic = "api-responses";
        public KafkaProducerService()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092" // Thay bằng địa chỉ Kafka của bạn
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task SendMessageAsync<T>(T message)
        {
            try
            {
                string jsonMessage = JsonSerializer.Serialize(message);
                await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = jsonMessage });
                Console.WriteLine($"Sent message to Kafka: {jsonMessage}");
            } catch (Exception ex)
            {
                Console.WriteLine($"Error sedning message to kafka : {ex.Message}");
            }
        }
    }
}
