namespace Covid_Project.Domain.Services.Communication
{
    public class Response<T>
    {
        public Response()
        {
            this.IsSuccess = true;
            this.Code = 200;
        }
        public Response(T data)
        {
            IsSuccess = true;
            Message = string.Empty;
            Code = 200;
            Data = data;
        }
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}