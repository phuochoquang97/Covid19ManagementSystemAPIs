using System;
using System.Collections.Generic;
using System.Linq;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Repositories.Admin;
using Covid_Project.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Covid_Project.Persistence.Repositories.Admin
{
    public class AdminUserRepository : IAdminUserRepository
    {
        private readonly AppDbContext _context;
        public AdminUserRepository(AppDbContext context)
        {
            _context = context;

        }
        public List<Profile> GetAllUserProfile()
        {
            try
            {
                var accounts = _context.Accounts
                .Where(x => x.IsDeleted == false && !x.Code.Equals("ADMINCODE"));
                var userProfiles = _context.Profiles
                .Include(x => x.Account)
                .Where(x => x.IsDeleted == false && !x.Account.Code.Equals("ADMINCODE"))
                .ToList();

                foreach(var profile in userProfiles)
                {
                    profile.Account = accounts.FirstOrDefault(x => x.Id == profile.AccountId);
                }
                
                return userProfiles;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}