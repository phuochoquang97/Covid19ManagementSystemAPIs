namespace Covid_Project.Domain.Models.Authorazation
{
    public class ConfirmationReponse
    {
        public ConfirmationReponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public bool IsSuccess { get; set; }
    }
}