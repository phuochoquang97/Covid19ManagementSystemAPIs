using System.Collections.Generic;
using AutoMapper;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Domain.Repositories.Common;
using Covid_Project.Domain.Services.Common;

namespace Covid_Project.Services.Common
{
    public class TestingLocationService : ITestingLocationService
    {
        private readonly ITestingLocationRepository _testingLocation;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _role;
        private readonly IPermissionRepository _permission;
        public TestingLocationService(ITestingLocationRepository testingLocation, IMapper mapper, IRoleRepository role, IPermissionRepository permission)
        {
            _permission = permission;
            _role = role;
            _mapper = mapper;
            _testingLocation = testingLocation;
        }
        public TestingLocationDto AddLocation(int accountId, TestingLocationDto testingLocation)
        {

            var roles = _role.GetRoles(accountId);
            if (!_permission.GetListPermission(roles).Contains("CREATE"))
            {
                return null;
            }
            var location = _mapper.Map<TestingLocation>(testingLocation);
            var locationRes = _testingLocation.AddLocation(location);
            return _mapper.Map<TestingLocationDto>(locationRes);
        }

        public bool DeleteLocation(int accountId, int locationId)
        {
            var roles = _role.GetRoles(accountId);
            if (!_permission.GetListPermission(roles).Contains("DELETE"))
            {
                return false;
            }
            return _testingLocation.DeleteLocation(locationId);
        }

        public List<TestingLocationDto> GetListLocation(int accountId)
        {
            var roles = _role.GetRoles(accountId);
            if (!_permission.GetListPermission(roles).Contains("READ"))
            {
                return null;
            }
            var listLocations = _testingLocation.GetListLocation();
            var listLocationsRet = _mapper.Map<List<TestingLocationDto>>(listLocations);
            for (int i = 0; i < listLocationsRet.Count; i++)
            {
                listLocationsRet[i].City = _mapper.Map<CityDto>(listLocations[i].City);
            }
            return listLocationsRet;
        }

        public TestingLocationDto GetLocationById(int accountId, int locationId)
        {
            var roles = _role.GetRoles(accountId);
            if (!_permission.GetListPermission(roles).Contains("READ"))
            {
                return null;
            }
            var location = _testingLocation.GetLocationById(locationId);
            if (location == null)
            {
                return null;
            }
            return _mapper.Map<TestingLocationDto>(location);
        }

        public TestingLocationDto UpdateLocation(int accountId, int locationId, TestingLocationDto location)
        {
            var roles = _role.GetRoles(accountId);
            if (!_permission.GetListPermission(roles).Contains("UPDATE"))
            {
                return null;
            }
            var locationUpdated = _mapper.Map<TestingLocation>(location);
            var locationRes = _testingLocation.UpdateLocation(locationId, locationUpdated);
            if (locationRes == null)
            {
                return null;
            }
            return _mapper.Map<TestingLocationDto>(locationRes);
        }
    }
}