using Crm.Domain.Entities;
using Crm.Domain.Repositories.Generics;

public interface IUserRepository
    : IRepository<User, long>
{
    Task<User?> GetByUserNameAsync(
        string userName,
        CancellationToken cancellationToken = default);
}