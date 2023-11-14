using Healthcare.Domain.Entities;

namespace Healthcare.Domain.Events;

public class ChangedPasswordUserDomainEvent : IDomainEvent
{
    internal ChangedPasswordUserDomainEvent(User user) => User = user;
    
    public User User { get; }
}