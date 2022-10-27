using Covid_Project.Domain.Models;
using Covid_Project.Domain.Repositories.Admin;
using Covid_Project.Domain.Repositories.Authorization;
using Microsoft.AspNetCore.Http;

namespace Covid_Project.Services.Admin
{
    public class AdminHomepageService : IAdminHomePageService
    {
        private readonly IAdminHomepageRepository _adminHomepage;
        private readonly IRoleRepository _role;
        private readonly IPermissionRepository _permission;
        public AdminHomepageService(IAdminHomepageRepository adminHomepage, IRoleRepository role, IPermissionRepository permission)
        {
            _permission = permission;
            _role = role;
            _adminHomepage = adminHomepage;
        }
        public News AddNews(int accountId, News news)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("ADMIN2"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("CREATE"))
            {
                return null;
            }

            return _adminHomepage.AddNews(news);
        }

        public bool DeleteNews(int accountId, int newsId)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("ADMIN2"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return false;
            }
            if (!_permission.GetListPermission(roles).Contains("DELETE"))
            {
                return false;
            }

            return _adminHomepage.DeleteNews(newsId);
        }

        public News EditNews(int accountId, int newsId, News news)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("ADMIN2"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("UPDATE"))
            {
                return null;
            }

            return _adminHomepage.EditNews(newsId, news);
        }

        public string PostNewsImage(int accountId, int newsId, IFormFile newsImg)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("ADMIN2"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("UPDATE"))
            {
                return null;
            }

            return _adminHomepage.PostNewsImage(newsId, newsImg);

        }
    }
}