using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Covid_Project.Persistence.BaseEntity;

namespace Covid_Project.Domain.Models
{
    [Table("Account")]
    public class Account : BaseEntity
    {
        public Account()
        {
            AccountHasRoles = new HashSet<AccountHasRole>();
            AccountHasPermissions = new HashSet<AccountHasPermission>();
            DailyCheckins = new HashSet<DailyCheckin>();
            Itineraries = new HashSet<Itinerary>();
            Profile = new Profile();
            MedicalInfo = new HashSet<MedicalInfo>();
            LocationCheckins = new HashSet<LocationCheckin>();
            Testings = new HashSet<Testing>();
        }
        [Key]
        public int Id { get; set; }
        [Required][MaxLength(320)]
        public string Email { get; set; }
        [Required][MaxLength(50)]
        public string Password { get; set; }
        public int IsVerified { get; set; }
        public string Code { get; set; }
        public string DynamicCode { get; set; }
        public virtual ICollection<AccountHasRole> AccountHasRoles { get; set; }
        public virtual ICollection<AccountHasPermission> AccountHasPermissions { get; set; }
        public virtual ICollection<DailyCheckin> DailyCheckins { get; set; }
        public virtual ICollection<Itinerary> Itineraries { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<MedicalInfo> MedicalInfo { get; set; }
        public virtual ICollection<LocationCheckin> LocationCheckins { get; set; }
        public virtual ICollection<Testing> Testings { get; set; }
    }
}