namespace Covid_Project.Domain.Models.DTOs
{
    public class MedicalInfoDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public bool Asthma { get; set; }
        public bool Pregnancy { get; set; }
        public bool HighBloodPressure { get; set; }
        public bool Obesity { get; set; }
        public bool HeartProblem { get; set; }
        public bool HIV { get; set; }
        public bool Cough { get; set; }
        public bool Fever { get; set; }
        public bool ShortnessOfBreath { get; set; }
        public bool RunningNose { get; set; }
        public bool Tiredness { get; set; }
        public bool None { get; set; }
        public string SpecialSymptom { get; set; }
    }
}