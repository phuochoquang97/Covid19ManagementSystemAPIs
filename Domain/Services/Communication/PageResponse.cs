using System.Reflection.PortableExecutable;
namespace Covid_Project.Domain.Services.Communication
{
    public class PageResponse<T> : Response<T>
    {
        public PageResponse(T data, int pageNumber, int pageSize)
        {
            
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            Message = null;
            IsSuccess = true;
            Code = 200;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public int CalTotalPages(int totalRecord, int pageSize)
        {
            return totalRecord%pageSize == 0 ? totalRecord/pageSize : totalRecord/pageSize + 1;
        }
    }
}