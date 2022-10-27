using System;
using System.Collections.Generic;
using System.Linq;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Repositories.Common;
using Covid_Project.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Covid_Project.Persistence.Repositories.Common
{
    public class TestingLocationRepository : ITestingLocationRepository
    {
        private readonly AppDbContext _context;
        public TestingLocationRepository(AppDbContext context)
        {
            _context = context;
        }
        public TestingLocation AddLocation(TestingLocation testingLocation)
        {
            try
            {
                _context.TestingLocations.Add(testingLocation);
                _context.SaveChanges();
                return testingLocation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool DeleteLocation(int locatonId)
        {
            try
            {
                var location = _context.TestingLocations.Where(s => s.IsDeleted == false).FirstOrDefault(x => x.Id == locatonId);
                if (location != null)
                {
                    location.IsDeleted = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public List<TestingLocation> GetListLocation()
        {
            try
            {
                return _context.TestingLocations
                .Include(x => x.City)
                .Where(x => x.IsDeleted == false)
                .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public TestingLocation GetLocationById(int locationId)
        {
            try
            {
                return _context.TestingLocations
                .Include(x => x.City)
                .Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Id == locationId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public TestingLocation UpdateLocation(int locationId, TestingLocation location)
        {
            try
            {
                var locationUpdated = _context.TestingLocations
                .Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Id == locationId);

                if (locationUpdated == null)
                {
                    return null;
                }

                locationUpdated.Name = location.Name;
                locationUpdated.Address = location.Address;
                var city = _context.Cities.FirstOrDefault(x => x.Id == location.CityId && x.IsDeleted == false);
                locationUpdated.City = city;
                _context.SaveChanges();

                return locationUpdated;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}