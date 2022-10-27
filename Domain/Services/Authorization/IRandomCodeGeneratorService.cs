namespace Covid_Project.Domain.Services.Authorization
{
    public interface IRandomCodeGeneratorService
    {
         string GenerateRandomCode(int codeLength);
    }
}