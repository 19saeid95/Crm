using Crm.Application.Interfaces.Authorization;
using Crm.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Crm.Infrastructure.Authorization;

public sealed class PermissionService(
    CrmDbContext context)
    : IPermissionService
{
    public async Task<bool> HasPermissionAsync(
        long userId,
        string permission,
        CancellationToken cancellationToken = default)
    {
        return await context.UserRoles
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .SelectMany(x => x.Role.RolePermissions)
            .AnyAsync(
                x => x.Permission.Name == permission &&
                     !x.Permission.IsDeleted &&
                     !x.Role.IsDeleted,
                cancellationToken);
    }
}