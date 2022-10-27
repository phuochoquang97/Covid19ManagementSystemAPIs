namespace Covid_Project.Domain.Services.Communication
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 9;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumberw;
            PageSize = pageSize < 1 ? 1 : pageSize;
        }
    }
}