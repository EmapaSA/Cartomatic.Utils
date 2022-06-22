using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Cartomatic.Utils.Email.MailKit
{
    /// <summary>
    /// Email sending functionality
    /// </summary>
    public class EmailSender: IEmailSender
    {
        /// <inheritdoc />
        /// <remarks>Uses Mailkit as an smtp client</remarks>
        public void Send(IEmailAccount emailAccount, IEmailTemplate emailTemplate, string recipient)
            => Send(emailAccount, emailTemplate, recipient, null);

        /// <inheritdoc />
        public void Send(IEmailAccount emailAccount, IEmailTemplate emailTemplate, string recipient, Action<string> logger)
        {
            Task.Run(async () =>
            {
                await SendAsync(emailAccount, emailTemplate, recipient, logger);
            });
        }

        /// <inheritdoc />
        public Task SendAsync(IEmailAccount emailAccount, IEmailTemplate emailTemplate, string recipient)
            => SendAsync(emailAccount, emailTemplate, recipient, null);

        /// <inheritdoc />
        public async Task SendAsync(IEmailAccount emailAccount, IEmailTemplate emailTemplate, string recipient, Action<string> logger)
        {
            await Task.Run(() =>
            {
                var msg = new MimeMessage();
                msg.To.Add(new MailboxAddress(recipient));
                msg.From.Add(new MailboxAddress(emailAccount.Sender));

                msg.Subject = emailTemplate.Title;
                msg.Body = new TextPart(emailTemplate.IsBodyHtml ? TextFormat.Html : TextFormat.Plain)
                {
                    Text = emailTemplate.Body
                };

                using (var emailClient = new SmtpClient())
                {
                    try
                    {
                        //emailClient.Connect(emailAccount.SmtpHost, emailAccount.SmtpPort ?? 587, true);
                        //looks like outlook mailkit does not like the enforce secure connection when talking to outlook online
                        //i guess outlook will enforce the tsl anyway
                        emailClient.Connect(emailAccount.SmtpHost, emailAccount.SmtpPort ?? 587);

                        //not using oauth, so remove it!
                        emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                        emailClient.Authenticate(emailAccount.User, emailAccount.Pass);

                        emailClient.Send(msg);

                        emailClient.Disconnect(true);
                    }
                    catch
                    {
                        //ignore
                    }

                }
            });
        }
    }
}
