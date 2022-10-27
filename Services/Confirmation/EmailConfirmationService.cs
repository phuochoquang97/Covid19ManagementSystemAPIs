using Covid_Project.Domain.Repositories.Confirmation;
using Covid_Project.Domain.Services.Confirmation;

namespace Covid_Project.Services.Confirmation
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly IEmailConfirmationRepository _emailConfirmationRepository;
        public EmailConfirmationService(IEmailConfirmationRepository emailConfirmationRepository)
        {
            _emailConfirmationRepository = emailConfirmationRepository;

        }
        public bool ConfirmEmail(string email, string code)
        {
            return _emailConfirmationRepository.ConfirmEmail(email, code);
        }
    }
}