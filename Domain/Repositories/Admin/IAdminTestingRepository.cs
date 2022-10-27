using System.Collections.Generic;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Services.Communication;

namespace Covid_Project.Domain.Repositories.Admin
{
    public interface IAdminTestingRepository
    {
        List<Testing> GetAllTesting();
        List<MedicalInfo> GetAllMedicalInfo();  
        List<Testing> GetTestingByUSer(int userAccountId);
        Testing GetTestingById(int testingId);
        Testing UpdateTesting(int testingId, Testing testing);
        TestingStatisticDto GetTestingStatistic(int testingLocationId);

            
    }
}