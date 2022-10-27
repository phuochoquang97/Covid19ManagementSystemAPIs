using System.ComponentModel.DataAnnotations;

namespace Covid_Project.Domain.Models.Authorazation
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}