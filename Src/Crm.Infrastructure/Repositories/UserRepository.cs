using Crm.Domain.Entities;
using Crm.Domain.Repositories;
using Crm.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Crm.Infrastructure.Repositories;

public class UserRepository(CrmDbContext context) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
    }

    public async Task<User?> GetByPhoneAsync(string phone, CancellationToken cancellationToken = default)
    {
        return await context.Users.FirstOrDefaultAsync(user => user.Phone == phone && !user.IsDeleted, cancellationToken);
    }

    public async Task<User?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted == false, cancellationToken);
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.IsDeleted == false, cancellationToken);
    }

    public async Task<bool> HasPermissionAsync(long userId, string permission, CancellationToken cancellationToken = default)
    {
        return await context.Users.
            AsNoTracking()
            .Where(user => user.Id == userId && !user.IsDeleted && user.IsActive)
               .AnyAsync(user => user.IsSuperAdmin || user.UserRoles
               .Any(userRole => userRole.Role.RolePermissions
               .Any(rolePermission => !rolePermission.Permission.IsDeleted && rolePermission.Permission.Name == permission)),
                cancellationToken);
    }
}
