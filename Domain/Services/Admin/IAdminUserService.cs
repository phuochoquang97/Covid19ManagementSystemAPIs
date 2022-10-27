using System.Collections.Generic;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Services.Communication;

namespace Covid_Project.Domain.Services.Admin
{
    public interface IAdminUserService
    {
        PageResponse<List<ProfileDto>> GetAllUserProfile(int accountId, PaginationFilter filter);
    }
}