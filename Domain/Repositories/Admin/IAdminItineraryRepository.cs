using System;
using System.Collections.Generic;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;

namespace Covid_Project.Domain.Repositories.Admin
{
    public interface IAdminItineraryRepository
    {
        Itinerary GetUserItinerary(string email);
        ItineraryStatisticDto GetItineraryStatistic(DateTime startDate, DateTime endDate);
        List<Itinerary> GetAllUserItinerary(string email);

    }
}