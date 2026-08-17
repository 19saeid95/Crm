using Crm.Application.Authorization;
using Crm.Application.Interfaces.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Crm.Infrastructure.Authorization;

public sealed class PermissionAuthorizationHandler(
    IPermissionService permissionService)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User.HasClaim(
                "IsSuperAdmin",
                "true"))
        {
            context.Succeed(requirement);
            return;
        }

        var userIdClaim =
            context.User.FindFirst(
                ClaimTypes.NameIdentifier);

        if (userIdClaim is null ||
            !long.TryParse(
                userIdClaim.Value,
                out var userId))
        {
            return;
        }

        var hasPermission =
            await permissionService.HasPermissionAsync(
                userId,
                requirement.Permission);

        if (hasPermission)
        {
            context.Succeed(requirement);
        }
    }
}