using System.Text;
using BackgroundJobs.Services;
using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BackgroundJobs.Tasks;

public sealed class IntegrationEventConsumerBackgroundService : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IModel _channel;
    private readonly IConnection _connection;

    public IntegrationEventConsumerBackgroundService(IServiceProvider serviceProvider, IOptions<MessageBrokerSettings> messageBrokerSettingsOptions, 
        ILogger<IntegrationEventConsumerBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        MessageBrokerSettings messageBrokerSettings = messageBrokerSettingsOptions.Value;

        var factory = new ConnectionFactory
        {
            HostName = messageBrokerSettings.HostName,
            Port = messageBrokerSettings.Port,
            UserName = messageBrokerSettings.UserName,
            Password = messageBrokerSettings.Password
        };

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();

        _channel.QueueDeclare(messageBrokerSettings.QueueName, false, false, false);

        try
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += OnIntegrationEventReceived;

            _channel.BasicConsume(messageBrokerSettings.QueueName, false, consumer);
        }
        catch (Exception e)
        {
            logger.LogCritical($"ERROR: Failed to process the integration events: {e.Message}", e.Message);
        }
    }

    private void OnIntegrationEventReceived(object? sender, BasicDeliverEventArgs eventArgs)
    {
        string body = Encoding.UTF8.GetString(eventArgs.Body.Span);

        var integrationEvent = JsonConvert.DeserializeObject<IIntegrationEvent>(body, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        });

        using var scope = _serviceProvider.CreateScope();

        var integrationEventConsumer = scope.ServiceProvider.GetRequiredService<IIntegrationEventConsumer>();

        integrationEventConsumer.Consume(integrationEvent!);

        _channel.BasicAck(eventArgs.DeliveryTag, false);
    }

    public void Dispose()
    {
        _connection?.Dispose();
        _channel?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Dispose();

        return Task.CompletedTask;
    }
}