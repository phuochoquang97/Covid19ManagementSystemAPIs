using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.Authorazation;
using Covid_Project.Domain.Services.Communication;

namespace Covid_Project.Domain.Repositories
{
    public interface ILoginRepository
    {
        LoginResponse Login(string email, string password);
    }
}