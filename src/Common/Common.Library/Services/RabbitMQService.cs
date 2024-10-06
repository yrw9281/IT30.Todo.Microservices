using System.Text;
using System.Text.Json;
using Common.Library.Models;
using Common.Library.Seedwork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Common.Library.Services;

public class RabbitMQService : IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    private readonly RabbitMQSettings _rabbitMQSettings;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQService(IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
        _rabbitMQSettings = _configuration.GetSection(RabbitMQSettings.SectionName).Get<RabbitMQSettings>() ?? throw new NullReferenceException();

        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMQSettings.HostName,
            Port = _rabbitMQSettings.Port
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void SendMessage<TIntegrationEvent>(TIntegrationEvent integrationEvent, string? queueName = null) where TIntegrationEvent : IIntegrationEvent
    {
        // Initialize queueName based on the event type if not provided
        queueName ??= typeof(TIntegrationEvent).Name;

        // Declare a queue
        _channel.QueueDeclare(queue: queueName,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        // Serialize 
        var message = JsonSerializer.Serialize(integrationEvent);
        var body = Encoding.UTF8.GetBytes(message);

        // Publish message
        _channel.BasicPublish(exchange: "",
                              routingKey: queueName,
                              basicProperties: null,
                              body: body);
    }

    public void StartListening<TIntegrationEvent>(Func<IServiceProvider, IIntegrationEventHandler<TIntegrationEvent>> eventHandlerDelegate, string? queueName = null)
        where TIntegrationEvent : IIntegrationEvent
    {
        // Initialize queueName based on the event type if not provided
        queueName ??= typeof(TIntegrationEvent).Name;

        _channel.QueueDeclare(queue: queueName,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // Deserialize 
            var integrationEvent = JsonSerializer.Deserialize<TIntegrationEvent>(message);

            if (integrationEvent != null)
            {
                // Use IServiceScopeFactory to create a new scope
                using (var scope = _serviceProvider.CreateScope())
                {
                    // Get the handler from the scoped service provider
                    var eventHandler = eventHandlerDelegate(scope.ServiceProvider);
                    await eventHandler.Handle(integrationEvent);
                }
            }
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}
