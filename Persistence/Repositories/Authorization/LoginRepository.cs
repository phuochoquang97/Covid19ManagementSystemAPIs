using System;
using System.Linq;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.Authorazation;
using Covid_Project.Domain.Repositories;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Domain.Services.Authorization;
using Covid_Project.Domain.Services.Communication;
using Covid_Project.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Covid_Project.Persistence.Repositories.Authorization
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _context;
        private readonly IPasswordService _passwordService;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly IRoleRepository _role;
        private readonly IPermissionRepository _permission;
        public LoginRepository(AppDbContext context, 
                                IPasswordService passwordService, 
                                IJwtAuthenticationManager jwtAuthenticationManager,
                                IRoleRepository role,
                                IPermissionRepository permission)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _passwordService = passwordService;
            _context = context;
            _role = role;
            _permission = permission;

        }

        public LoginResponse Login(string email, string password)
        {
            try
            {
                if (email.Equals("") || password.Equals(""))
                {
                    return null;
                }
                var account = _context.Accounts
                .Include(x => x.Profile)
                .FirstOrDefault(x => x.Email.Equals(email));
                if (account == null)
                {
                    return null;
                }
                // var passwordDecode = _passwordService.PasswordDecoder(account.Password);
                var passwordEncode = _passwordService.PasswordEncoder(password);
                if (passwordEncode.Equals(account.Password))
                {
                    var roles = _role.GetRoles(account.Id);
                    var returnedAccount = new LoginResponse
                    {
                        Id = account.Id,
                        FullName = account.Profile.FirstName + " " + account.Profile.LastName ,
                        Token = _jwtAuthenticationManager.Authenticate(account.Id, account.Email),
                        Roles = roles,
                        IsVerified = account.IsVerified == 1 ? true : false,
                        Permissions = _permission.GetListPermission(roles),
                        Email = email
                    };
                    return returnedAccount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
    }
}