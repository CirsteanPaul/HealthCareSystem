using Healthcare.Application.Core.Abstractions.Messaging;
using MediatR;

namespace BackgroundJobs.Services;

public sealed class IntegrationEventConsumer : IIntegrationEventConsumer
{
    private readonly IMediator _mediator;

    public IntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void Consume(IIntegrationEvent integrationEvent)
    {
        _mediator.Publish(integrationEvent).GetAwaiter().GetResult();
    }
}