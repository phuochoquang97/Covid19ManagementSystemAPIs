using System;
namespace Covid_Project.Domain.Models.DTOs
{
    public class UserItineraryDto
    {
        public UserItineraryDto()
        {
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public int DepartureCityId { get; set; }
        public int DestinationCityId { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; } 
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public string TravelNo { get; set; }
    }
}