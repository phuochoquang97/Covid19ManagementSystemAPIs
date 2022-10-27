using System.ComponentModel.DataAnnotations;

namespace Covid_Project.Domain.Models.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}