using Covid_Project.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Covid_Project.Domain.Repositories.User
{
    public interface IUserProfileRepository
    {
        Profile GetProfile(int accountId);
        Profile EditProfile(int accountId, Profile profile);
        bool ChangePassword(int accountId, string oldPassword, string newPassword);
        string GetAvatar(int accountId);
        string PostAvatar(int accountId, IFormFile avatar);
    }
}