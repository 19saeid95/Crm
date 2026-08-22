using Crm.Domain.Entities;
using Crm.Domain.Repositories;
using Crm.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Crm.Infrastructure.Repositories;

public sealed class LocationRepository(CrmDbContext context) : ILocationRepository
{
    public async Task AddAsync(Location location, CancellationToken cancellationToken = default)
    {
        await context.Locations.AddAsync(location, cancellationToken);
    }

    public async Task<Location?> GetByIdAsync(long locationId, CancellationToken cancellationToken = default)
    {
        return await context.Locations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == locationId && !x.IsDeleted, cancellationToken);
    }
}