using System.Collections.Generic;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Services.Communication;
using Microsoft.AspNetCore.Http;

namespace Covid_Project.Domain.Repositories.Homepage
{
    public interface IHomepageRepository
    {
        PageResponse<List<News>> GetListNews(PaginationFilter filter);
        News GetNewsById(int id);
        List<News> GetAllNews();
        
    }
}