using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid_Project.Persistence.BaseEntity;

namespace Covid_Project.Domain.Models
{
    [Table("City")]
    public class City : BaseEntity
    {
        public City()
        {
            DepartureItineraries = new HashSet<Itinerary>();
            DestinationItineraties = new HashSet<Itinerary>();
            TestingLocations = new HashSet<TestingLocation>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status  { get; set; }
        public virtual ICollection<Itinerary> DepartureItineraries { get; set; }
        public virtual ICollection<Itinerary> DestinationItineraties { get; set; }
        public virtual ICollection<TestingLocation> TestingLocations { get; set; }
    }
}