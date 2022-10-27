using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid_Project.Persistence.BaseEntity;

namespace Covid_Project.Domain.Models
{
    [Table("PeopleLocation")]
    public class PeopleLocation : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int LocationCheckinId { get; set; }
        [ForeignKey("LocationCheckinId")]
        public virtual LocationCheckin LocationCheckin { get; set; }
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}