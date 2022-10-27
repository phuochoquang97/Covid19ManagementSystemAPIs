using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid_Project.Persistence.BaseEntity;

namespace Covid_Project.Domain.Models
{
    [Table("TestingLocation")]
    public class TestingLocation : BaseEntity
    {
        public TestingLocation()
        {
            Testings = new HashSet<Testing>();
        }
        [Key]
        public int Id { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public virtual ICollection<Testing> Testings { get; set; }
    }
}