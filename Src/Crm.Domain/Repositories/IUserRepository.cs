using Crm.Domain.Entities;

namespace Crm.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}

