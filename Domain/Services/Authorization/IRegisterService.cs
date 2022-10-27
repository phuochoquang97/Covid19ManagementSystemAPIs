using Covid_Project.Domain.Models.Authorazation;

namespace Covid_Project.Domain.Services.Authorization
{
    public interface IRegisterService
    {  
        bool checkAccountExistence(string email);
        int Register(RegisterModel register);
        bool ForgotPasswordCode(string email);
        bool ForgotPassword(string email, string code, string password);
    }
}