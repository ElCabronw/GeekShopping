using System;
using System.Text;
using System.Text.Json;
using GeekShopping.OrderAPI.Messages;
using GeekShopping.OrderAPI.Model;
using GeekShopping.OrderAPI.RabbitMQSender;
using GeekShopping.OrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GeekShopping.OrderAPI.MessageConsumer
{
	public class RabbitMQPaymentConsumer : BackgroundService
	{
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQPaymentConsumer(OrderRepository repository,
            IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _repository = repository;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orderpaymentresultqueue", false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                UpdatePaymentResultVO vo = JsonSerializer.Deserialize<UpdatePaymentResultVO>(content);
                UpdatePaymentStatus(vo).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("orderpaymentresultqueue", false,consumer);
            return Task.CompletedTask;
        }

        private async Task UpdatePaymentStatus(UpdatePaymentResultVO vo)
        {
            try
            {
                _repository.UpdateOrderPaymentStatus(vo.OrderId, vo.Status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

