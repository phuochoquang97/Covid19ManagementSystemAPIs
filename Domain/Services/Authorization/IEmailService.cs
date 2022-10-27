using System.Threading.Tasks;
using Covid_Project.Domain.Models.Email;

namespace Covid_Project.Domain.Services.Authorization
{
    public interface IEmailService
    {
        bool SendEmail(EmailMessage message);
    }
}