namespace GraphQLPractive.Kafka
{
    public interface IKafkaProducerService
    {
        Task SendMessageAsync<T>(T message);
    }
}
