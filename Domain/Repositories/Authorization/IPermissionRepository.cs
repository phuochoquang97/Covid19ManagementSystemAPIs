using System.Collections.Generic;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;

namespace Covid_Project.Domain.Repositories.Authorization
{
    public interface IPermissionRepository
    {
        List<string> GetListPermission(List<RoleDto> roles);
    }
}