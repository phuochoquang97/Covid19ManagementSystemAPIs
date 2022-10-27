using System.Collections.Generic;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;

namespace Covid_Project.Domain.Repositories.Common
{
    public interface ITestingLocationRepository
    {
        List<TestingLocation> GetListLocation();
        TestingLocation GetLocationById(int locationId);
        TestingLocation AddLocation(TestingLocation testingLocation);
        TestingLocation UpdateLocation(int locationId, TestingLocation location);
        bool DeleteLocation(int locatonId);
    }
}