using Crm.Domain.BaseEntites;

namespace Crm.Domain.Repositories.Generics;

public interface IEfRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
{
    Task<TEntity?> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}