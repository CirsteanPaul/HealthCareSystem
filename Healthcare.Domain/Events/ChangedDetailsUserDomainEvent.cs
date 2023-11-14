using Healthcare.Domain.Entities;

namespace Healthcare.Domain.Events;

public class ChangedDetailsUserDomainEvent : IDomainEvent
{
    internal ChangedDetailsUserDomainEvent(User user) => User = user;
    
    public User User { get; }
}