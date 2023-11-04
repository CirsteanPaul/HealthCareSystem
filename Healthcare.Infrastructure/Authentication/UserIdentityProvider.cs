using System.Security.Claims;
using Healthcare.Application.Core.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace Healthcare.Infrastructure.Authentication;

public class UserIdentityProvider : IUserIdentityProvider
{
    public UserIdentityProvider(IHttpContextAccessor httpContextAccessor)
    {
        string userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue("userId")
                             ?? "nothing";
        UserId = userIdClaim;
    }
    
    public string UserId { get; }
}