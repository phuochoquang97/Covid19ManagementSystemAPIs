using System.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Repositories.Admin;
using Covid_Project.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Covid_Project.Persistence.Repositories.Admin
{
    public class AdminItineraryRepository : IAdminItineraryRepository
    {
        private readonly AppDbContext _context;
        public AdminItineraryRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Itinerary> GetAllUserItinerary(string email)
        {
            try
            {
                var account = _context.Accounts.Where(s => s.IsDeleted == false).FirstOrDefault(x => x.Email.Equals(email));
                if(account == null)
                {
                    return null;
                }
                var itinerary = _context.Itineraries
                .Include(x => x.DepartureCity)
                .Include(y => y.DestinationCity)
                .Where(z => z.AccountId == account.Id && z.IsDeleted == false)
                .OrderBy(x => x.CreatedAt)
                .ToList();
                return itinerary;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ItineraryStatisticDto GetItineraryStatistic(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dataQuery = _context.Itineraries.Where(x => x.IsDeleted == false && x.LandingTime >= startDate && x.LandingTime <= endDate)
                    .Include(x => x.DestinationCity)
                    .GroupBy(p => p.DestinationCity.Name)
                    .Select(g => new { destination = g.Key, count = g.Count() });
                
                IDictionary<string, int> data = new Dictionary<string, int>();
                foreach(var record in dataQuery)
                {
                    data.Add(record.destination.ToString(), record.count);
                }

                var itineraryStatistic = new ItineraryStatisticDto();
                itineraryStatistic.StartDate = startDate;
                itineraryStatistic.EndDate = endDate;
                itineraryStatistic.Data = data;

                return itineraryStatistic;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

            
        }

        public Itinerary GetUserItinerary(string email)
        {
            try
            {
                var account = _context.Accounts.Where(s => s.IsDeleted == false).FirstOrDefault(x => x.Email.Equals(email));
                if(account == null)
                {
                    return null;
                }
                var itinerary = _context.Itineraries
                .Include(x => x.DepartureCity)
                .Include(y => y.DestinationCity)
                .OrderByDescending(z => z.CreatedAt)
                .FirstOrDefault(z => z.AccountId == account.Id && z.IsDeleted == false);
                return itinerary;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}