using Healthcare.Application.Core.Abstractions.Messaging;

namespace BackgroundJobs.Services;

public interface IIntegrationEventConsumer
{
    void Consume(IIntegrationEvent integrationEvent);
}