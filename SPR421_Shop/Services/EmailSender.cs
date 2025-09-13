using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace SPR421_Shop.Services
{
    public class EmailSender : IEmailSender
    {
        private string _fromEmail = "";
        private string _password = "";
        private string _host = "smtp.gmail.com";
        private int _port = 587;
        private readonly SmtpClient _smtpClient;

        public EmailSender()
        {
            _smtpClient = new SmtpClient(_host, _port);
            _smtpClient.Credentials = new NetworkCredential(_fromEmail, _password);
            _smtpClient.EnableSsl = true;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage();
            message.From = new MailAddress(_fromEmail);
            message.To.Add(email);
            message.Subject = subject;
            message.Body = htmlMessage;
            message.IsBodyHtml = true;

            await _smtpClient.SendMailAsync(message);
        }
    }
}
