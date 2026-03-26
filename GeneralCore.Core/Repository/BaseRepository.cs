using Microsoft.EntityFrameworkCore;

namespace GeneralCore.Repository;

public abstract class BaseRepository<TId, TEntity> : IBaseRepository<TId, TEntity>
    where TEntity : class
{
    protected DbContext Context { get; }
    protected DbSet<TEntity> Set => Context.Set<TEntity>();

    protected BaseRepository(DbContext context)
    {
        Context = context;
    }

    public virtual IQueryable<TEntity> Query => Set;

    public virtual ValueTask<TEntity?> FindAsync(TId id, CancellationToken ct = default)
        => Set.FindAsync(new object?[] { id }, ct);

    public void Add(TEntity entity) => Set.Add(entity);
    public void AddRange(IEnumerable<TEntity> entities) => Set.AddRange(entities);
    public void Remove(TEntity entity) => Set.Remove(entity);
    public void RemoveRange(IEnumerable<TEntity> entities) => Set.RemoveRange(entities);
}
