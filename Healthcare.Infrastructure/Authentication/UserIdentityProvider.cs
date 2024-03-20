using System.Security.Claims;
using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.Results;
using Microsoft.AspNetCore.Http;

namespace Healthcare.Infrastructure.Authentication;

public class UserIdentityProvider : IUserIdentityProvider, IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    public UserIdentityProvider(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public async Task<Result<UserPermission>> ValidateJwt()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue("userId")
                          ?? string.Empty;
        var emailClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue("email_address")
                             ?? string.Empty;
        var userPermissionClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue("user_permission") 
                                  ?? string.Empty;
        
        if (Guid.TryParse(userIdClaim, out var guid))
        {
            UserId = guid;
        }
        
        Email = emailClaim;
        
        if (int.TryParse(userPermissionClaim, out var result))
        {
            UserPermission = (UserPermission)result;
        }

        if (UserId == Guid.Empty || Email == string.Empty || UserPermission == UserPermission.Unknown)
        {
            return Result.Failure<UserPermission>(DomainErrors.General.InternalServerError);
        }

        var userResult = await _userRepository.FindByIdAsync(UserId);
        
        if (userResult.IsFailure)
        {
            return Result.Failure<UserPermission>(DomainErrors.General.InternalServerError);
        }
        
        if (userResult.Value.UserPermission != UserPermission)
        {
            return Result.Failure<UserPermission>(DomainErrors.General.InternalServerError);
        }

        return UserPermission;
    }

    public Guid UserId { get; private set; } = Guid.Empty;
    public string Email { get; private set; } = string.Empty;
    public UserPermission UserPermission { get; private set; } = UserPermission.Unknown;
}