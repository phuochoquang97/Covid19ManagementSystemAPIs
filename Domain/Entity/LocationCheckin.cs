using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid_Project.Persistence.BaseEntity;

namespace Covid_Project.Domain.Models
{
    [Table("LocationCheckin")]
    public class LocationCheckin : BaseEntity
    {
        public LocationCheckin()
        {
            PeopleLocations = new List<PeopleLocation>();
        }
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public string Address { get; set; }
        public DateTime Time { get; set; }
        public virtual List<PeopleLocation> PeopleLocations { get; set; }
    }
}