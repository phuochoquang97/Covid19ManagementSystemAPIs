using System;
using System.Collections.Generic;
using System.Linq;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Repositories.User;
using Covid_Project.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Covid_Project.Persistence.Repositories.User
{
    public class UserTestingRepository : IUserTestingRepository
    {
        const int MINIMUM_DAY_REQUIRE = 2;
        const int MAX_TESTING_CAPACITY = 2;
        private readonly AppDbContext _context;
        public UserTestingRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckRegister(int testingLocationId, DateTime testingDate)
        {
            var testingLocation = _context.TestingLocations.FirstOrDefault(x => x.Id == testingLocationId);
            if (testingLocation == null)
            {
                return false;
            }
            var countTesting = _context.Testings
            .Where(x => x.TestingLocationId == testingLocation.Id && x.TestingDate == testingDate && x.IsDeleted == false)
            .ToList();
            return (countTesting.Count > MAX_TESTING_CAPACITY);
        }

        public List<DateTime> GetInvalidTestingDate(int testingLocationId)
        {
            // lay test cua location do
            // tim cac ngay co so luong dang ky >=2. Test
            try
            {
                var dates = _context.Testings.Where(x => x.TestingLocationId == testingLocationId && x.IsDeleted == false)
                   .GroupBy(p => p.TestingDate)
                   .Select(g => new { date = g.Key, count = g.Count() });
                var invalidDates = new List<DateTime>();
                foreach (var date in dates)
                {
                    if (date.count > MINIMUM_DAY_REQUIRE && date.date >= DateTime.UtcNow.AddDays(-1))
                    {
                        invalidDates.Add(date.date);
                    }
                }
                return invalidDates;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public List<Testing> GetListTesting(int accountId)
        {
            try
            {
                var account = _context.Accounts.FirstOrDefault(x => x.Id == accountId && x.IsDeleted == false);
                if (account == null)
                {
                    return null;
                }
                var testings = _context.Testings
                .Include(x => x.TestingLocation)
                .Where(x => x.AccountId == accountId && x.IsDeleted == false)
                .OrderByDescending(x => x.RegisterDate)
                .ToList();

                var testingLocations = _context.TestingLocations.Where(x => x.IsDeleted == false).ToList();
                foreach (var testing in testings)
                {
                    testing.TestingLocation = testingLocations.FirstOrDefault(x => x.Id == testing.TestingLocationId);
                }
                return testings;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public MedicalInfo MedicalInforRegister(int accountId, MedicalInfo medicalInfo)
        {
            try
            {
                var account = _context.Accounts.FirstOrDefault(x => x.Id == accountId && x.IsDeleted == false);
                if (account == null)
                {
                    return null;
                }
                _context.MedicalInfo.Add(medicalInfo);
                _context.SaveChanges();
                return medicalInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Testing Register(int accountId, Testing register)
        {
            try
            {
                var account = _context.Accounts.FirstOrDefault(x => x.Id == accountId && x.IsDeleted == false);
                var registerLocation = _context.TestingLocations.FirstOrDefault(x => x.Id == register.TestingLocationId && x.IsDeleted == false);
                if (account == null || registerLocation == null)
                {
                    return null;
                }
                var testingLocation = _context.TestingLocations.FirstOrDefault(x => x.Id == register.TestingLocationId && x.IsDeleted == false);
                if (testingLocation == null)
                {
                    return null;
                }
                register.TestingLocation = testingLocation;
                register.AccountId = accountId;
                register.RegisterDate = DateTime.Now.AddHours(7);
                _context.Testings.Add(register);
                _context.SaveChanges();
                return register;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}