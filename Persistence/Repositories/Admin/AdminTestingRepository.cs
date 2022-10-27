using System;
using System.Collections.Generic;
using System.Linq;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Repositories.Admin;
using Covid_Project.Domain.Services.Communication;
using Covid_Project.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Covid_Project.Persistence.Repositories.Admin
{
    public class AdminTestingRepository : IAdminTestingRepository
    {
        private readonly AppDbContext _context;
        public AdminTestingRepository(AppDbContext context)
        {
            _context = context;

        }

        public List<MedicalInfo> GetAllMedicalInfo()
        {
            try
            {
                var medicalInfos = _context.MedicalInfo
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.CreatedAt)
                .ToList();

                return medicalInfos;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public List<Testing> GetAllTesting()
        {
            try
            {
                var testings = _context.Testings
                .Include(x => x.Account.Profile)
                .Include(x => x.TestingLocation)
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.CreatedAt)
                .ToList();
            
                var testingLocations = _context.TestingLocations
                .Include(x => x.City)
                .Where(x => x.IsDeleted == false).ToList();
                foreach(var testing in testings)
                {
                    testing.TestingLocation = testingLocations.FirstOrDefault(x => x.Id == testing.TestingLocationId);
                }
                return testings;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Testing GetTestingById(int testingId)
        {
            try
            {
                var testing = _context.Testings
                .Include(x => x.TestingLocation)
                .Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Id == testingId);
                var testingLocations = _context.TestingLocations.Where(x => x.IsDeleted == false).ToList();
                testing.TestingLocation = testingLocations.FirstOrDefault(x => x.Id == testing.TestingLocationId);
                return testing;
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public List<Testing> GetTestingByUSer(int userAccountId)
        {
            try
            {
                var account = _context.Accounts.FirstOrDefault(x => x.Id == userAccountId && x.IsDeleted == false);
                if(account == null)
                {
                    return null;
                }
                var userTestings = _context.Testings
                .Include(x => x.TestingLocation)
                .Where(x => x.IsDeleted == false && x.AccountId == userAccountId)
                .ToList();
                
                var testingLocations = _context.TestingLocations.Where(x => x.IsDeleted == false).ToList();
                foreach(var testing in userTestings)
                {
                    testing.TestingLocation = testingLocations.FirstOrDefault(x => x.Id == testing.TestingLocationId);
                }
                return userTestings;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public TestingStatisticDto GetTestingStatistic(int testingLocationId)
        {
            try
            {
                var testingStatistic = new TestingStatisticDto();
                testingStatistic.NumberOfUsers = _context.Accounts
                    .Where(x => x.IsDeleted == false)
                    .ToList()
                    .Count();

                if(testingLocationId == -1)
                {
                    testingStatistic.NegativeCases = _context.Testings
                    .Where(x => x.IsDeleted == false && x.Result == false && x.TestingState == 2)
                    .ToList()
                    .Count();

                    testingStatistic.PositiveCases = _context.Testings
                    .Where(x => x.IsDeleted == false && x.Result == true && x.TestingState == 2)
                    .ToList()
                    .Count();

                    testingStatistic.PendingCases = _context.Testings
                    .Where(x => x.IsDeleted == false && x.TestingState != 2)
                    .ToList()
                    .Count(); 
                }
                else
                {
                    testingStatistic.NegativeCases = _context.Testings
                    .Where(x => x.IsDeleted == false && x.Result == false && x.TestingState == 2 && x.TestingLocationId == testingLocationId)
                    .ToList()
                    .Count();

                    testingStatistic.PositiveCases = _context.Testings
                    .Where(x => x.IsDeleted == false && x.Result == true && x.TestingState == 2 && x.TestingLocationId == testingLocationId)
                    .ToList()
                    .Count();

                    testingStatistic.PendingCases = _context.Testings
                    .Where(x => x.IsDeleted == false && x.TestingState != 2 && x.TestingLocationId == testingLocationId)
                    .ToList()
                    .Count();
                }

                return testingStatistic;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        // just update Result, TestingState and IsPaid
        public Testing UpdateTesting(int testingId, Testing testing)
        {
            try
            {
                var testingUpdated = _context.Testings
                .Include(x => x.TestingLocation)
                .FirstOrDefault(x => x.Id == testingId && x.IsDeleted == false);
                if(testingUpdated == null)
                {
                    return null;
                }
                testingUpdated.Result = testing.Result;
                testingUpdated.TestingState = testing.TestingState;
                testingUpdated.IsPaid = testing.IsPaid;
                _context.SaveChanges();

                return testingUpdated;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}