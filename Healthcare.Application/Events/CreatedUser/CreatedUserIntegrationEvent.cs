using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Events;
using Newtonsoft.Json;

namespace Healthcare.Application.Events.CreatedUser;

public sealed class CreatedUserIntegrationEvent : IIntegrationEvent
{
    // For publishing
    internal CreatedUserIntegrationEvent(CreatedUserDomainEvent userCreatedDomainEvent) => UserId = userCreatedDomainEvent.User.Id;
        
    // For consuming 
    [JsonConstructor]
    private CreatedUserIntegrationEvent(Guid userId) => UserId = userId;

    public Guid UserId { get; }   
}