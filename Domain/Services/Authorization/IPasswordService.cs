using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid_Project.Domain.Services.Authorization
{
    public interface IPasswordService
    {
        string PasswordEncoder(string passwordEncode);
        string PasswordDecoder(string passwordDecode);
    }
}