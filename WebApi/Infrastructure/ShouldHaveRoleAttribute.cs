using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Infrastructure;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ShouldHaveRole : Attribute, IAuthorizationFilter
{
    public string Role { get; set; } = string.Empty;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var identityService = context.HttpContext.RequestServices.GetRequiredService<IIdentityService>()!;

        var result = identityService.ValidateJwt().Result;

        if (result.IsFailure)
        {
            context.Result = new StatusCodeResult(401);
            return;
        }

        if (Role == nameof(UserPermission.Patient))
        {
            return;
        }

        var permissionRole = MapUserPermissionToNames(result.Value);

        if (Role != permissionRole)
        {
            context.Result = new StatusCodeResult(403);
        }
    }

    private static string MapUserPermissionToNames(UserPermission permission) => permission switch
    {
        UserPermission.Admin => nameof(UserPermission.Admin),
        UserPermission.Doctor => nameof(UserPermission.Doctor),
        UserPermission.Patient => nameof(UserPermission.Patient),
        UserPermission.Pharmacist => nameof(UserPermission.Pharmacist),
        UserPermission.Registratur => nameof(UserPermission.Registratur),
        _ => nameof(UserPermission.Unknown),
    };
}