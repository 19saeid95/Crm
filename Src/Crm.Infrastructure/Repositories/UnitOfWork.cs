using Crm.Domain.Repositories;
using Crm.Infrastructure.Persistence;

namespace Crm.Infrastructure.Repositories;

public class UnitOfWork(
    CrmDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(
            cancellationToken);
    }
}