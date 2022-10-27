using System.Collections.Generic;
using Covid_Project.Domain.Models;

namespace Covid_Project.Domain.Repositories.Admin
{
    public interface IAdminUserRepository
    {
        List<Profile> GetAllUserProfile();
    }
}