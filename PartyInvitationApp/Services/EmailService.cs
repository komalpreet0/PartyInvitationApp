using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace PartyInvitationApp.Services
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _senderEmail;
        private readonly string _senderPassword;
        private readonly bool _enableSsl;

        public EmailService(IConfiguration configuration)
        {
            try
            {
                var emailSettings = configuration.GetSection("EmailSettings");

                // Ensure no null values are assigned
                _smtpServer = emailSettings["SmtpServer"] ?? throw new ArgumentNullException("SmtpServer is missing in configuration.");
                _smtpPort = int.TryParse(emailSettings["SmtpPort"], out int port) ? port : throw new ArgumentNullException("SmtpPort is invalid.");
                _senderEmail = emailSettings["SenderEmail"] ?? throw new ArgumentNullException("SenderEmail is missing in configuration.");
                _senderPassword = emailSettings["SenderPassword"] ?? throw new ArgumentNullException("SenderPassword is missing in configuration.");
                _enableSsl = bool.TryParse(emailSettings["EnableSsl"], out bool ssl) ? ssl : throw new ArgumentNullException("EnableSsl is invalid.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error loading email settings: {ex.Message}");
                throw; // Stop execution if email settings are incorrect
            }
        }

        public async Task SendInvitationEmail(string toEmail, string subject, string body)
        {
            try
            {
                Console.WriteLine("📤 Sending email...");
                Console.WriteLine($"SMTP Server: {_smtpServer}, Port: {_smtpPort}, SSL: {_enableSsl}");
                Console.WriteLine($"From: {_senderEmail} -> To: {toEmail}");

                using (var client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
                    client.EnableSsl = _enableSsl;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_senderEmail),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(toEmail);

                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine("✅ Email successfully sent!");
                }
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"❌ SMTP Error: {smtpEx.Message}");
                if (smtpEx.InnerException != null)
                    Console.WriteLine($"➡ Inner Exception: {smtpEx.InnerException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Email sending failed: {ex.Message}");
            }
        }
    }
}
