using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Repositories.Admin;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Domain.Repositories.User;
using Covid_Project.Domain.Services.Admin;
using Covid_Project.Domain.Services.Communication;

namespace Covid_Project.Services.Admin
{
    public class AdminItineraryService : IAdminItineraryService
    {
        private readonly IAdminItineraryRepository _adminItinerary;
        private readonly IPermissionRepository _permission;
        private readonly IRoleRepository _role;
        private readonly IUserItineraryRepository _userItinerary;
        private readonly IMapper _mapper;
        public AdminItineraryService(IAdminItineraryRepository adminItinerary, 
        IRoleRepository role, 
        IPermissionRepository permission, 
        IUserItineraryRepository userItinerary,
        IMapper mapper)
        {
            _userItinerary = userItinerary;
            _role = role;
            _permission = permission;
            _adminItinerary = adminItinerary;
            _mapper = mapper;
        }

        public ItineraryStatisticDto GetItineraryStatistic(int accountId, DateTime startDate, DateTime endDate)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("ADMIN"))
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
            return _adminItinerary.GetItineraryStatistic(startDate, endDate);
        }

        public UserItineraryDto GetUserItinerary(int accountId, string email)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("ADMIN"))
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
            var userItinerary = _adminItinerary.GetUserItinerary(email);
            if (userItinerary == null)
            {
                return null;
            }
            var userItineraryRes = new UserItineraryDto()
            {
                Id = userItinerary.Id,
                Email = email,
                Departure = userItinerary.DepartureCity.Name,
                Destination = userItinerary.DestinationCity.Name,
                DepartureTime = userItinerary.DepartureTime,
                LandingTime = userItinerary.LandingTime,
                TravelNo = userItinerary.FlyNo
            };
            return userItineraryRes;
        }

        public PageResponse<List<UserItineraryDto>> GetItinerariesByUser(int accountId, string userEmail, PaginationFilter filter)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("ADMIN2"))
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
            var userItineraries = _adminItinerary.GetAllUserItinerary(userEmail);
            var userItinerariesMapped = _mapper.Map<List<UserItineraryDto>>(userItineraries);
            for(int i =0 ; i<userItineraries.Count; i++)
            {
                userItinerariesMapped[i].Email = userEmail;
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

        public List<LocationCheckinDto> GetListLocationCheckinByUser(int accountId, int userId)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("ADMIN2"))
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
            var userLocationCheckin = _userItinerary.GetListLocationCheckin(userId);
            return _mapper.Map<List<LocationCheckinDto>>(userLocationCheckin);
        }
    }
}