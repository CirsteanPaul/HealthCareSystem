using Healthcare.Domain.Entities;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Application.Core.Abstractions.Authentication;

public interface IIdentityService
{ 
    Task<Result<UserPermission>> ValidateJwt();
}