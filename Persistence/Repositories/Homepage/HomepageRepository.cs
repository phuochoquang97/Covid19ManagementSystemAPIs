using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Repositories.Homepage;
using Covid_Project.Domain.Services.Communication;
using Covid_Project.Persistence.Context;
using Microsoft.AspNetCore.Http;

namespace Covid_Project.Persistence.Repositories.Homepage
{
    public class HomepageRepository : IHomepageRepository
    {
        private readonly AppDbContext _context;
        public HomepageRepository(AppDbContext context)
        {
            _context = context;

        }
        public PageResponse<List<News>> GetListNews(PaginationFilter filter)
        {
            try
            {
                List<News> listNews = _context.News.Where(x => x.IsDeleted == false).ToList();
                var listNewsResponse = listNews
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
                if(listNewsResponse.Count == 0)
                {
                    return null;
                }
                var response = new PageResponse<List<News>>(listNewsResponse, filter.PageNumber, filter.PageSize);
                response.TotalRecords = listNews.Count();
                response.TotalPages = response.CalTotalPages(listNews.Count(), filter.PageSize);

                return response;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Connecting to DB failed.");
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public News GetNewsById(int id)
        {
            try
            {
                return _context.News.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Connecting to DB failed.");
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public List<News> GetAllNews()
        {
            try
            {
                return _context.News
                .Where(x => x.IsDeleted == false)
                .ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


    }
}