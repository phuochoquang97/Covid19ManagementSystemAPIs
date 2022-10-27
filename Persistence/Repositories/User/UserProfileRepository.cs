using System;
using System.IO;
using System.Linq;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Repositories.User;
using Covid_Project.Persistence.Context;
using Covid_Project.Services.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Covid_Project.Persistence.Repositories.User
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly AppDbContext _context;
        public UserProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool ChangePassword(int accountId, string oldPassword, string newPassword)
        {
            try
            {
                var account = _context.Accounts.FirstOrDefault(x => x.Id == accountId && x.IsDeleted == false);
                if (account == null)
                {
                    return false;
                }
                var _passwordService = new PasswordService();
                var checkPassword = _passwordService.PasswordEncoder(oldPassword).Equals(account.Password);
                if(checkPassword == false)
                {
                    return false;
                }
                account.Password = _passwordService.PasswordEncoder(newPassword);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public Profile EditProfile(int accountId, Profile profile)
        {
            try
            {
                var profileEditted = _context.Profiles.FirstOrDefault(x => x.AccountId == accountId && x.IsDeleted == false);
                if (profileEditted == null)
                {
                    return null;
                }

                profileEditted.FirstName = profile.FirstName;
                profileEditted.LastName = profile.LastName;
                profileEditted.Address = profile.Address;
                profileEditted.DateOfBirth = profile.DateOfBirth;
                profileEditted.Gender = profile.Gender;
                profileEditted.PhoneNumber = profile.PhoneNumber;
                profileEditted.Nationality = profile.Nationality;
                profileEditted.IdNo = profile.IdNo;
                profileEditted.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();

                return profile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        

        public Profile GetProfile(int accountId)
        {
            try
            {
                var profile =  _context.Profiles.Where(s => s.IsDeleted == false).FirstOrDefault(x => x.AccountId == accountId);
                profile.Account = _context.Accounts.FirstOrDefault(x => x.Id == accountId);
                return profile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        // Avatar
        public string GetAvatar(int accountId)
        {
            try
            {
                var account = _context.Accounts
                .Include(x => x.Profile)
                .FirstOrDefault(x => x.Id == accountId && x.IsDeleted == false);
                if(account == null)
                {
                    return null;
                }
                return Path.Combine("/Image", "Avatar", account.Profile.Avatar);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public string PostAvatar(int accountId, IFormFile avatar)
        {
            try
            {
                if (CheckFile(avatar))
                {
                    return WriteFile(accountId, avatar);
                }
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        private bool CheckFile(IFormFile avatar)
        {
            if(avatar.Length / (1024*1024) > 11)
            {
                return false;
            }
            var fileName = avatar.FileName;
            string ext = Path.GetExtension(fileName);
            if (ext.Equals(".png") || ext.Equals(".jpg") || ext.Equals(".jpeg"))
            {
                return true;
            }
            return false;
        }

        private string WriteFile(int accountId, IFormFile avatar)
        {
            try
            {
                var account = _context.Accounts
                .Include(x => x.Profile)
                .FirstOrDefault(x => x.Id == accountId && x.IsDeleted == false);

                if(account.Profile.Avatar != null)
                {
                    var link = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Avatar", account.Profile.Avatar);
                    File.Delete(link);
                }

                var ext = Path.GetExtension(avatar.FileName);
                string fileName = $"user{accountId}_{Path.GetFileNameWithoutExtension(avatar.FileName)}_{avatar.Length}" + ext;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Avatar", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    avatar.CopyTo(bits);
                    account.Profile.Avatar = fileName;
                    _context.SaveChanges();
                }
                return fileName;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}