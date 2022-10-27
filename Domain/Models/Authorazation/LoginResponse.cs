using System.Collections.Generic;
using Covid_Project.Domain.Models.DTOs;

namespace Covid_Project.Domain.Models.Authorazation
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string FullName { get; set; }
        public bool IsVerified { get; set; }
        public List<RoleDto> Roles { get; set; }

        public List<string> Permissions { get; set; }
    }
}