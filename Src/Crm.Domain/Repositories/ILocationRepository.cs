using Crm.Domain.Entities;

namespace Crm.Domain.Repositories;

public interface ILocationRepository
{
    Task AddAsync(Location location, CancellationToken cancellationToken = default);
    Task<Location?> GetByIdAsync(long locationId, CancellationToken cancellationToken = default);
}