using AutoMapper;
using AutoMapper.QueryableExtensions;
using GeneralCore.Pagination;
using GeneralCore.Repository;
using StatusGeneric;

namespace GeneralCore.Services;

public abstract class BaseService<TId, TEntity, TListDto, TFilterOptions> : StatusGenericHandler
    where TEntity : class
    where TListDto : class
    where TFilterOptions : SortFilterPageOptions
{
    protected readonly IBaseRepository<TId, TEntity> Repository;
    protected readonly IMapper Mapper;
    protected readonly IUnitOfWork UnitOfWork;

    protected BaseService(
        IBaseRepository<TId, TEntity> repository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        Repository = repository;
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }

    public virtual Task<PagedResult<TListDto>> GetListAsync(
        TFilterOptions options,
        CancellationToken ct = default)
    {
        return ApplyFilter(Repository.Query, options)
            .ProjectTo<TListDto>(Mapper.ConfigurationProvider)
            .ToPagedResultAsync(options, ct);
    }

    // Concrete service da override qilib filter qo'shiladi
    protected virtual IQueryable<TEntity> ApplyFilter(
        IQueryable<TEntity> query,
        TFilterOptions options) => query;
}
