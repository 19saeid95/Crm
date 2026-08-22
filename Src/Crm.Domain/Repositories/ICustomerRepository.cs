using Crm.Domain.Entities;

namespace Crm.Domain.Repositories;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);

    Task<Customer?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);

    Task<Customer?> GetByLocationIdAsync(long locationId, CancellationToken cancellationToken = default);

    Task<Customer?> GetByCustomerCodeAsync(string customerCode, CancellationToken cancellationToken = default);

    Task<(List<Customer> Items, int TotalCount)> GetPagedAsync(int pageNumber,int pageSize,CancellationToken cancellationToken = default);
}