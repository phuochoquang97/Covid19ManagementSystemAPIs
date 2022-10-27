using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Repositories.Admin;
using Covid_Project.Domain.Services.Communication;
using Covid_Project.Domain.Services.Homepage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace Covid_Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomepageController : ControllerBase
    {
        private readonly IHomepageService _homepageService;
        private readonly IAdminHomePageService _adminHomepage;
        public HomepageController(IHomepageService homepageService, IAdminHomePageService adminHomepage)
        {
            _adminHomepage = adminHomepage;
            _homepageService = homepageService;

        }
        [AllowAnonymous]
        [HttpGet("news")]
        public ActionResult GetListNews([FromQuery] PaginationFilter filter)
        {
            var listNews = _homepageService.GetListNews(filter);
            if (listNews != null)
            {
                return Ok(listNews);
            }
            var response = new Response<List<News>>();
            response.IsSuccess = false;
            response.Code = 404;
            response.Message = "Không tìm thấy.";
            return NotFound(response);
        }

        [AllowAnonymous]
        [HttpGet("news/{id}")]
        public ActionResult GetNewsById(int id)
        {
            var news = _homepageService.GetNewsById(id);
            var response = new Response<News>(news);
            if (news != null)
            {
                response.Message = "Thành công.";
                return Ok(response);
            }
            response.Message = "Không tìm thấy.";
            response.Code = 404;
            response.IsSuccess = false;
            return NotFound(response);
        }

        [AllowAnonymous]
        [HttpGet("allnews")]
        public ActionResult GetAllNews()
        {
            var listNews = _homepageService.GetAllNews();
            var response = new Response<List<News>>();

            if (listNews != null)
            {
                response.Message = "Lấy danh sách thành công.";
                response.Data = listNews;
                return Ok(response);
            }
            response.IsSuccess = false;
            response.Code = 404;
            response.Message = "Không tìm thấy.";
            return NotFound(response);
        }

        [Authorize]
        [HttpPost("news")]
        public ActionResult AddNews([FromBody] News news)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<News>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }

            var newsRet = _adminHomepage.AddNews(Int32.Parse(accountId), news);
            if(newsRet == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Thêm bản tin không thành công.";
                return BadRequest(response);
            }
            response.Data = newsRet;
            response.Message = "Thêm bản tin thành công.";
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("news/{newsId}")]
        public ActionResult DeleteNew(int newsId)
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
            response.IsSuccess = _adminHomepage.DeleteNews(Int32.Parse(accountId), newsId);
            if(response.IsSuccess == true)
            {
                response.Data = true;
                response.Message = "Xóa bản tin thành công.";
                return Ok(response);
            }

            response.Code = 400;
            response.Message = "Xóa bản tin không thành công.";
            return BadRequest(response); 
        }

        [Authorize]
        [HttpPut("news/{newsId}")]
        public ActionResult EditNews(int newsId, [FromBody] News news)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<News>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }

            var newsRet = _adminHomepage.EditNews(Int32.Parse(accountId), newsId, news);

            if(newsRet == null)
            {
                response.IsSuccess = false;
                response.Code = 400;
                response.Message = "chỉnh sửa bản tin không thành công.";
                return BadRequest(response);
            }

            response.Data = newsRet;
            response.Message = "Chỉnh sửa bản tin thành công.";
            return Ok(response);
        }

        [Authorize]
        [HttpPost("news/image/{newsId}")]
        public ActionResult PostNewsImage(int newsId, [FromForm(Name = "newsImg")] IFormFile newsImg)
        {
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = new Response<string>();
            if (accountId == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "Người dùng không được phân quyền.";
                return Unauthorized(response);
            }

            var img = _adminHomepage.PostNewsImage(Int32.Parse(accountId), newsId, newsImg);
            if(img == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Upload ảnh không thành công. Lưu ý ảnh phải lưu dưới định dạng .jpg hoặc .png.";
                return BadRequest(response);
            }
            response.Data = img;
            response.Message = "Upload ảnh thành công.";
            return Ok(response);

        }

        [AllowAnonymous]
        [HttpGet("medical-statistic")]
        public ActionResult GetMedicalInfoStatistic(){
            var medicalInfoStatistic = _homepageService.GetMedicalInfoStatistic();
            var response = new Response<JObject>(medicalInfoStatistic);
            if (medicalInfoStatistic != null)
            {
                response.Message = "Thành công.";
                return Ok(response);
            }
            response.Message = "Không tìm thấy.";
            response.Code = 404;
            response.IsSuccess = false;
            return NotFound(response);
        }
    }
}