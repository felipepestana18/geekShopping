using GeekShooping.CartApi.Messages;
using GeekShooping.MessageBus;
using Microsoft.AspNetCore.Components;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShooping.CartApi.RabbitMQSender
{
    public class RabbitMQCheckoutConsumer : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public RabbitMQCheckoutConsumer()
        {
            _hostName = "localhost";
            _userName = "guest";
            _password = "guest";
        }

        public void SendMessage(BaseMessage message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password
            };
            _connection = factory.CreateConnection();

            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
            byte[] body = GetMessageAsByteArray(message);
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            // transformando em bytes
            var options = new JsonSerializerOptions
            {
                // para serialize a herença
                // se não fazer desse jeito não consegue retorna os valores da herença
                WriteIndented = true
            };


            var json = JsonSerializer.Serialize<CheckoutHeaderVo>((CheckoutHeaderVo)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;


        }
    }
}
