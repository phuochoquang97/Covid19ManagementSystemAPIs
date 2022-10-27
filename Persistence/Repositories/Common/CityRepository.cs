using System;
using System.Collections.Generic;
using System.Linq;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Repositories.Common;
using Covid_Project.Persistence.Context;

namespace Covid_Project.Persistence.Repositories.Common
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;
        public CityRepository(AppDbContext context)
        {
            _context = context;

        }
        public City AddCity(City city)
        {
            try
            {
                _context.Cities.Add(city);
                _context.SaveChanges();
                return city;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool DeleteCity(int cityId)
        {
            try
            {
                var cityDeleted = _context.Cities.Where(s => s.IsDeleted == false).FirstOrDefault(x => x.Id == cityId);
                if(cityDeleted == null)
                {
                    return false;
                }
                cityDeleted.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public City GetCityById(int cityId)
        {
            try
            {
                var city = _context.Cities.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == cityId);
                return city;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public List<City> GetListCity()
        {
            try
            {
                return _context.Cities
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.Name)
                .ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public City UpdateCity(int cityId, City city)
        {
            try
            {
                var cityUpdated = _context.Cities.Where(s => s.IsDeleted == false).FirstOrDefault(x => x.Id == cityId);
                if(cityUpdated == null)
                {
                    return null;
                }
                cityUpdated.Name = city.Name;
                cityUpdated.Status = city.Status;

                _context.SaveChanges();

                return city;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}