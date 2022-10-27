using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid_Project.Persistence.BaseEntity;

namespace Covid_Project.Domain.Models
{
    [Table("UserProfile")]
    public class Profile : BaseEntity
    {
        public Profile()
        {
            Gender = -1;
        }
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdNo { get; set; }
        [MaxLength(50)]
        public string Nationality { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        
    }
}