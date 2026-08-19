using Crm.Domain.Entities;

namespace Crm.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<User?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<bool> HasPermissionAsync(long userId, string permission, CancellationToken cancellationToken = default);
}

