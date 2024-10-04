using System.Text;
using Common.Library.Models;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Common.Library.Services;

public class RabbitMQService : IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly RabbitMQSettings _rabbitMQSettings;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQService(IConfiguration configuration)
    {        
        _configuration = configuration;
        _rabbitMQSettings = _configuration.GetSection(RabbitMQSettings.SectionName).Get<RabbitMQSettings>() ?? throw new NullReferenceException();

        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMQSettings.HostName,
            Port = _rabbitMQSettings.Port
        };
        
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void SendMessage(string queueName, string message)
    {
        // Declare a queue
        _channel.QueueDeclare(queue: queueName,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        // Publish message
        _channel.BasicPublish(exchange: "",
                              routingKey: queueName,
                              basicProperties: null,
                              body: body);
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}
