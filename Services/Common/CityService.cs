using System.Collections.Generic;
using AutoMapper;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Domain.Repositories.Common;
using Covid_Project.Domain.Services.Common;

namespace Covid_Project.Services.Common
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _city;
        private readonly IRoleRepository _role;
        private readonly IPermissionRepository _permission;
        private readonly IMapper _mapper;
        public CityService(ICityRepository city, IRoleRepository role, IPermissionRepository permission, IMapper mapper)
        {
            _mapper = mapper;
            _permission = permission;
            _role = role;
            _city = city;

        }
        public CityDto AddCity(int accountId, CityDto city)
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

            if (!_permission.GetListPermission(roles).Contains("CREATE"))
            {
                return null;
            }
            var cityRet = _city.AddCity(_mapper.Map<City>(city));
            if(cityRet == null)
            {
                return null;
            }
            return _mapper.Map<CityDto>(cityRet);
        }

        public bool DeleteCity(int accountId, int cityId)
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
                return false;
            }
            if (!_permission.GetListPermission(roles).Contains("DELETE"))
            {
                return false;
            }
            return _city.DeleteCity(cityId);    
        }

        public CityDto GetCityById(int accountId, int cityId)
        {
            var roles = _role.GetRoles(accountId);
            if (!_permission.GetListPermission(roles).Contains("READ"))
            {
                return null;
            }
            var city = _city.GetCityById(cityId);
            if (city == null)
            {
                return null;
            }
            var cityRes = new CityDto(city.Id, city.Name, city.Status == 0 ? "Negative" : "Positive");
            return cityRes;
        }

        public List<CityDto> GetListCity(int accountId)
        {
            var roles = _role.GetRoles(accountId);
            if (!_permission.GetListPermission(roles).Contains("READ"))
            {
                return null;
            }
            var listCities = _city.GetListCity();
            var listCitiesRes = new List<CityDto>();
            foreach (var city in listCities)
            {
                var cityDto = new CityDto(city.Id, city.Name, city.Status == 0 ? "Negative" : "Positive");
                listCitiesRes.Add(cityDto);
            }
            return listCitiesRes;
        }

        public CityDto UpdateCity(int accountId, int cityId, CityDto cityDto)
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

            if (!_permission.GetListPermission(roles).Contains("UPDATE"))
            {
                return null;
            }
            var cityRes = _city.UpdateCity(cityId, _mapper.Map<City>(cityDto));
            if(cityRes == null)
            {
                return null;
            }
            return cityDto;
        }
    }
}