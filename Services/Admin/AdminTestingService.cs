using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Repositories.Admin;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Domain.Services.Admin;
using Covid_Project.Domain.Services.Communication;

namespace Covid_Project.Services.Admin
{
    public class AdminTestingService : IAdminTestingService
    {
        private readonly IAdminTestingRepository _adminTesting;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _role;
        private readonly IPermissionRepository _permission;
        public AdminTestingService(IAdminTestingRepository adminTesting, IMapper mapper, IRoleRepository role, IPermissionRepository permission)
        {
            _permission = permission;
            _role = role;
            _mapper = mapper;
            _adminTesting = adminTesting;
        }
        public PageResponse<List<TestingMedicalInfo>> GetAllTesting(int accountId, PaginationFilter filter)
        {
            var roles = _role.GetRoles(accountId);
            if(roles == null)
            {
                return null;
            }
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("ADMIN"))
                {
                    checkRole = true;
                }
            }
            if (!checkRole)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("READ"))
            {
                return null;
            }
            var testingRet = _adminTesting.GetAllTesting();
            var medicalInfoRet = _adminTesting.GetAllMedicalInfo();
            if(testingRet == null || medicalInfoRet == null)
            {
                return null;
            }
            var testings = _mapper.Map<List<TestingResultDto>>(testingRet);
            var medicalInfos = _mapper.Map<List<MedicalInfoDto>>(medicalInfoRet);

            for(int i  = 0; i < testings.Count; i++)
            {
                testings[i].FullName = testingRet[i].Account.Profile.FirstName + " " + testingRet[i].Account.Profile.LastName;
            }
            var testingsRepsponse = testings
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

            var medicalInfoResponse = medicalInfos
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

            var testingMedicalInfos = new List<TestingMedicalInfo>();
            for(int i = 0; i<testingsRepsponse.Count; i++){
                testingMedicalInfos.Add(new TestingMedicalInfo(testingsRepsponse[i], medicalInfos[i]));
            }

            
            var response = new PageResponse<List<TestingMedicalInfo>>(testingMedicalInfos, filter.PageNumber, filter.PageSize);
            response.TotalRecords = testings.Count();
            response.TotalPages = response.CalTotalPages(testings.Count(), filter.PageSize);

            return response;
        }

        public TestingResultDto GetTestingById(int accountId, int testingId)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Equals("ADMIN2"))
                {
                    checkRole = true;
                }
            }
            if (!checkRole)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("READ"))
            {
                return null;
            }
            var testingRes = _adminTesting.GetTestingById(testingId);
            if(testingRes == null)
            {
                return null;
            }
            
            var testings = _mapper.Map<TestingResultDto>(testingRes);
            return testings;
        }

        public PageResponse<List<TestingResultDto>> GetTestingByUser(int accountId, int userAccountId, PaginationFilter filter)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Equals("ADMIN2"))
                {
                    checkRole = true;
                }
            }
            if (!checkRole)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("READ"))
            {
                return null;
            }
            var checkUser = _adminTesting.GetTestingByUSer(userAccountId);
            if (checkUser == null)
            {
                return null;
            }
            var testings = _mapper.Map<List<TestingResultDto>>(checkUser);
            
            var testingsResponse = testings
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();
            var response = new PageResponse<List<TestingResultDto>>(testingsResponse, filter.PageNumber, filter.PageSize);
            response.TotalRecords = testings.Count();
            response.TotalPages = response.CalTotalPages(testings.Count(), filter.PageSize);

            return response;
        }

        public TestingStatisticDto GetTestingStatistic(int accountId, int tesingLocationId)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Equals("ADMIN2"))
                {
                    checkRole = true;
                }
            }
            if (!checkRole)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("READ"))
            {
                return null;
            }
            return _adminTesting.GetTestingStatistic(tesingLocationId);
        }

        public TestingDto UpdateTesting(int accountId, int testingId, TestingDto testing)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Equals("ADMIN2"))
                {
                    checkRole = true;
                }
            }
            if (!checkRole)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("UPDATE"))
            {
                return null;
            }
            var testingUpdated = _mapper.Map<Testing>(testing);

            return _mapper.Map<TestingDto>(_adminTesting.UpdateTesting(testingId, testingUpdated));
        }
    }
}