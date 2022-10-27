using System;
using System.Collections.Generic;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Services.Communication;

namespace Covid_Project.Domain.Services.Admin
{
    public interface IAdminItineraryService
    {
        UserItineraryDto GetUserItinerary(int accountId, string email);
        ItineraryStatisticDto GetItineraryStatistic(int accountId, DateTime startDate, DateTime endDate);
        PageResponse<List<UserItineraryDto>> GetItinerariesByUser(int accountId, string userEmail, PaginationFilter filter);
        List<LocationCheckinDto> GetListLocationCheckinByUser(int accountId, int userId);
    }
}