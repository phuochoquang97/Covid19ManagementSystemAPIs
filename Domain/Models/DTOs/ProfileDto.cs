using System;

namespace Covid_Project.Domain.Models.DTOs
{
    public class ProfileDto
    {
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string IdNo { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        
    }
}