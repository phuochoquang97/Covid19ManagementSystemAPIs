using System.Collections.Generic;

namespace Covid_Project.Domain.Models.DTOs
{

    public class TestingMedicalInfo
    {
    public TestingMedicalInfo(TestingResultDto testingResultDto, MedicalInfoDto medicalInfo)
    {
        this.testingResultDto = testingResultDto;
        this.medicalInfo = medicalInfo;    
    }
        public TestingResultDto testingResultDto;
        public MedicalInfoDto medicalInfo;
    }
}