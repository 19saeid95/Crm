using Crm.Domain.Entities;
using Crm.Domain.Repositories;
using Crm.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Crm.Infrastructure.Repositories;

public sealed class CustomerRepository(CrmDbContext context) : ICustomerRepository
{
    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await context.Customers.AddAsync(customer, cancellationToken);
    }

    public async Task<Customer?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId && !x.IsDeleted, cancellationToken);
    }

    public async Task<Customer?> GetByLocationIdAsync(long locationId, CancellationToken cancellationToken = default)
    {
        return await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.LocationId == locationId && !x.IsDeleted, cancellationToken);
    }

    public async Task<Customer?> GetByCustomerCodeAsync(string customerCode, CancellationToken cancellationToken = default)
    {
        return await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerCode == customerCode && !x.IsDeleted, cancellationToken);
    }

    public async Task<(List<Customer> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = context.Customers.AsNoTracking()
            .Include(customer => customer.User)
            .Include(customer => customer.Location)
            .Where(customer => !customer.IsDeleted)
            .OrderBy(customer => customer.Id);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return (items, totalCount);
    }
}