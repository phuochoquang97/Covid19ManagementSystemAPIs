namespace Covid_Project.Domain.Repositories.Confirmation
{
    public interface IEmailConfirmationRepository
    {
         bool ConfirmEmail(string email, string code);
    }
}