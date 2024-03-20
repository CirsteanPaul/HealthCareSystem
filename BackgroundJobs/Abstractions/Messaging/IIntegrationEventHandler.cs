using Healthcare.Application.Core.Abstractions.Messaging;
using MediatR;

namespace BackgroundJobs.Abstractions.Messaging;

public interface IIntegrationEventHandler<in TIntegrationEvent> : INotificationHandler<TIntegrationEvent> 
    where TIntegrationEvent: IIntegrationEvent
{
    
}