namespace introApiWeb.RabbitMQ
{
    public interface IRabitMQProducer
    {
        public void SendProductMessage<T>(T message, string classeMensagem);
    }
}
