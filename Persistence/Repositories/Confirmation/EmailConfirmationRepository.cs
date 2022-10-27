using System.Linq;
using Covid_Project.Domain.Repositories.Confirmation;
using Covid_Project.Persistence.Context;

namespace Covid_Project.Persistence.Repositories.Confirmation
{
    public class EmailConfirmationRepository : IEmailConfirmationRepository
    {
        private readonly AppDbContext _context;
        public EmailConfirmationRepository(AppDbContext context)
        {
            _context = context;

        }
        public bool ConfirmEmail(string email, string code)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Email.Equals(email));
            if(account == null)
            {
                return false;
            }
            if(code.Equals(account.Code))
            {
                account.IsVerified = 1;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}