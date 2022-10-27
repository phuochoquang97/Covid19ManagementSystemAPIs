using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Covid_Project.Domain.Models.Authorazation;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Services.Admin;
using Covid_Project.Domain.Services.Communication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Covid_Project.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminTestingService _adminTesting;
        private readonly IAdminItineraryService _adminItinerary;
        private readonly IAdminUserService _user;
        public AdminController(IAdminTestingService adminTesting, IAdminItineraryService adminItinerary, IAdminUserService user)
        {
            _user = user;
            _adminItinerary = adminItinerary;
            _adminTesting = adminTesting;
        }

        [HttpGet("testing")]
        public ActionResult GetAllTesting([FromQuery] PaginationFilter filter)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<TestingResultDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var testings = _adminTesting.GetAllTesting(Int32.Parse(accountId), filter);
            if (testings == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return BadRequest(response);
            }
            testings.Message = "Lấy danh sách xét nghiệm thành công.";
            return Ok(testings);
        }

        [HttpGet("testing/{testingId}")]
        public ActionResult GetTestingById(int testingId)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<TestingResultDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var testing = _adminTesting.GetTestingById(Int32.Parse(accountId), testingId);
            if (testing == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Không tìm thấy xét nghiệm.";
                return BadRequest(response);
            }
            response.Message = "Lấy thông tin xét nghiệm thành công.";
            response.Data = testing;
            return Ok(response);
        }

        [HttpGet("testing/user/{userAccountId}")]
        public ActionResult GetTestingByUser(int userAccountId, [FromQuery] PaginationFilter filter)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<List<TestingResultDto>>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var testings = _adminTesting.GetTestingByUser(Int32.Parse(accountId), userAccountId, filter);
            if (testings == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Không tìm thấy người dùng.";
                return BadRequest(response);
            }
            testings.Message = "Lấy danh sách xét nghiệm thành công.";
            return Ok(testings);
        }

        [HttpPut("testing/{testingId}")]
        public ActionResult UpdateTesting(int testingId, [FromBody] TestingDto testing)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<TestingDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var testingRes = _adminTesting.UpdateTesting(Int32.Parse(accountId), testingId, testing);
            if (testingRes == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Không tìm thấy xét nghiệm hoặc bạn không được phân quyền";
                return BadRequest(response);
            }
            response.Message = "Cập nhật xét nghiệm thành công.";
            response.Data = testingRes;
            return Ok(response);
        }

        [HttpGet("testing/statistic")]
        public ActionResult GetTestingStatistic([FromQuery] int testingLocationId)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<TestingStatisticDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var testingStatistic = _adminTesting.GetTestingStatistic(Int32.Parse(accountId), testingLocationId);
            if (testingStatistic == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Lấy thông tin thống kê thất bại.";
                return BadRequest(response);
            }
            response.Message = "Lấy thông tin thống kê thành công";
            response.Data = testingStatistic;
            return Ok(response);
        }
        [HttpGet("itinerary/user")]
        public ActionResult GetUserItinerary([FromQuery] string email)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<UserItineraryDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var itinerary = _adminItinerary.GetUserItinerary(Int32.Parse(accountId), email);
            if (itinerary == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Không tìm thấy lịch trình người dùng hoặc bạn không được phân quyền";
                return BadRequest(response);
            }
            response.Message = "Lấy lịch trình người dùng thành công.";
            response.Data = itinerary;
            return Ok(response);
        }

        [HttpGet("itineraries/user")]
        public ActionResult GetUserItineraries([FromQuery] string email, [FromQuery] PaginationFilter filter)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<List<UserItineraryDto>>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var itinerary = _adminItinerary.GetItinerariesByUser(Int32.Parse(accountId), email, filter);
            if (itinerary == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Không tìm thấy lịch trình người dùng hoặc bạn không được phân quyền";
                return BadRequest(response);
            }
            itinerary.Message = "Lấy lịch trình người dùng thành công.";
            return Ok(itinerary);
        }

        
        [HttpGet("itinerary/statistic")]
        public ActionResult GetItineraryStatistic([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<ItineraryStatisticDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var data = _adminItinerary.GetItineraryStatistic(Int32.Parse(accountId), startDate, endDate);
            if (data == null)
            {
                response.Code = 400;
                response.Message = "Thống kê lịch trình thất bại.";
                response.IsSuccess = false;
                return BadRequest(response);
            }
            response.Data = data;
            response.Message = "Lấy thông tin thống kê lịch trình thành công.";
            return Ok(response);
        }
        [HttpGet("user-statistic")]
        public ActionResult GetAllUser([FromQuery] PaginationFilter filter)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<List<ProfileDto>>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var users = _user.GetAllUserProfile(Int32.Parse(accountId), filter);
            if(users == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return NotFound(response);
            }
            users.Message = "Lấy thông tin người dùng thành công.";
            return Ok(users);
        }
        [HttpGet("location-checkin/{userId}")]
        public ActionResult GetLocationCheckinByUser(int userId)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<List<LocationCheckinDto>>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var userLocationCheckin = _adminItinerary.GetListLocationCheckinByUser(Int32.Parse(accountId), userId);
            if(userLocationCheckin == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Lấy thông tin lịch trình người dùng không thành công.";
                return BadRequest(response);
            }
            response.Data = userLocationCheckin;
            response.Message = "Lấy thông tin lịch trình người dùng thành công.";
            return Ok(response);
        }
        
    }
}