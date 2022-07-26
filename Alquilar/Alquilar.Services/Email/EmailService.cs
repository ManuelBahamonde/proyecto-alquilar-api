using Alquilar.Models;
using Alquilar.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace Alquilar.Services
{
    public class EmailService
    {
        #region Members
        private readonly EmailSettings _emailSettings;
        #endregion

        #region Constructor
        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }
        #endregion

        #region Methods
        public void SendEmail(SendEmailRequest sendEmailRequest)
        {
            // Building Email
            //var email = new MimeMessage
            //{
            //    Subject = ReplaceTagsFields(sendEmailRequest.Subject, sendEmailRequest.Tags),
            //    Body = BuildHtmlMessageBody(sendEmailRequest.Body, sendEmailRequest.Tags)
            //};
            //email.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Mail));
            //email.To.Add(MailboxAddress.Parse(sendEmailRequest.To));

            //// Sending Email
            //using var smtp = new SmtpClient();
            //smtp.Connect(_emailSettings.Host, _emailSettings.Port);
            //smtp.Authenticate(_emailSettings.Mail, _emailSettings.Password);
            //smtp.Send(email);
            //smtp.Disconnect(true);
        }
        #endregion

        #region Private Helpers
        private MimeEntity BuildHtmlMessageBody(string htmlBody, Dictionary<string, string> tags)
        {
            var parsedHtmlBody = ReplaceTagsFields(htmlBody, tags);

            var builder = new BodyBuilder
            {
                HtmlBody = parsedHtmlBody
            };

            return builder.ToMessageBody();
        }

        private static string ReplaceTagsFields(string text, Dictionary<string, string> tags)
        {
            return tags.Keys.Aggregate(text, (parsedText, currentKey) => parsedText.Replace(currentKey, tags[currentKey]));
        }
        #endregion
    }
}
