using GeekShooping.MessageBus;

namespace GeekShooping.CartApi.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        // adicionar referencia para o projeto baseMessage
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
