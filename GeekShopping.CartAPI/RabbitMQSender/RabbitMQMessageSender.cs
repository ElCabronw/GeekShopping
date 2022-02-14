using System;
using System.Text;
using System.Text.Json;
using GeekShopping.CartAPI.Messages;
using GeekShopping.MessageBus;
using RabbitMQ.Client;

namespace GeekShopping.CartAPI.RabbitMQSender
{
	public class RabbitMQMessageSender : IRabbitMQMessageSender
	{
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }

        public void SendMessage(BaseMessage message, string queueName)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };

                _connection = factory.CreateConnection();

                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: queueName, false, false, arguments: null);
                byte[] body = GetMessageAsByteArray(message);
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize<CheckoutHeaderVO>((CheckoutHeaderVO)message,options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
    }
}

