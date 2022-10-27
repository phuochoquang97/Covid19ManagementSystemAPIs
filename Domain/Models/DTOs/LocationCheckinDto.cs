using System;
namespace Covid_Project.Domain.Models.DTOs
{
    public class LocationCheckinDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public DateTime Time { get; set; }
    }
}