using Covid_Project.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Covid_Project.Domain.Repositories.Admin
{
    public interface IAdminHomepageRepository
    {
        News AddNews(News news);
        bool DeleteNews(int newsId);
        News EditNews(int newsId, News news);
        string PostNewsImage(int newsId, IFormFile newsImg);
    }
}