namespace Covid_Project.Domain.Models.DTOs
{
    public class CityDto
    {
        public CityDto(int id, string name, string status)
        {
            Id = id;
            Name = name;
            Status = status;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}