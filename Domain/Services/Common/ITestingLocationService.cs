using System.Collections.Generic;
using Covid_Project.Domain.Models.DTOs;

namespace Covid_Project.Domain.Services.Common
{
    public interface ITestingLocationService
    {
         List<TestingLocationDto> GetListLocation(int accountId);
        TestingLocationDto GetLocationById(int accountId, int locationId);
        TestingLocationDto AddLocation(int accountId, TestingLocationDto testingLocation);
        TestingLocationDto UpdateLocation(int accountId, int locationId, TestingLocationDto location);
        bool DeleteLocation(int accountId, int locationId);
    }
}