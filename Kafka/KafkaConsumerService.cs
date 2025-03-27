
using Confluent.Kafka;
using static GraphQL.Validation.Rules.OverlappingFieldsCanBeMerged;

namespace GraphQLPractive.Kafka
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly ILogger<KafkaConsumerService> _logger;

        public KafkaConsumerService(ILogger<KafkaConsumerService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Kafka Consumer Service is starting...");

            // Chạy vòng lặp nhận message trong Task.Run để tránh block startup
            //_ = Task.Run(async () =>
            //{
            //    while (!stoppingToken.IsCancellationRequested)
            //    {
            //        try
            //        {
            //            _logger.LogInformation("Waiting for Kafka messages...");
            //            await Task.Delay(1000, stoppingToken); // Giả lập việc nhận tin nhắn
            //        }
            //        catch (OperationCanceledException)
            //        {
            //            _logger.LogInformation("Kafka Consumer Service is stopping...");
            //            break;
            //        }
            //        catch (Exception ex)
            //        {
            //            _logger.LogError(ex, "Error in Kafka Consumer Service");
            //        }
            //    }
            //}, stoppingToken);

            await Task.CompletedTask;
        }
    }
}
