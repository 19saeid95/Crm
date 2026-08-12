using Crm.Domain.Entities;
using Crm.Infrastructure.Persistence;
using Crm.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;

public class UserRepository(
    CrmDbContext context)
    : Repository<User, long>(context),
      IUserRepository
{
    public Task<User?> GetByUserNameAsync(
        string userName,
        CancellationToken cancellationToken = default)
    {
        return context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.UserName == userName &&
                     !x.IsDeleted,
                cancellationToken);
    }
}