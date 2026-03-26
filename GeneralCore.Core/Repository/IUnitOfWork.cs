using Microsoft.EntityFrameworkCore.Storage;

namespace GeneralCore.Repository;

public interface IUnitOfWork
{
    Task SaveAsync(CancellationToken ct = default);
    void Save();

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default);
    IDbContextTransaction BeginTransaction();
    void Commit();
    void Rollback();
}
