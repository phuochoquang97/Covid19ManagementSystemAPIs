using Covid_Project.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Covid_Project.Domain.Repositories.Admin
{
    public interface IAdminHomePageService
    {
        News AddNews(int accountId, News news);
        bool DeleteNews(int accountId, int newsId);
        News EditNews(int accountId, int newsId, News news);
        string PostNewsImage(int accountId, int newsId, IFormFile newsImg);
    }
}