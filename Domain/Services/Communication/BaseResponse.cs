namespace Covid_Project.Domain.Services.Communication
{
    public class BaseResponse
    {
        public BaseResponse(int code, string message)
        {
            Code = code;
            Message = message;            
        }
        public int Code { get; set; }
        public string Message { get; set; }
        
    }
}