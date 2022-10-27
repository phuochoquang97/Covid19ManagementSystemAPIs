using System.Collections.Generic;
using Covid_Project.Domain.Models;

namespace Covid_Project.Domain.Repositories.Common
{
    public interface ICityRepository
    {
        List<City> GetListCity();
        City GetCityById(int cityId);
        City UpdateCity(int cityId, City city);
        City AddCity(City city);
        bool DeleteCity(int cityId);
    }
}