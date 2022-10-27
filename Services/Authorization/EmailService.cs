using System;
using System.IO;
using Covid_Project.Domain.Models.Email;
using Covid_Project.Domain.Services.Authorization;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Covid_Project.Services.Authorization
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _mailConfiguration;
        public EmailService(IOptions<EmailConfiguration> mailConfiguration)
        {
            _mailConfiguration = mailConfiguration.Value;

        }
        public bool SendEmail(EmailMessage message)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailConfiguration.From);
                email.To.Add(MailboxAddress.Parse(message.ToEmail));
                email.Subject = message.Subject;
                var builder = new BodyBuilder();
                if(message.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach(var file in message.Attachments)
                    {
                        if(file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();
                            }
                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }
                builder.HtmlBody = message.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailConfiguration.SmtpServer,
                            _mailConfiguration.Port);
                smtp.Authenticate(_mailConfiguration.From,
                                _mailConfiguration.Password);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;

        }
    }
}