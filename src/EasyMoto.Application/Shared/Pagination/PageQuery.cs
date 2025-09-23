namespace EasyMoto.Application.Shared.Pagination;

public sealed class PageQuery
{
    public int Page { get; }
    public int Size { get; }

    public PageQuery(int page = 1, int size = 10)
    {
        Page = page < 1 ? 1 : page;
        Size = size < 1 ? 10 : size;
    }
}