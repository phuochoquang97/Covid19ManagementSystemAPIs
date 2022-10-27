using Covid_Project.Domain.Models.Authorazation;
using Covid_Project.Domain.Services.Communication;

namespace Covid_Project.Domain.Repositories.Authorization
{
    public interface IRegisterRepository
    {
        bool CheckAccountExistence(string email);
        int Register(RegisterModel register);

        bool ForgotPasswordCode(string email);
        bool ForgotPassword(string email, string code, string password);
    }
}