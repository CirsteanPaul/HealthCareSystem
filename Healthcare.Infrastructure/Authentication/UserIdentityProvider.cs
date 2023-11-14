using System.Security.Claims;
using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Healthcare.Infrastructure.Authentication;

public class UserIdentityProvider : IUserIdentityProvider
{
    public UserIdentityProvider(IHttpContextAccessor httpContextAccessor)
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue("userId")
                             ?? string.Empty;
        var emailClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue("email_address")
                             ?? string.Empty;
        var userPermissionClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue("user_permission") 
                                  ?? string.Empty;
        UserId = Guid.Empty;
        
        if (Guid.TryParse(userIdClaim, out var guid))
        {
            UserId = guid;
        }
        
        Email = emailClaim;
        
        UserPermission = UserPermission.Unknown;
        if (int.TryParse(userPermissionClaim, out var result))
        {
            UserPermission = (UserPermission)result;
        }
    }
    
    public Guid UserId { get; }
    public string Email { get; }
    public UserPermission UserPermission { get; }
}