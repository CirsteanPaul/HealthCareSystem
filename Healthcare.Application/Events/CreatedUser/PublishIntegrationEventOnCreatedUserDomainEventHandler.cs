using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Events;

namespace Healthcare.Application.Events.CreatedUser;

public sealed class PublishIntegrationEventOnCreatedUserDomainEventHandler : IDomainEventHandler<CreatedUserDomainEvent>
{
    private readonly IIntegrationEventPublisher _integrationEventPublisher;

    public PublishIntegrationEventOnCreatedUserDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher)
    {
        _integrationEventPublisher = integrationEventPublisher;
    }

    public async Task Handle(CreatedUserDomainEvent notification, CancellationToken cancellationToken)
    {
        _integrationEventPublisher.Publish(new CreatedUserIntegrationEvent(notification));

        await Task.CompletedTask;
    }
}