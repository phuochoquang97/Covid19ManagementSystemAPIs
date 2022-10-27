using System;
using System.Linq;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.Authorazation;
using Covid_Project.Domain.Models.Email;
using Covid_Project.Domain.Repositories;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Domain.Services.Authorization;
using Covid_Project.Domain.Services.Communication;
using Covid_Project.Persistence.Context;

namespace Covid_Project.Persistence.Repositories.Authorization
{
    public class RegisterRepository : BaseRepository, IRegisterRepository
    {
        private readonly IPasswordService _passwordSerivce;
        private readonly AppDbContext _context;
        private readonly IRandomCodeGeneratorService _randomCodeGenratorService;
        private readonly IEmailService _emailService;
        public RegisterRepository(AppDbContext context,
                                  IPasswordService passwordSerivce,
                                  IRandomCodeGeneratorService randomCodeGenratorService,
                                  IEmailService emailService) : base(context)
        {
            _randomCodeGenratorService = randomCodeGenratorService;
            _context = context;
            _passwordSerivce = passwordSerivce;
            _emailService = emailService;

        }

        public bool CheckAccountExistence(string email)
        {
            var checkAccountExsistence = _context.Accounts.FirstOrDefault(x => x.Email.Equals(email));
            if(checkAccountExsistence != null)
            {
                return true;
            }
            return false;
        }

        public bool ForgotPasswordCode(string email)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Email.Equals(email));
            if(account == null)
            {
                return false;
            }

            var resetPasswordCode = _randomCodeGenratorService.GenerateRandomCode(6);
            string messageBody = "Đây là mã khôi phục mật khẩu của bạn: " + resetPasswordCode;
            var message = new EmailMessage(email, "KHÔI PHỤC MẬT KHẨU" , messageBody);
            var checkSendEmail = _emailService.SendEmail(message);
            if(!checkSendEmail)
            {
                return false;
            }
            
            account.DynamicCode = resetPasswordCode;
            _context.SaveChanges();
            return true;
        }
        public bool ForgotPassword(string email, string code, string password)
        {
            try
            {
                var account = _context.Accounts.FirstOrDefault(x => x.Email.Equals(email));
                if(account == null || !account.DynamicCode.Equals(code))
                {
                    return false;
                }
                
                var encodePassword = _passwordSerivce.PasswordEncoder(password);
                account.Password = encodePassword;
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
            
        }

        public int Register(RegisterModel register)
        {
            try
            {
                string passwordEncode = _passwordSerivce.PasswordEncoder(register.Password);
                var registerAccount = new Account
                {
                    Email = register.Email,
                    Password = passwordEncode,
                    IsVerified = 0,
                    Code = _randomCodeGenratorService.GenerateRandomCode(6)
                };

                string messageBody = "Đây là mã xác nhận tài khoản của bạn: " + registerAccount.Code;
                var message = new EmailMessage(registerAccount.Email, "XÁC NHẬN TÀI KHOẢN EMAIL" , messageBody);
                var checkSendEmail = _emailService.SendEmail(message);
                if(!checkSendEmail)
                {
                    return -1;
                }

                _context.Accounts.Add(registerAccount);
                _context.SaveChanges();
                var account = _context.Accounts.FirstOrDefault(x => x.Email == register.Email);
                var profile = _context.Profiles.FirstOrDefault(x => x.AccountId == account.Id);
                profile.FirstName = register.FirstName;
                profile.LastName = register.LastName;
                _context.SaveChanges();

                var accountHasRole = new AccountHasRole();
                var role = _context.Roles.FirstOrDefault(x => x.GuardName.Equals("USER"));
                accountHasRole.RoleId = role.Id;
                accountHasRole.AccountId = account.Id;
                _context.AccountHasRoles.Add(accountHasRole);
                _context.SaveChanges();
                
                return account.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }
    }
}