using System;
using System.ComponentModel.DataAnnotations;

namespace Covid_Project.Domain.Models.User
{
    public class ItineraryModel
    {
        public int Id { get; set; }
        [Required]
        public int DepartureCityId { get; set; }
        [Required]
        public int DestinationCityId { get; set; }
        [Required]
        public string FlyNo { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        [Required]
        public DateTime LandingTime { get; set; }
        public bool MustTesting { get; set; }
    }
}