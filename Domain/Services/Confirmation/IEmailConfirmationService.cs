namespace Covid_Project.Domain.Services.Confirmation
{
    public interface IEmailConfirmationService
    {
         bool ConfirmEmail(string Email, string code);
    }
}