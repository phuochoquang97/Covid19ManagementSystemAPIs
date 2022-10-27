using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Services.Common;
using Covid_Project.Domain.Services.Communication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Covid_Project.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ITestingLocationService _testingLocation;
        private readonly ICityService _city;
        public CommonController(ICityService city, ITestingLocationService testingLocation)
        {
            _city = city;
            _testingLocation = testingLocation;
        }
        [HttpGet("/city")]
        public ActionResult GetListCity()
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<List<CityDto>>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return BadRequest(response);
            }
            var cities = _city.GetListCity(Int32.Parse(accountId));

            response.Data = cities;
            response.Message = "Lấy danh sách thành phố thành công.";
            return Ok(response);
        }
        [HttpGet("/city/{cityId}")]
        public ActionResult GetCityById(int cityId)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<CityDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return BadRequest(response);
            }
            var city = _city.GetCityById(Int32.Parse(accountId), cityId);
            if (city == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Thành phố không tồn tại.";
                return NotFound(response);
            }
            response.Data = city;
            response.Message = "Lấy thông tin thành phố thành công.";
            return Ok(response);
        }
        [HttpPut("/city/{cityId}")]
        public ActionResult UpdateCity(int cityId, [FromBody] CityDto city)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<CityDto>();
            if(!city.Status.Equals("Positive") && !city.Status.Equals("Negative"))
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Trạng thái thành phố chỉ nhận Positive hoặc Negative";
                return BadRequest(response);
            }
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return BadRequest(response);
            }
            var cityRes = _city.UpdateCity(Int32.Parse(accountId), cityId, city);
            if (cityRes == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Thành phố không tồn tại.";
                return NotFound(response);
            }
            response.Data = cityRes;
            response.Message = "Cập nhật thông tin thành phố thành công.";
            return Ok(response);
        }
        [HttpPost("/city")]
        public ActionResult AddCity([FromBody] CityDto city)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<CityDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return BadRequest(response);
            }
            var cityRes = _city.AddCity(Int32.Parse(accountId), city);
            if (cityRes == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Thêm thành phố không thành công.";
                return BadRequest(response);
            }
            response.Data = cityRes;
            response.Message = "Thêm thành phố thành công.";
            return Ok(response);
        }

        [HttpDelete("/city/{cityId}")]
        public ActionResult DeleteCity(int cityId)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<string>("");
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return BadRequest(response);
            }
            var cityRes = _city.DeleteCity(Int32.Parse(accountId), cityId);
            if (cityRes == false)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Thành phố không tồn tại.";
                return NotFound(response);
            }
            response.Message = "Xóa thành phố thành công.";
            return Ok(response);
        }

        [HttpGet("/testing/location")]
        public ActionResult GetListTestingLocation()
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<List<TestingLocationDto>>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var listLocation = _testingLocation.GetListLocation(Int32.Parse(accountId));
            response.Data = listLocation;
            response.Message = "Lấy danh sách địa điểm xét nghiệm thành công.";
            return Ok(response);
        }
        [HttpGet("/testing/location/{locationId}")]
        public ActionResult GetTestingLocation(int locationId)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<TestingLocationDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var location = _testingLocation.GetLocationById(Int32.Parse(accountId), locationId);
            response.Data = location;

            if (location == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Địa điểm xét nghiệm không tồn tại.";
                return NotFound(response);
            }
            response.Message = "Lấy địa điểm xét nghiệm thành công.";
            return Ok(response);
        }
        [HttpPut("/testing/location/{locationId}")]
        public ActionResult UpdateTestingLocation(int locationId, [FromBody] TestingLocationDto location)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<TestingLocationDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var locationRes = _testingLocation.UpdateLocation(Int32.Parse(accountId), locationId, location);
            response.Data = locationRes;

            if (locationRes == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            response.Message = "Cập nhật địa điểm xét nghiệm thành công.";
            return Ok(response);
        }
        [HttpPost("/testing/location")]
        public ActionResult AddTestingLocation([FromBody] TestingLocationDto location)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<TestingLocationDto>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            var locationRes = _testingLocation.AddLocation(Int32.Parse(accountId), location);
            response.Data = locationRes;

            if (locationRes == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            response.Message = "Thêm địa điểm xét nghiệm thành công.";
            return Ok(response);
        }

        [HttpDelete("/testing/location/{locationId}")]
        public ActionResult DeleteTestingLocation(int locationId)
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
            var locationRes = _testingLocation.DeleteLocation(Int32.Parse(accountId), locationId);
            response.Data = locationRes;

            if (locationRes == false)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }
            response.Message = "Xóa địa điểm xét nghiệm thành công.";
            return Ok(response);
        }
    }
}