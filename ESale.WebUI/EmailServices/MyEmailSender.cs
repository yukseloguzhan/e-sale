using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.EmailServices
{
    public class MyEmailSender : IEmailSender
    {

        private const string SendGridApiKey = "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";

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(SendGridApiKey, subject, htmlMessage ,email);
        }

        private Task Execute(string sendGridApiKey, string subject, string message , string email)
        {
            var client = new SendGridClient(sendGridApiKey);

            var msg = new SendGridMessage()
            {
                 From = new EmailAddress("yukseloguzhan53@gmail.com", "ESale Application"),  // mesajın nerden geldiğini söylüyoruz
                 Subject = subject,
                 PlainTextContent = message,
                 HtmlContent = message
            };


            msg.AddTo(new EmailAddress(email));   // nereye gidicek
            return client.SendEmailAsync(msg);

        }
    }
}
