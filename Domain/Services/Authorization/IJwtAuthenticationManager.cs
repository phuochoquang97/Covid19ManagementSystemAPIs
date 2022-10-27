namespace Covid_Project.Domain.Services.Authorization
{
    public interface IJwtAuthenticationManager
    {
         string Authenticate(int accountId, string email);
    }
}