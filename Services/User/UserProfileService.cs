using AutoMapper;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Domain.Repositories.User;
using Covid_Project.Domain.Services.User;
using Microsoft.AspNetCore.Http;

namespace Covid_Project.Services.User
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _profile;
        private readonly IRoleRepository _role;
        private readonly IPermissionRepository _permission;
        private readonly IMapper _mapper;
        public UserProfileService(IUserProfileRepository profile, IRoleRepository role, IPermissionRepository permission, IMapper mapper)
        {
            _mapper = mapper;
            _permission = permission;
            _role = role;
            _profile = profile;
        }

        public bool ChangePassword(int accountId, string oldPassword, string newPassword)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("USER"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return false;
            }
            if (!_permission.GetListPermission(roles).Contains("UPDATE"))
            {
                return false;
            }
            return _profile.ChangePassword(accountId, oldPassword, newPassword);
        }

        public ProfileDto EditProfile(int accountId, ProfileDto profile)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("USER"))
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
            if(profile == null)
            {
                return null;
            }
            var profileEditted = _mapper.Map<Domain.Models.Profile>(profile);
            var profileRes = _profile.EditProfile(accountId, profileEditted);
            if(profileRes == null)
            {
                return null;
            }
            return _mapper.Map<ProfileDto>(profileRes);

        }

        

        public ProfileDto GetProfile(int accountId)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("USER"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("READ"))
            {
                return null;
            }
            var profileRes = _profile.GetProfile(accountId);
            if(profileRes == null)
            {
                return null;
            }
            if(profileRes == null)
            {
                return null;
            }
            var profileMapped = _mapper.Map<ProfileDto>(profileRes);
            profileMapped.Email = profileRes.Account.Email;
            return profileMapped;
        }

        public string GetAvatar(int accountId)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("USER"))
                {
                    checkRole = true;
                }
            }
            if (checkRole == false)
            {
                return null;
            }
            if (!_permission.GetListPermission(roles).Contains("READ"))
            {
                return null;
            }
            var avatar = _profile.GetAvatar(accountId);
            return avatar;
        }

        public string PostAvatar(int accountId, IFormFile avatar)
        {
            var roles = _role.GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role.GuardName.Contains("USER"))
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

            return _profile.PostAvatar(accountId, avatar);
        }
    }
}