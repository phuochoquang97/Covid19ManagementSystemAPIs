using System;
using System.Collections.Generic;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Services.Communication;

namespace Covid_Project.Domain.Services.User
{
    public interface IUserTestingService
    {
        TestingResultDto RegisterTesting(int accountId, TestingRegisterDto registerDto);
        List<DateTime> GetInvalidTestingDate(int accountId, int testingLocationId);
        MedicalInfoDto MedicalInfoRegister(int accountId, MedicalInfoDto medicalInfoDto);
        PageResponse<List<TestingResultDto>> GetListTesting(int accountId, PaginationFilter filter);
    }
}