namespace Covid_Project.Domain.Models.Authorazation
{
    public class RegisterResponse
    {
        public RegisterResponse()
        {
            IsSuccess = true;
            IsVerified = false;
        }
        public int AccountId { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public bool IsVerified { get; set; }
    }
}