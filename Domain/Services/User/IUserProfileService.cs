using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace Covid_Project.Domain.Services.User
{
    public interface IUserProfileService
    {
        ProfileDto GetProfile(int accountId);
        ProfileDto EditProfile(int accountId, ProfileDto profile);
        bool ChangePassword(int accountId, string oldPassword, string newPassword);
        string GetAvatar(int accountId);
        string PostAvatar(int accountId, IFormFile avatar);
    }
}