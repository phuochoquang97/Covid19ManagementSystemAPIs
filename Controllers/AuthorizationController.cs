using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid_Project.Domain.Models.Authorazation;
using Covid_Project.Domain.Services.Authorization;
using Covid_Project.Domain.Services.Communication;
using Covid_Project.Domain.Services.Confirmation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Covid_Project.Domain.Models.DTOs;

namespace Covid_Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IRegisterService _register;
        private readonly ILoginService _login;
        private readonly IEmailConfirmationService _emailConfirmation;
        public AuthorizationController(IRegisterService register, ILoginService login, IEmailConfirmationService emailConfirmation)
        {
            _emailConfirmation = emailConfirmation;
            _login = login;
            _register = register;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterModel register)
        {
            var res = new RegisterResponse();
            bool checkAccountExistence = _register.checkAccountExistence(register.Email);
            if(checkAccountExistence == true)
            {
                var message = new BaseResponse(400, "Email đã tồn tại.");
                return BadRequest(message);
            } 
            int checkRegister = _register.Register(register);
            if(checkRegister == -1)
            {
                res.IsSuccess = false;
                return BadRequest(res);
            }
            res.AccountId = checkRegister;

            return Ok(res);
        }
        // tra ve account id
        [AllowAnonymous]
        [HttpPost("admin/login")]
        public ActionResult LoginAdmin([FromBody] LoginModel loginModel)
        {
            var account = _login.Login(loginModel.Email, loginModel.Password);
            var response = new Response<LoginResponse>();
            var checkRole = false;
            
            if (account != null )
            {
                foreach(var role in account.Roles)
                {
                    if(role.GuardName.Contains("ADMIN"))
                    {
                        checkRole = true;
                        break;
                    }
                }
                if(checkRole)
                {
                    response.Data = account;
                    response.Message = "Đăng nhập thành công.";
                    return Ok(response);
                }
                
            }
            response.IsSuccess = false;
            response.Message = "Email hoặc mật khẩu không hợp lệ.";
            response.Code = 400;
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost("user/login")]
        public ActionResult LoginUser([FromBody] LoginModel loginModel)
        {
            var account = _login.Login(loginModel.Email, loginModel.Password);
            var response = new Response<LoginResponse>();
            var checkRole = false;
            
            if (account != null )
            {
                foreach(var role in account.Roles)
                {
                    if(role.GuardName.Contains("USER"))
                    {
                        checkRole = true;
                        break;
                    }
                }
                if(checkRole)
                {
                    response.Data = account;
                    response.Message = "Đăng nhập thành công.";
                    return Ok(response);
                }
                
            }
            response.IsSuccess = false;
            response.Message = "Email hoặc mật khẩu không hợp lệ.";
            response.Code = 400;
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost("email")]
        public ActionResult ConfirmEmail([FromBody] ConfirmationModel confirmation)
        {
            var checkCode = _emailConfirmation.ConfirmEmail(confirmation.Email, confirmation.Code);
            var response = new Response<string>("");
            response.Data = null;
            if(checkCode == true)
            {
                response.Message = "Xác nhận email thành công";
                return Ok(response);
            }
            response.IsSuccess = false;
            response.Message = "Mã xác nhận không hợp lệ.";
            response.Code = 400;
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost("forgotpassword/code")]
        public ActionResult ForgotPasswordCode([FromBody] ConfirmationModel email)
        {
            var response = new Response<string>("");
            var checkAccountExistence = _register.checkAccountExistence(email.Email);
            if(checkAccountExistence)
            {
                _register.ForgotPasswordCode(email.Email);
                response.Message = "Hệ thống đã gửi mã khôi phục mật khẩu đến Email của bạn.";
                return Ok(response);
            }
            else
            {
                response.Code = 404;
                response.Message = "Tài khoản không tồn tại.";
                response.IsSuccess = false;
            }
            return NotFound(response);
        }

        [HttpPost("forgotpassword")]
        public ActionResult ForgotPasswordCode([FromBody] ForgotPasswordDto forgotPassword)
        {
            var response = new Response<string>("");
            if(!ModelState.IsValid)
            {
                response.Message = "Thông tin lấy lại mật khẩu không hợp lệ.";
                response.Code = 400;
                response.IsSuccess = false;
                return BadRequest(response);
            }
            var check = _register.ForgotPassword(forgotPassword.Email, forgotPassword.Code, forgotPassword.Password);
            if(check)
            {
                response.Message = "Đổi mật khẩu thành công";
                return Ok(response);
            }
            response.Message = "Không tìm thấy người dùng hoặc mã xác nhận không chính xác.";
            response.IsSuccess = false;
            response.Code = 404;
            return NotFound(response);
        }
    }
}