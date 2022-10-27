namespace Covid_Project.Domain.Models.Email
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
    }
}