using System;
namespace Covid_Project.Domain.Models.DTOs
{
    public class TestingRegisterDto
    {
        public TestingRegisterDto()
        {
            this.RegisterDate = DateTime.UtcNow;
        }
        public int TestingLocationId { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime TestingDate { get; set; }

    }
}