namespace Covid_Project.Domain.Models.DTOs
{
    public class TestingStatisticDto
    {
        public int NumberOfUsers { get; set; }
        public int PositiveCases { get; set; }
        public int NegativeCases { get; set; }
        public int PendingCases { get; set; }
    }
}