using Crm.Application.Contracts.Authentication;
using Crm.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Crm.Infrastructure.Authentication;

public sealed class PermissionAuthorizationHandler(ICurrentUser currentUser, IUserRepository userRepository)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (!currentUser.IsAuthenticated)
            return;

        if (currentUser.UserId <= 0)
            return;

        var hasPermission = await userRepository.HasPermissionAsync(currentUser.UserId, requirement.Permission, CancellationToken.None);

        if (hasPermission)
            context.Succeed(requirement);

    }
}
