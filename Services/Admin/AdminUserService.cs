using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Repositories.Admin;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Domain.Services.Admin;
using Covid_Project.Domain.Services.Communication;

namespace Covid_Project.Services.Admin
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IAdminUserRepository _user;
        private readonly IPermissionRepository _permission;
        private readonly IRoleRepository _role;
        private readonly IMapper _mapper;
        public AdminUserService(IAdminUserRepository user, IRoleRepository role, IPermissionRepository permission, IMapper mapper)
        {
            _mapper = mapper;
            _role = role;
            _permission = permission;
            _user = user;
        }
        public PageResponse<List<ProfileDto>> GetAllUserProfile(int accountId, PaginationFilter filter)
        {
            var roles = _role.GetRoles(accountId);
            if (roles == null)
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
            var users = _user.GetAllUserProfile();
            var userMapped = _mapper.Map<List<ProfileDto>>(users);
            for (int i = 0; i < users.Count; i++)
            {
                userMapped[i].Email = users[i].Account.Email;
            }

            if (users == null)
            {
                return null;
            }
            var usersRet = userMapped
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

            var response = new PageResponse<List<ProfileDto>>(usersRet, filter.PageNumber, filter.PageSize);
            response.TotalRecords = users.Count();
            response.TotalPages = response.CalTotalPages(users.Count(), filter.PageSize);

            return response;
        }
    }
}