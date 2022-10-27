using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.Authorazation;
using Covid_Project.Domain.Repositories;
using Covid_Project.Domain.Services.Authorization;

namespace Covid_Project.Services.Authorization
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _login;
        public LoginService(ILoginRepository login)
        {
            _login = login;

        }
        public LoginResponse Login(string email, string password)
        {
            return _login.Login(email, password);
        }
    }
}