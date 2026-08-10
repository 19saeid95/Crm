namespace Crm.Domain.BaseEntites;

public abstract class BaseEntity<TKey>
{
    public TKey Id { get; set; } = default!;

    public DateTime CreateDate { get; set; }

    public DateTime? LastUpdate { get; set; }

    public bool IsDeleted { get; set; }
}
