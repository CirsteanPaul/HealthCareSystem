using Healthcare.Domain.Entities;

namespace Healthcare.Application.Core.Abstractions.Authentication;

public interface IUserIdentityProvider
{ 
    Guid UserId { get; }
    string Email { get; }
    UserPermission UserPermission { get; }
}