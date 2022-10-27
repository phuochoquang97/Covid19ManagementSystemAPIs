using System.ComponentModel.DataAnnotations;

namespace Covid_Project.Domain.Models.DTOs
{
    public class TestingLocationDto
    {
        public int Id { get; set; }
        [Required]
        public int CityId { get; set; }
        public CityDto City { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}