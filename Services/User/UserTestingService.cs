using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Domain.Repositories.User;
using Covid_Project.Domain.Services.Communication;
using Covid_Project.Domain.Services.User;

namespace Covid_Project.Services.User
{
    public class UserTestingService : IUserTestingService
    {
        private readonly IUserTestingRepository _userTesting;
        private readonly IMapper _mapper;
        private readonly IPermissionRepository _permission;
        private readonly IRoleRepository _role;
        public UserTestingService(IUserTestingRepository userTesting, IMapper mapper, IPermissionRepository permission, IRoleRepository role)
        {
            _role = role;
            _permission = permission;
            _mapper = mapper;
            _userTesting = userTesting;
        }
        public TestingResultDto RegisterTesting(int accountId, TestingRegisterDto registerDto)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach(var role in roles)
            {
                if(role.GuardName.Equals("USER"))
                {
                    checkRole = true;
                }
            }
            if(!checkRole)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("CREATE"))
            {
                return null;
            }
            var checkRegister = _userTesting.CheckRegister(registerDto.TestingLocationId, registerDto.TestingDate);
            if (checkRegister == true)
            {
                return null;
            }
            var register = _mapper.Map<Testing>(registerDto);
            var registerRes = _userTesting.Register(accountId, register);
            if(registerRes == null)
            {
                return null;
            }
            var registerDtoRes = _mapper.Map<TestingResultDto>(registerRes);
            registerDtoRes.Result = "Chưa có kết quả.";
            registerDtoRes.TestingLocation = register.TestingLocation;
            registerDtoRes.TestingLocation.Testings = null;
            return registerDtoRes;
        }

        public List<DateTime> GetInvalidTestingDate(int accountId, int testingLocationId)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach(var role in roles)
            {
                if(role.GuardName.Equals("USER"))
                {
                    checkRole = true;
                }
            }
            if(checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("READ"))
            {
                return null;
            }
            return _userTesting.GetInvalidTestingDate(testingLocationId);
        }

        public MedicalInfoDto MedicalInfoRegister(int accountId, MedicalInfoDto medicalInfoDto)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach(var role in roles)
            {
                if(role.GuardName.Equals("USER"))
                {
                    checkRole = true;
                }
            }
            if(checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("CREATE"))
            {
                return null;
            }
            medicalInfoDto.AccountId = accountId;
            var medicalInfo = _mapper.Map<MedicalInfo>(medicalInfoDto);
            return _mapper.Map<MedicalInfoDto>(_userTesting.MedicalInforRegister(accountId, medicalInfo));
        }

        public PageResponse<List<TestingResultDto>> GetListTesting(int accountId, PaginationFilter filter)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach(var role in roles)
            {
                if(role.GuardName.Equals("USER"))
                {
                    checkRole = true;
                }
            }
            if(checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("CREATE"))
            {
                return null;
            }
            var testingsMapped = _mapper.Map<List<TestingResultDto>>(_userTesting.GetListTesting(accountId));

            var testingsResponse = testingsMapped
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

            var response = new PageResponse<List<TestingResultDto>>(testingsResponse, filter.PageNumber, filter.PageSize);
            response.TotalRecords = testingsMapped.Count();
            response.TotalPages = response.CalTotalPages(testingsMapped.Count(), filter.PageSize);

            return response;
        }
    }
}