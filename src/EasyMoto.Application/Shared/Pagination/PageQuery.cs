namespace EasyMoto.Application.Shared.Pagination
{
    public sealed class PageQuery
    {
        private int _page = 1;
        private int _size = 10;

        public int Page
        {
            get => _page;
            set => _page = value < 1 ? 1 : value;
        }

        public int Size
        {
            get => _size;
            set => _size = value < 1 ? 10 : value;
        }

        public PageQuery() { }

        public PageQuery(int page, int size)
        {
            Page = page;
            Size = size;
        }
    }
}