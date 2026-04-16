using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using LogisticApp.Models;
namespace LogisticApp.Services;

public class RabbitMqConsumer : BackgroundService
{
    private readonly ILogger<RabbitMqConsumer> _logger;
    private readonly MqMessageStore _store;

    public RabbitMqConsumer(ILogger<RabbitMqConsumer> logger, MqMessageStore store)
    {
        _logger = logger;
        _store = store;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare("logistic_mq_messages", false, false, false, null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var json = JsonDocument.Parse(message).RootElement;

            var item = new Item
            {
                Id = int.Parse(json.GetProperty("AssignmentIdField").GetString() ?? "0"),
                AssignmentIdField = json.GetProperty("AssignmentIdField").GetString(),
                Package = json.GetProperty("Package").GetString(),
                PickupLocationField = json.GetProperty("PickupLocationField").GetString(),
                DeliveryLocationField = json.GetProperty("DeliveryLocationField").GetString(),
                PriorityField = json.GetProperty("PriorityField").GetString(),
                SentAt = json.TryGetProperty("SentAt", out var sent)
                    ? sent.GetDateTime()
                    : null,
                ReceivedAt = DateTime.UtcNow
            };

            _store.Items.Add(item);

            _logger.LogInformation($"[MQ RECEIVED] {item.Id}");
        };

        channel.BasicConsume("logistic_mq_messages", true, consumer);

        return Task.CompletedTask;
    }
}