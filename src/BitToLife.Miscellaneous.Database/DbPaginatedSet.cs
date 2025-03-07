using Microsoft.EntityFrameworkCore;

namespace BitToLife.Miscellaneous.Database;

/// <summary>
/// 데이터베이스 페이지 세트
/// </summary>
public static class DbPaginatedSet
{
    /// <summary>
    /// 페이지 세트를 생성합니다.
    /// </summary>
    /// <param name="query">데이터 쿼리</param>
    /// <param name="pageSize">페이지 사이즈</param>
    /// <param name="pageIndex">페이지 인덱스</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async static Task<PaginatedSet<T>> CreateAsync<T>(IQueryable<T> query, int pageSize, int pageIndex = 0, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize);
        ArgumentOutOfRangeException.ThrowIfNegative(pageIndex);

        int totalItemCount = await query.CountAsync(cancellationToken);
        int totalPageCount = PaginatedSet<T>.CalcTotalPageCount(totalItemCount, pageSize);
        IEnumerable<T> items = await query.Skip(pageSize * pageIndex).Take(pageSize).ToArrayAsync(cancellationToken);

        PaginatedSet<T> set = new()
        {
            PageSize = pageSize,
            CurrentPageIndex = pageIndex,
            TotalItemCount = totalItemCount,
            TotalPageCount = totalPageCount,
            Items = items,
            HasPreviousPage = pageIndex > 0,
            HasNextPage = pageIndex + 1 < totalPageCount
        };

        return set;
    }
}