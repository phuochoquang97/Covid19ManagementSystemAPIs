using System.Net.Sockets;
using System.Collections.Generic;
using AutoMapper;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Models.User;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Domain.Repositories.User;
using Covid_Project.Domain.Services.Communication;
using Covid_Project.Domain.Services.User;
using System.Linq;
using Covid_Project.Domain.Repositories.Common;

namespace Covid_Project.Services.User
{
    public class UserItineraryService : IUserItineraryService
    {
        private readonly IUserItineraryRepository _userItinerary;
        private readonly IRoleRepository _role;
        private readonly IPermissionRepository _permission;
        private readonly IMapper _mapper;
        private readonly ICityRepository _city;
        public UserItineraryService(IUserItineraryRepository userItinerary, IRoleRepository role, IPermissionRepository permission, IMapper mapper, ICityRepository city)
        {
            _city = city;
            _mapper = mapper;
            _permission = permission;
            _role = role;
            _userItinerary = userItinerary;
        }
        public ItineraryModel AddItinerary(int accountId, ItineraryModel itinerary)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("USER"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("CREATE"))
            {
                return null;
            }
            return _userItinerary.AddItinerary(accountId, itinerary);
        }

        public LocationCheckinDto CheckinLocation(int accountId, LocationCheckinDto locationCheckinDto)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("USER"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("CREATE"))
            {
                return null;
            }
            var locationCheckin = _mapper.Map<LocationCheckin>(locationCheckinDto);
            var locationCheckinRes = _userItinerary.CheckinLocation(accountId, locationCheckin);
            if (locationCheckinRes == null)
            {
                return null;
            }
            return _mapper.Map<LocationCheckinDto>(locationCheckinRes);
        }

        public bool DeleteItinerary(int accountId, int itineraryId)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("USER"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return false;
            }
            if (!_permission.GetListPermission(roles).Contains("UPDATE"))
            {
                return false;
            }

            return _userItinerary.DeleteItinerary(itineraryId);
        }

        public bool DeleteLocationCheckin(int accountId, int locationId)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("USER"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return false;
            }
            if (!_permission.GetListPermission(roles).Contains("UPDATE"))
            {
                return false;
            }

            return _userItinerary.DeleteCheckinLocation(locationId);
        }

        public ItineraryModel EditItinerary(int accountId, int itineraryId, ItineraryModel itinerary)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("USER"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("UPDATE"))
            {
                return null;
            }

            var itineraryEditted = _userItinerary.EditItinerary(accountId, itineraryId, itinerary);

            if (itineraryEditted == null)
            {
                return null;
            }
            
            return itineraryEditted;
        }

        public LocationCheckinDto EditLocationCheckin(int accountId, int locationId, LocationCheckinDto locationCheckinDto)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("USER"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("UPDATE"))
            {
                return null;
            }
            var locationEdittedModel = _mapper.Map<LocationCheckin>(locationCheckinDto);
            var locationEditted = _userItinerary.EditLocationCheckin(locationId, locationEdittedModel);
            if (locationEditted == null)
            {
                return null;
            }
            return _mapper.Map<LocationCheckinDto>(locationEditted);
        }

        public PageResponse<List<UserItineraryDto>> GetItineraries(int accountId, PaginationFilter filter)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("USER"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("READ"))
            {
                return null;
            }
            var userItineraries = _userItinerary.GetItineraries(accountId);

            var userItinerariesMapped = _mapper.Map<List<UserItineraryDto>>(userItineraries);
            for (int i = 0; i < userItineraries.Count; i++)
            {
                userItinerariesMapped[i].DepartureCityId = userItineraries[i].DepartureCityId;
                userItinerariesMapped[i].DestinationCityId = userItineraries[i].DestinationCityId;
                userItinerariesMapped[i].Departure = userItineraries[i].DepartureCity.Name;
                userItinerariesMapped[i].Destination = userItineraries[i].DestinationCity.Name;
                userItinerariesMapped[i].DepartureTime = userItineraries[i].DepartureTime;
                userItinerariesMapped[i].LandingTime = userItineraries[i].LandingTime;
                userItinerariesMapped[i].TravelNo = userItineraries[i].FlyNo;
            }
            var userItinerariesRepsponse = userItinerariesMapped
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

            var response = new PageResponse<List<UserItineraryDto>>(userItinerariesRepsponse, filter.PageNumber, filter.PageSize);
            response.TotalRecords = userItineraries.Count();
            response.TotalPages = response.CalTotalPages(userItineraries.Count(), filter.PageSize);

            return response;

        }

        public PageResponse<List<LocationCheckinDto>> GetListLocationCheckin(int accountId, PaginationFilter filter)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("USER"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("READ"))
            {
                return null;
            }
            var listLocation = _userItinerary.GetListLocationCheckin(accountId);
            if (listLocation == null)
            {
                return null;
            }
            var userLocationCheckin = _mapper.Map<List<LocationCheckinDto>>(listLocation);
            var userLocationCheckinResponse = userLocationCheckin
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

            var response = new PageResponse<List<LocationCheckinDto>>(userLocationCheckinResponse, filter.PageNumber, filter.PageSize);
            response.TotalRecords = userLocationCheckin.Count();
            response.TotalPages = response.CalTotalPages(userLocationCheckin.Count(), filter.PageSize);

            return response;
        }
    }
}