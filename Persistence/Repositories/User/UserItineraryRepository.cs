using System;
using System.Collections.Generic;
using System.Linq;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.User;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Domain.Repositories.User;
using Covid_Project.Domain.Services.Communication;
using Covid_Project.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Covid_Project.Persistence.Repositories.User
{
    public class UserItineraryRepository : IUserItineraryRepository
    {
        const int MINIMUM_REQUIRE_DAY = 21;
        private readonly AppDbContext _context;
        public UserItineraryRepository(AppDbContext context)
        {
            _context = context;
        }
        public ItineraryModel AddItinerary(int accountId, ItineraryModel itinerary)
        {
            try
            {
                var current = DateTime.UtcNow.AddHours(7);
                if (itinerary.LandingTime < current)
                {
                    return null;
                }

                var isValidTime = false;
                var userItineraries = _context.Itineraries
                .Where(x => x.AccountId == accountId && x.IsDeleted == false)
                .OrderByDescending(x => x.LandingTime)
                .ToList();

                var listDepartureTime = new List<DateTime>();
                var listLandingTime = new List<DateTime>();

                foreach (var userItinerary in userItineraries)
                {
                    listDepartureTime.Add(userItinerary.DepartureTime);
                    listLandingTime.Add(userItinerary.LandingTime);
                }
                if (userItineraries.Count == 0)
                {
                    isValidTime = true;
                }
                else if (itinerary.DepartureTime >= listLandingTime[0] || itinerary.LandingTime <= listDepartureTime[listDepartureTime.Count - 1])
                {
                    isValidTime = true;
                }
                else
                {
                    int index = 0;
                    while (listDepartureTime[index] >= itinerary.LandingTime)
                    {
                        index++;
                    }
                    if (listLandingTime[index] <= itinerary.DepartureTime)
                    {
                        isValidTime = true;
                    }
                }
                if (isValidTime == false)
                {
                    return null;
                }

                var departureCity = _context.Cities.FirstOrDefault(x => x.Id == itinerary.DepartureCityId && x.IsDeleted == false);
                var destinationCity = _context.Cities.FirstOrDefault(x => x.Id == itinerary.DestinationCityId && x.IsDeleted == false);
                if (departureCity == null || destinationCity == null)
                {
                    return null;
                }
                var itineraryAdded = new Itinerary()
                {
                    AccountId = accountId,
                    DepartureCity = departureCity,

                    DestinationCity = destinationCity,

                    FlyNo = itinerary.FlyNo,
                    DepartureTime = itinerary.DepartureTime,
                    LandingTime = itinerary.LandingTime
                };
                if (departureCity.UpdatedAt.HasValue)
                {
                    var dt = departureCity.UpdatedAt.Value;
                    var duration = itinerary.DepartureTime.Subtract(dt);
                    var days = duration.Days;
                    if(departureCity.Status == 1)
                    {
                        itinerary.MustTesting = true;
                    }
                    else
                    {
                        itinerary.MustTesting = Math.Abs(days) <= MINIMUM_REQUIRE_DAY ? true : false;
                    }
                    
                }
                _context.Itineraries.Add(itineraryAdded);
                _context.SaveChanges();

                return itinerary;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public LocationCheckin CheckinLocation(int accountId, LocationCheckin locationCheckin)
        {
            try
            {
                var account = _context.Accounts.FirstOrDefault(x => x.Id == accountId && x.IsDeleted == false);
                if (account == null)
                {
                    return null;
                }
                locationCheckin.AccountId = accountId;
                _context.LocationCheckins.Add(locationCheckin);
                _context.SaveChanges();
                return locationCheckin;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public bool DeleteCheckinLocation(int locationId)
        {
            try
            {
                var locationCheckin = _context.LocationCheckins
                .FirstOrDefault(x => x.Id == locationId);

                if (locationCheckin == null)
                {
                    return false;
                }
                locationCheckin.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool DeleteItinerary(int itineraryId)
        {
            try
            {
                var itinerary = _context.Itineraries
                .FirstOrDefault(x => x.Id == itineraryId && x.IsDeleted == false);

                if (itinerary == null)
                {
                    return false;
                }

                itinerary.IsDeleted = true;
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public ItineraryModel EditItinerary(int accountId, int itineraryId, ItineraryModel itinerary)
        {
            try
            {
                var itineraryEditted = _context.Itineraries
                .FirstOrDefault(x => x.Id == itineraryId && x.IsDeleted == false);

                if (itineraryEditted == null)
                {
                    return null;
                }

                var userItineraries = _context.Itineraries
                .Where(x => x.AccountId == accountId && x.IsDeleted == false && x.Id != itineraryId)
                .OrderByDescending(x => x.LandingTime)
                .ToList();

        

                int index = 0;
                var listDepartureTime = new List<DateTime>();
                var listLandingTime = new List<DateTime>();
                bool isValidTime = false;
                if(userItineraries.Count != 0)
                {
                    foreach (var userItinerary in userItineraries)
                    {
                        listDepartureTime.Add(userItinerary.DepartureTime);
                        listLandingTime.Add(userItinerary.LandingTime);
                    }

                    while (listLandingTime[index] > itinerary.DepartureTime)
                    {
                        index++;
                        if(index >= listDepartureTime.Count)
                        {
                            break;
                        }
                    }
                    
                    if (index == 0)
                    {
                        if (itinerary.DepartureTime >= listLandingTime[0])
                        {
                            isValidTime = true;
                        }
                    }
                    else if (itinerary.LandingTime <= listDepartureTime[index - 1])
                    {
                        isValidTime = true;
                    }

                    if (!isValidTime)
                    {
                        return null;
                    }
                }
                

                var departureCity = _context.Cities.FirstOrDefault(x => x.Id == itinerary.DepartureCityId && x.IsDeleted == false);
                var destinationCity = _context.Cities.FirstOrDefault(x => x.Id == itinerary.DestinationCityId && x.IsDeleted == false);

                if (departureCity == null || destinationCity == null)
                {
                    return null;
                }
                itineraryEditted.DepartureCity = departureCity;
                itineraryEditted.DestinationCity = destinationCity;
                itineraryEditted.FlyNo = itinerary.FlyNo;
                itineraryEditted.DepartureTime = itinerary.DepartureTime;
                itineraryEditted.LandingTime = itinerary.LandingTime;

                if (departureCity.UpdatedAt.HasValue)
                {
                    var dt = departureCity.UpdatedAt.Value;
                    var duration = itinerary.DepartureTime.Subtract(dt);
                    var days = duration.Days;
                    if(departureCity.Status == 1)
                    {
                        itinerary.MustTesting = true;
                    }
                    else
                    {
                        itinerary.MustTesting = Math.Abs(days) <= MINIMUM_REQUIRE_DAY ? true : false;
                    }
                }

                itinerary.Id = itineraryEditted.Id;
                _context.SaveChanges();
                return itinerary;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public LocationCheckin EditLocationCheckin(int locationId, LocationCheckin locationCheckin)
        {
            try
            {
                var location = _context.LocationCheckins
                .FirstOrDefault(x => x.Id == locationId && x.IsDeleted == false);
                if (location == null)
                {
                    return null;
                }
                location.Time = locationCheckin.Time;
                location.Address = locationCheckin.Address;
                _context.SaveChanges();
                return location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public List<Itinerary> GetItineraries(int accountId)
        {
            try
            {
                var account = _context.Accounts.FirstOrDefault(x => x.Id == accountId && x.IsDeleted == false);
                if (account == null)
                {
                    return null;
                }
                var listItineraries = _context.Itineraries
                .Include(x => x.DepartureCity)
                .Include(y => y.DestinationCity)
                .Where(z => z.AccountId == account.Id && z.IsDeleted == false)
                .OrderByDescending(x => x.LandingTime)
                .ToList();
                return listItineraries;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public List<LocationCheckin> GetListLocationCheckin(int accountId)
        {
            try
            {
                var account = _context.Accounts.FirstOrDefault(x => x.Id == accountId && x.IsDeleted == false);
                if (account == null)
                {
                    return null;
                }
                return _context.LocationCheckins
                .Where(x => x.AccountId == accountId && x.IsDeleted == false)
                .OrderByDescending(x => x.Time)
                .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}