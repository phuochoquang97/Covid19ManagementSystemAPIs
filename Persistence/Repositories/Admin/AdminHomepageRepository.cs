using System;
using System.IO;
using System.Linq;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Repositories.Admin;
using Covid_Project.Persistence.Context;
using Microsoft.AspNetCore.Http;

namespace Covid_Project.Persistence.Repositories.Admin
{
    public class AdminHomepageRepository : IAdminHomepageRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminHomepageRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public News AddNews(News news)
        {
            try
            {
                _context.News.Add(news);
                _context.SaveChanges();
                return news;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool DeleteNews(int newsId)
        {
            try
            {
                var news = _context.News
                .FirstOrDefault(x => x.Id == newsId && x.IsDeleted == false);

                if (news == null)
                {
                    return false;
                }

                news.IsDeleted = true;
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public News EditNews(int newsId, News news)
        {
            try
            {
                var newsEditted = _context.News.Where(s => s.IsDeleted == false)
                .FirstOrDefault(x => x.Id == newsId);

                if (newsEditted == null)
                {
                    return null;
                }

                newsEditted.Title = news.Title;
                newsEditted.Content = news.Content;

                _context.SaveChanges();
                return newsEditted;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public string PostNewsImage(int newsId, IFormFile newsImg)
        {
            try
            {
                if (CheckFile(newsImg))
                {
                    return WriteFile(newsId, newsImg);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        private bool CheckFile(IFormFile newsImg)
        {
            var fileName = newsImg.FileName;
            string ext = Path.GetExtension(fileName);
            if (ext.Equals(".png") || ext.Equals(".jpg") || ext.Equals(".jpeg"))
            {
                return true;
            }
            return false;
        }

        private string WriteFile(int newsId, IFormFile newsImg)
        {
            try
            {
                var news = _context.News
                .FirstOrDefault(x => x.Id == newsId && x.IsDeleted == false);
                if(news == null)
                {
                    return null;
                }

                if (news.Image != null)
                {
                    var link = Path.Combine(Directory.GetCurrentDirectory(), "Image", "News", news.Image.Split("/").Last());
                    File.Delete(link);
                }

                var ext = Path.GetExtension(newsImg.FileName);
                string fileName = $"news{newsId}" + ext;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Image", "News", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    newsImg.CopyTo(bits);
                    news.Image = Path.Combine("https://" + _httpContextAccessor.HttpContext.Request.Host.Value, "Image", "News", fileName);
                    _context.SaveChanges();
                }
                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}