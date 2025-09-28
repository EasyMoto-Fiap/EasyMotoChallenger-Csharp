namespace EasyMoto.Application.DTOs.Common
{
    public class PagedResponse<T>
    {
        public IReadOnlyList<T> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }

        public PagedResponse(IReadOnlyList<T> items, int page, int pageSize, int total)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            Total = total;
        }
    }
}