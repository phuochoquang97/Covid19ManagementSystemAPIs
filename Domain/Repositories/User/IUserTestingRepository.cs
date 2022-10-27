
using System;
using System.Collections.Generic;
using Covid_Project.Domain.Models;

namespace Covid_Project.Domain.Repositories.User
{
    public interface IUserTestingRepository
    {
        bool CheckRegister(int testingLocationId, DateTime testingDate);
        Testing Register(int accountId, Testing register);
        List<Testing> GetListTesting(int accountId);
        List<DateTime> GetInvalidTestingDate(int testingLocationId); 
        MedicalInfo MedicalInforRegister(int accountId, MedicalInfo medicalInfo);
    }
}