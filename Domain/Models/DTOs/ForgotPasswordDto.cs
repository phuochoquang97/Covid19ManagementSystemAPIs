using System.ComponentModel.DataAnnotations;

namespace Covid_Project.Domain.Models.DTOs
{
    public class ForgotPasswordDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Password { get; set; }
    }
}