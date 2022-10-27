using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Covid_Project.Domain.Models.Authorazation
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        [Required][MinLength(8)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}