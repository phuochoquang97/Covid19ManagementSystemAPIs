using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid_Project.Persistence.BaseEntity;

namespace Covid_Project.Domain.Models
{
    [Table("UserItinerary")]
    public class Itinerary : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        [Required]
        public int DepartureCityId { get; set; }
        [ForeignKey("DepartureCityId")]
        public virtual City DepartureCity { get; set; }

        [Required]
        public int DestinationCityId { get; set; }
        [ForeignKey("DestinationCityId")]
        public virtual City DestinationCity { get; set; }
        [Required][MaxLength(50)]
        
        public string FlyNo { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        [Required]
        public DateTime LandingTime { get; set; }
    }
}