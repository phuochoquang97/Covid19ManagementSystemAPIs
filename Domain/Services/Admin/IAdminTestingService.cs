using System.Collections.Generic;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Services.Communication;

namespace Covid_Project.Domain.Services.Admin
{
    public interface IAdminTestingService
    {
        PageResponse<List<TestingMedicalInfo>> GetAllTesting(int accountId, PaginationFilter filter);
        TestingResultDto GetTestingById(int accountId, int testingId);
        PageResponse<List<TestingResultDto>> GetTestingByUser(int accountId, int userAccountId, PaginationFilter filter);
        TestingDto UpdateTesting(int accountId, int testingId, TestingDto testing);
        TestingStatisticDto GetTestingStatistic(int accountId, int tesingLocationId);
        
    }
}