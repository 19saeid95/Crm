using Crm.Domain.Entities;

namespace Crm.Domain.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<User?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<bool> HasPermissionAsync(long userId, string permission, CancellationToken cancellationToken = default);
    Task<User?> GetByPhoneAsync(string phone, CancellationToken cancellationToken = default);
    Task<User?> GetDetailsByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<(List<User> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}

