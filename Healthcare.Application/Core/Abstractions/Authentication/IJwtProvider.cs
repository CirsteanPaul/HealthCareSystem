using Healthcare.Domain.Entities;

namespace Healthcare.Application.Core.Abstractions.Authentication;

public interface IJwtProvider
{
    string Create(Guid userId, string email, UserPermission userPermission);
}