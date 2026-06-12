using ECom.Core.DTO;
using ECom.Core.Services;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
namespace ECom.infrastrucrure.Repositries.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(EmailDto emailDto)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Ecom", _configuration["EmailSettings:From"]));
            email.Subject = emailDto.Subject;
            email.To.Add(new MailboxAddress(emailDto.To, emailDto.To));
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            { Text = emailDto.Content };

            // Send the email using SMTP
            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
         _configuration["EmailSettings:Host"],
         int.Parse(_configuration["EmailSettings:Port"]!),
         SecureSocketOptions.StartTls
     );
            await smtp.AuthenticateAsync(
             _configuration["EmailSettings:Username"],
             _configuration["EmailSettings:Password"]
         );
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

        }
    }
}
