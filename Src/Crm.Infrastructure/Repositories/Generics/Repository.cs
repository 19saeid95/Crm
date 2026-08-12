using Crm.Domain.Entities.BaseEntites;
using Crm.Domain.Repositories.Generics;
using Crm.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Crm.Infrastructure.Repositories.Generics;

public class Repository<TEntity, TKey>(
    CrmDbContext context)
    : IRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
{
    protected readonly CrmDbContext Context = context;

    protected readonly DbSet<TEntity> DbSet =
        context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync(
            [id],
            cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(
            entity,
            cancellationToken);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        entity.IsDeleted = true;
        DbSet.Update(entity);
    }
}