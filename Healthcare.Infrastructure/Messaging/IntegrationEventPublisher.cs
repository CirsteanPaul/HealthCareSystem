using System.Text;
using Healthcare.Application.Core.Abstractions.Messaging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Healthcare.Infrastructure.Messaging;

public sealed class IntegrationEventPublisher : IIntegrationEventPublisher, IDisposable
{
    private readonly MessageBrokerSettings _messageBrokerSettings;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public IntegrationEventPublisher(IOptions<MessageBrokerSettings> messageBrokerSettings)
    {
        _messageBrokerSettings = messageBrokerSettings.Value;

        IConnectionFactory connectionFactory = new ConnectionFactory
        {
            HostName = _messageBrokerSettings.HostName,
            Port = _messageBrokerSettings.Port,
            UserName = _messageBrokerSettings.UserName,
            Password = _messageBrokerSettings.Password
        };

        _connection = connectionFactory.CreateConnection();

        _channel = _connection.CreateModel();

        _channel.QueueDeclare(_messageBrokerSettings.QueueName, false, false, false);
    }

    public void Publish(IIntegrationEvent integrationEvent)
    {
        string payload = JsonConvert.SerializeObject(integrationEvent, typeof(IIntegrationEvent),
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

        byte[] body = Encoding.UTF8.GetBytes(payload);
        
        _channel.BasicPublish(String.Empty, _messageBrokerSettings.QueueName, body: body);
    }

    public void Dispose()
    {
        _connection?.Dispose();

        _channel.Dispose();
    }
}