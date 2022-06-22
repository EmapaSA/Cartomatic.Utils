using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Cartomatic.Utils.Email
{
    /// <summary>
    /// Email sending functionality
    /// </summary>
    public class EmailSender : IEmailSender
    {
        public void Send(IEmailAccount emailAccount, IEmailTemplate emailTemplate, string recipient)
            => Send(emailAccount, emailTemplate, recipient, null);

        /// <summary>
        /// Sends an email to the recipient. Email is sent in a fire'n'forget manner.
        /// Note: in some scenarios fire'n'forget means the email may not eventually be sent at all.
        /// </summary>
        /// <param name="emailAccount">EmailAccount deails</param>
        /// <param name="emailTemplate">Email data to be sent out</param>
        /// <param name="recipient">Email of a recipient</param>
        public void Send(IEmailAccount emailAccount, IEmailTemplate emailTemplate, string recipient, Action<string> logger)
        {
            Task.Run(async () =>
                {
                    await SendAsync(emailAccount, emailTemplate, recipient, logger);
                }
            );
        }

        public Task SendAsync(IEmailAccount emailAccount, IEmailTemplate emailTemplate, string recipient)
            => SendAsync(emailAccount, emailTemplate, recipient, null);

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount"></param>
        /// <param name="emailTemplate"></param>
        /// <param name="recipient"></param>
        /// <returns></returns>
        public async Task SendAsync(IEmailAccount emailAccount, IEmailTemplate emailTemplate, string recipient, Action<string> logger)
        {
            await Task.Run(() =>
            {
                var mail = new MailMessage();

                mail.To.Add(recipient);

                mail.From = new MailAddress(emailAccount.Sender);

                mail.Subject = emailTemplate.Title;
                mail.SubjectEncoding = Encoding.UTF8;

                mail.Body = emailTemplate.Body;
                mail.IsBodyHtml = emailTemplate.IsBodyHtml;
                mail.BodyEncoding = Encoding.UTF8;

                var smtp = new SmtpClient
                {
                    Host = emailAccount.SmtpHost,
                    Port = emailAccount.SmtpPort ?? 587,
                    Credentials = new System.Net.NetworkCredential(emailAccount.User, emailAccount.Pass),
                    EnableSsl = emailAccount.Ssl ?? false
                };

                var now = DateTime.Now.Ticks;
                var actionId = Guid.NewGuid();

                try
                {
                    logger?.Invoke(
                        $"{actionId} :: Attempting to send email to {recipient}; time start: {DateTime.Now.ToShortDateString()}-{DateTime.Now.ToShortTimeString()}");

                    smtp.Send(mail);

                    logger?.Invoke(
                        $"{actionId} :: Email sent to {recipient}; Time taken in seconds: {new TimeSpan(DateTime.Now.Ticks - now).TotalSeconds}");
                }
                catch (Exception ex)
                {
                    logger?.Invoke(
                        $"{actionId} :: Failed to send emails to {recipient}; Time taken in seconds: {new TimeSpan(DateTime.Now.Ticks - now).TotalSeconds}");
                    logger?.Invoke(
                        $"Sender details - host: {emailAccount.SmtpHost}, port: {emailAccount.SmtpPort}, sender: {emailAccount.Sender}, user: {emailAccount.User}, pass: {emailAccount.Pass}, ssl: {emailAccount.Ssl}");

                    var e = ex;
                    var tab = string.Empty;

                    while (e != null)
                    {
                        logger?.Invoke($"{tab}{e.Message}");
                        tab += '\t';
                        e = e.InnerException;
                    }
                }
            });
        }
    }
}
