namespace EasyMoto.Application.Shared.Pagination;

public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; }
    public int Page { get; }
    public int Size { get; }
    public long TotalCount { get; }
    public int TotalPages => Size <= 0 ? 0 : (int)Math.Ceiling((double)TotalCount / Size);
    public bool HasPrevious => Page > 1;
    public bool HasNext => Page < TotalPages;

    public PagedResult(IEnumerable<T> items, int page, int size, long totalCount)
    {
        Items = items is IReadOnlyList<T> list ? list : items.ToList();
        Page = page < 1 ? 1 : page;
        Size = size < 1 ? 10 : size;
        TotalCount = totalCount < 0 ? 0 : totalCount;
    }
}