using Covid_Project.Domain.Models.Authorazation;
using Covid_Project.Domain.Repositories;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Domain.Services.Authorization;

namespace Covid_Project.Services.Authorization
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepository _register;
        public RegisterService(IRegisterRepository register)
        {
            _register = register;
        }

        public bool checkAccountExistence(string email)
        {
            return _register.CheckAccountExistence(email);
        }

        public bool ForgotPasswordCode(string email)
        {
            return _register.ForgotPasswordCode(email);
        }

        public bool ForgotPassword(string email, string code, string password)
        {
            return _register.ForgotPassword(email, code, password);
        }
        public int Register(RegisterModel register)
        {
            return _register.Register(register);
        }

    }
}