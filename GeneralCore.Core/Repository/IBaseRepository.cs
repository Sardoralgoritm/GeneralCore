namespace GeneralCore.Repository;

public interface IBaseRepository<TId, TEntity>
    where TEntity : class
{
    IQueryable<TEntity> Query { get; }
    ValueTask<TEntity?> FindAsync(TId id, CancellationToken ct = default);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}
