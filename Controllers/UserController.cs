using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Models.User;
using Covid_Project.Domain.Services.Common;
using Covid_Project.Domain.Services.Communication;
using Covid_Project.Domain.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Covid_Project.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserProfileService _profile;
        private readonly IUserItineraryService _itinerary;
        private readonly ICityService _city;
        private readonly ITestingLocationService _testingLocation;
        private readonly IUserTestingService _userTesting;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IUserProfileService profile,
        IUserItineraryService itinerary,
        ICityService city,
        ITestingLocationService testingLocation,
        IUserTestingService userTesting, IHttpContextAccessor httpContextAccessor)
        {
            _city = city;
            _itinerary = itinerary;
            _profile = profile;
            _testingLocation = testingLocation;
            _userTesting = userTesting;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("profile")]
        public ActionResult GetProfile()
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<ProfileDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var profile = _profile.GetProfile(Int32.Parse(accountId));
            if (profile == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Không tìm thấy.";
                return BadRequest(response);
            }
            response.Data = profile;
            response.Message = "Lấy thông tin người dùng thành công.";
            return Ok(response);
        }

        [HttpPut("profile")]
        public ActionResult EditProfile([FromBody] ProfileDto profile)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<ProfileDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var returnedProfile = _profile.EditProfile(Int32.Parse(accountId), profile);
            if (returnedProfile == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Thông tin chỉnh sửa không hợp lệ.";
                return BadRequest(response);
            }
            response.Data = returnedProfile;
            response.Message = "Chỉnh sửa thông tin cá nhân thành công.";
            return Ok(response);
        }
        [HttpPut("profile/password")]
        public ActionResult ChangePassword([FromBody] ChangePasswordDto passwordDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<bool>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var isSuccess = _profile.ChangePassword(Int32.Parse(accountId), passwordDto.OldPassword, passwordDto.NewPassword);
            if (isSuccess == false)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Mật khẩu không chính xác.";
                return BadRequest(response);
            }
            response.Data = isSuccess;
            response.Message = "Đổi mật khẩu thành công.";
            return Ok(response);
        }

        [HttpGet("itinerary")]
        public ActionResult GetItineraries([FromQuery] PaginationFilter filter)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<PageResponse<List<UserItineraryDto>>>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var itineraries = _itinerary.GetItineraries(Int32.Parse(accountId), filter);
            if (itineraries == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Không tìm thấy.";
                return BadRequest(response);
            }
            itineraries.Message = "Lấy danh sách lịch trình thành công.";
            return Ok(itineraries);
        }

        [HttpPost("itinerary")]
        public ActionResult AddItinerary([FromBody] ItineraryModel itinerary)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<ItineraryModel>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var itineraryAdded = _itinerary.AddItinerary(Int32.Parse(accountId), itinerary);
            if (itineraryAdded == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Thông tin lịch trình không hợp lệ.";
                return BadRequest(response);
            }
            response.Message = "Thêm lịch trình thành công.";
            response.Data = itinerary;
            return Ok(response);
        }

        [HttpPut("itinerary/{itineraryId}")]
        public ActionResult EditItinerary(int itineraryId, [FromBody] ItineraryModel itinerary)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<ItineraryModel>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }

            var itineraryEditted = _itinerary.EditItinerary(Int32.Parse(accountId), itineraryId, itinerary);
            if(itineraryEditted == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Thông tin lịch trình không hợp lệ.";
                return BadRequest(response);
            }

            response.Message = "Chỉnh sửa lịch trình thành công.";
            response.Data = itineraryEditted;
            return Ok(response);
        }

        [HttpDelete("itinerary/{itineraryId}")]
        public ActionResult DeleteItinerary(int itineraryId)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<bool>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }

            var isItineraryDeleted = _itinerary.DeleteItinerary(Int32.Parse(accountId), itineraryId);
            response.Data = isItineraryDeleted;
            if(isItineraryDeleted == true)
            {
                response.Message = "Xóa lịch trình thành công.";
                return Ok(response); 
            }

            response.IsSuccess = false;
            response.Message = "Xóa lịch trình không thành công.";
            return BadRequest(response);
        }
        [HttpPost("itinerary/location-checkin")]
        public ActionResult CheckinLocation([FromBody] LocationCheckinDto locationCheckinDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<LocationCheckinDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var locationCheckinRes = _itinerary.CheckinLocation(Int32.Parse(accountId), locationCheckinDto);
            if (locationCheckinRes == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Checkin không thành công.";
                return BadRequest(response);
            }
            response.Message = "Checkin thành công.";
            response.Data = locationCheckinRes;
            return Ok(response);
        }

        [HttpGet("itinerary/location-checkin")]
        public ActionResult GetListLocationCheckin([FromQuery] PaginationFilter filter)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<PageResponse<List<LocationCheckinDto>>>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var listLocation = _itinerary.GetListLocationCheckin(Int32.Parse(accountId), filter);
            if(listLocation == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Không tìm thấy người dùng";
                return Unauthorized(response);
            }
            listLocation.Message = "Lấy danh sách địa điểm check-in thành công.";
            return Ok(listLocation);
        }

        [HttpDelete("itinerary/location-checkin/{locationId}")]
        public ActionResult DeleteLocationCheckin(int locationId)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<bool>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var isDeleted = _itinerary.DeleteLocationCheckin(Int32.Parse(accountId), locationId);
            response.Data = isDeleted;
            if(isDeleted == true)
            {
                response.Message = "Xóa địa điểm check-in thành công.";
                return Ok(response);
            }
            response.Code = 404;
            response.IsSuccess = false;
            response.Message = "Xóa địa điểm check-in thất bại.";
            return BadRequest(response);
        }

        [HttpPut("itinerary/location-checkin/{locationId}")]
        public ActionResult EditLocationCheckin(int locationId, [FromBody] LocationCheckinDto locationDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<LocationCheckinDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var locationRet = _itinerary.EditLocationCheckin(Int32.Parse(accountId), locationId, locationDto);
            if(locationRet == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Cập nhật thông tin thất bại.";
                return BadRequest(response);
            }
            response.Data = locationRet;
            response.Message = "Cập nhật thông tin thành công.";
            return Ok(response);

        }

        [HttpPost("testing/register")]
        public ActionResult RegisterTesting([FromBody] TestingRegisterDto register)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<TestingResultDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var testing = _userTesting.RegisterTesting(Int32.Parse(accountId), register);
            if (testing == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Đã đầy chỗ, vui lòng chọn thời gian hoặc địa điểm xét nghiệm khác.";
                return BadRequest(response);
            }
            response.Message = "Đăng ký thành công.";
            response.Data = testing;
            return Ok(response);
        }
        [HttpGet("testing/invaliddate/{testingLocationId}")]
        public ActionResult GetInvalidTestingDate(int testingLocationId)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<List<DateTime>>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var invalidDates = _userTesting.GetInvalidTestingDate(Int32.Parse(accountId), testingLocationId);
            if (invalidDates == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return BadRequest(response);
            }
            response.Message = "Lấy danh sách thành công.";
            response.Data = invalidDates;
            return Ok(response);
        }
        [HttpPost("testing/medicalinfo")]
        public ActionResult MedicalInfoRegister([FromBody] MedicalInfoDto medicalInfoDto)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<MedicalInfoDto>();
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.Code = 400;
                response.Message = "Thông tin y tế không hợp lệ.";
                return BadRequest(response);
            }
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var medicalInfoRes = _userTesting.MedicalInfoRegister(Int32.Parse(accountId), medicalInfoDto);
            if (medicalInfoRes == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không tồn tại.";
                return Unauthorized(response);
            }
            response.Message = "Lưu thông tin y tế thành công.";
            response.Data = medicalInfoRes;
            return Ok(response);
        }
        [HttpGet("testing")]
        public ActionResult GetListTesting([FromQuery] PaginationFilter filter)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<List<TestingResultDto>>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var listTestings = _userTesting.GetListTesting(Int32.Parse(accountId), filter);
            if (listTestings == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Không tìm thấy người dùng";
                return NotFound(response);
            }
            listTestings.Message = "Lấy danh sách xét nghiệm thành công";
            return Ok(listTestings);
        }
        [HttpGet("profile/avatar")]
        public ActionResult Getavatar()
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<string>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var avatar = _profile.GetAvatar(Int32.Parse(accountId));
            if(avatar == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Không tìm thấy ảnh đại diện.";
                return NotFound(response);
            }
            response.Data = "https://" + _httpContextAccessor.HttpContext.Request.Host.Value + avatar;
            response.Message = "Lấy ảnh đại diện thành công.";
            return Ok(response);
        }

        [HttpPost("profile/avatar")]
        public ActionResult PostAvatar([FromForm(Name = "avatar")] IFormFile avatar)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<string>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Phiên đăng nhập hết hạn.";
                return Unauthorized(response);
            }
            var avatarRet = _profile.PostAvatar(Int32.Parse(accountId), avatar);
            if(avatarRet == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Thay đổi ảnh đại diện thất bại.";
                return BadRequest(response);
            }
            response.Data = avatarRet;
            response.Message = "Thay đổi ảnh đại diện thành công.";
            return Ok(response);
        }
    }
}