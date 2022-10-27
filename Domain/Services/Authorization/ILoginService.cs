using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.Authorazation;

namespace Covid_Project.Domain.Services.Authorization
{
    public interface ILoginService
    {
         LoginResponse Login(string email, string password);
    }
}