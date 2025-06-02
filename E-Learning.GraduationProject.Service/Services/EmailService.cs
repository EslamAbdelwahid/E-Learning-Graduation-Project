using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(Email email)
        {
            var mailSettings = _config.GetSection("MailSettings");

            var fromEmail = mailSettings["Email"];
            var fromPassword = mailSettings["Password"];
            var host = mailSettings["Host"];
            var port = int.Parse(mailSettings["Port"]);

            var smtpClient = new SmtpClient(host, port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage(
                from: fromEmail,
                to: email.To,
                subject: email.Subject,
                body: email.Body
            )
            {
                IsBodyHtml = true
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
