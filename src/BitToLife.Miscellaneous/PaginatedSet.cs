namespace BitToLife.Miscellaneous;

/// <summary>
/// 페이지 세트
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed record PaginatedSet<T>
{
    /// <summary>
    /// 전체 페이지 개수를 계산합니다.
    /// </summary>
    /// <param name="itemCount"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static int CalcTotalPageCount(int itemCount, int pageSize)
    {
        return (int)Math.Ceiling((decimal)itemCount / pageSize);
    }

    /// <summary>
    /// 페이지 세트를 생성합니다.
    /// </summary>
    /// <param name="currentPageItems">현재 페이지의 아이템</param>
    /// <param name="totalItemCount">전체 아이템 개수</param>
    /// <param name="pageSize">페이지 사이즈</param>
    /// <param name="pageIndex">페이지 인덱스</param>
    /// <returns></returns>
    public static PaginatedSet<T> Create(IEnumerable<T> currentPageItems, int totalItemCount, int pageSize, int pageIndex = 0)
    {
        int totalPageCount = CalcTotalPageCount(totalItemCount, pageSize);
        PaginatedSet<T> set = new()
        {
            PageSize = pageSize,
            CurrentPageIndex = pageIndex,
            LastPageIndex = totalPageCount - 1,
            TotalItemCount = totalItemCount,
            TotalPageCount = totalPageCount,
            Items = currentPageItems,
            HasPreviousPage = pageIndex > 0,
            HasNextPage = pageIndex + 1 < totalPageCount
        };

        return set;
    }

    /// <summary>
    /// 빈 페이지 세트를 생성합니다.
    /// </summary>
    /// <returns></returns>
    public static PaginatedSet<T> CreateEmpty()
    {
        PaginatedSet<T> set = new()
        {
            PageSize = 0,
            CurrentPageIndex = 0,
            LastPageIndex = 0,
            TotalItemCount = 0,
            TotalPageCount = 0,
            Items = [],
            HasPreviousPage = false,
            HasNextPage = false
        };

        return set;
    }

    /// <summary>
    /// 페이지 사이즈
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// 현재 페이지 인덱스
    /// </summary>
    public int CurrentPageIndex { get; init; }

    /// <summary>
    /// 마지막 페이지 인덱스
    /// </summary>
    public int LastPageIndex { get; init; }

    /// <summary>
    /// 전체 아이템 개수
    /// </summary>
    public int TotalItemCount { get; init; }

    /// <summary>
    /// 전체 페이지 수
    /// </summary>
    public int TotalPageCount { get; init; }

    /// <summary>
    /// 이전 페이지가 있는지 여부
    /// </summary>
    public bool HasPreviousPage { get; init; }

    /// <summary>
    /// 다음 페이지가 있는지 여부
    /// </summary>
    public bool HasNextPage { get; init; }

    public IEnumerable<T> Items { get; init; } = [];
}