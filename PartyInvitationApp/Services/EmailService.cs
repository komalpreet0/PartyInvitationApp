using System.Net;
using System.Net.Mail;

namespace PartyInvitationApp.Services
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly string _senderEmail;
        private readonly string _senderPassword;
        private readonly int _smtpPort;

        public EmailService()
        {
            _smtpServer = "smtp.example.com"; // Change to your SMTP server
            _senderEmail = "your-email@example.com"; // Change to your email
            _senderPassword = "yourpassword"; // Change to your password
            _smtpPort = 587; // Change if necessary
        }

        public async Task SendInvitationEmail(string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
                client.EnableSsl = true;

                var message = new MailMessage(_senderEmail, toEmail, subject, body);
                message.IsBodyHtml = true;
                await client.SendMailAsync(message);
            }
        }
    }
}
