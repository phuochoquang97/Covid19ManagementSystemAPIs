using System.Collections.Generic;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;

namespace Covid_Project.Domain.Services.Common
{
    public interface ICityService
    {
        List<CityDto> GetListCity(int accountId);
        CityDto GetCityById(int accountId, int cityId);
        CityDto UpdateCity(int accountId, int cityId, CityDto city);
        CityDto AddCity(int accountId,CityDto city);
        bool DeleteCity(int accountId, int cityId);
    }
}