using Healthcare.Domain.Entities;

namespace Healthcare.Domain.Events;

public sealed class CreatedUserDomainEvent : IDomainEvent
{
    internal CreatedUserDomainEvent(User user) => User = user;
    
    public User User { get; }
}