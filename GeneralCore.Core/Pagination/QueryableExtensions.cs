using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace GeneralCore.Pagination;

public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        IPageOptions options,
        CancellationToken cancellationToken = default)
    {
        var totalRows = await query.CountAsync(cancellationToken);

        var rows = await query
            .Skip((options.PageNumber - 1) * options.PageSize)
            .Take(options.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>(totalRows, options.PageSize, rows);
    }

    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        SortFilterPageOptions options,
        CancellationToken cancellationToken = default)
    {
        if (options.HasSort())
            query = query.ApplySort(options.SortBy!, options.IsDescending);

        return await query.ToPagedResultAsync((IPageOptions)options, cancellationToken);
    }

    public static IQueryable<T> ApplySort<T>(
        this IQueryable<T> query,
        string sortBy,
        bool descending)
    {
        var property = typeof(T).GetProperty(
                           sortBy,
                           BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                       ?? typeof(T).GetProperty(
                           "Id",
                           BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property is null)
            return query;

        var param = Expression.Parameter(typeof(T), "x");
        var body = Expression.Convert(Expression.Property(param, property), typeof(object));
        var keySelector = Expression.Lambda<Func<T, object>>(body, param);

        return descending
            ? query.OrderByDescending(keySelector)
            : query.OrderBy(keySelector);
    }
}
