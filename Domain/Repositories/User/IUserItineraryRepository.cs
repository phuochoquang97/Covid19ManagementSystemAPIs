using System.Collections.Generic;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.User;
using Covid_Project.Domain.Services.Communication;

namespace Covid_Project.Domain.Repositories.User
{
    public interface IUserItineraryRepository
    {
        List<Itinerary> GetItineraries(int accountId);
        ItineraryModel AddItinerary(int accountId, ItineraryModel itineraty); 
        bool DeleteItinerary(int itineraryId);
        ItineraryModel EditItinerary(int accountId, int itineraryId, ItineraryModel itinerary);
        LocationCheckin CheckinLocation(int accountId, LocationCheckin locationCheckin);
        List<LocationCheckin> GetListLocationCheckin(int accountId);
        bool DeleteCheckinLocation(int locationId);
        LocationCheckin EditLocationCheckin(int locationId, LocationCheckin locationCheckin);
    }
}