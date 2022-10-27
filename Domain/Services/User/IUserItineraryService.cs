using System.Collections.Generic;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Models.User;
using Covid_Project.Domain.Services.Communication;

namespace Covid_Project.Domain.Services.User
{
    public interface IUserItineraryService
    {
        PageResponse<List<UserItineraryDto>> GetItineraries(int accountId, PaginationFilter filter);
        ItineraryModel AddItinerary(int accountId, ItineraryModel itinerary);
        bool DeleteItinerary(int accountId, int itineraryId);
        ItineraryModel EditItinerary(int accountId, int itineraryId, ItineraryModel itinerary);
        LocationCheckinDto CheckinLocation(int accountId, LocationCheckinDto locationCheckinDto);
        PageResponse<List<LocationCheckinDto>> GetListLocationCheckin(int accountId, PaginationFilter filter);
        bool DeleteLocationCheckin(int accountId, int locationId);
        LocationCheckinDto EditLocationCheckin(int accountId, int locationId, LocationCheckinDto locationCheckinDto);
    }
}