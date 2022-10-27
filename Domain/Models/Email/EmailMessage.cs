using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Covid_Project.Domain.Models.Email
{
    public class EmailMessage
    {
        public EmailMessage(string toMail, string subject, string body)
        {
            ToEmail = toMail;
            Subject = subject;
            Body = body;
        }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}