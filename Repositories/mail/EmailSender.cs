using Microsoft.AspNetCore.Identity.UI.Services; // Add this namespace
using Microsoft.EntityFrameworkCore; // For EF Core
using Repositories;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StoreApp.Mail
{
    public class EmailSender : IEmailSender // Use Microsoft’s interface
    {
        private readonly RepositoryContext _context;

        public EmailSender(RepositoryContext context)
        {
            _context = context;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Pull SMTP settings from the database
            var emailSettings = await _context.EmailSettings.FirstOrDefaultAsync();
            if (emailSettings == null)
            {
                throw new Exception("Email settings not found.");
            }

            var smtpClient = new SmtpClient
            {
                Host = emailSettings.SmtpHost,
                Port = emailSettings.SmtpPort,
                EnableSsl = emailSettings.EnableSsl,
                Credentials = new NetworkCredential(emailSettings.SenderEmail, emailSettings.SenderPassword)
            };

            using (var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings.SenderEmail),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            })
            {
                mailMessage.To.Add(email);
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}