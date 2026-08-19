using Crm.Domain.Entities;

namespace Crm.Domain.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<User?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<bool> HasPermissionAsync(long userId, string permission, CancellationToken cancellationToken = default);
    Task<User?> GetByPhoneAsync(string phone, CancellationToken cancellationToken = default);
}

