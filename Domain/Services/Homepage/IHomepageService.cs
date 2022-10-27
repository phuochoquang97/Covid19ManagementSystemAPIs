using System.Net;
using System.Collections.Generic;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Services.Communication;
using Newtonsoft.Json.Linq;

namespace Covid_Project.Domain.Services.Homepage
{
    public interface IHomepageService
    {
        PageResponse<List<News>> GetListNews(PaginationFilter filter);
        News GetNewsById(int id);
        List<News> GetAllNews();
        JObject GetMedicalInfoStatistic();
        
    }
}