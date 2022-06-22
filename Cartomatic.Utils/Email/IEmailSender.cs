using System;
using System.Threading.Tasks;

namespace Cartomatic.Utils.Email
{
    /// <summary>
    /// Email sender
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount"></param>
        /// <param name="emailTemplate"></param>
        /// <param name="recipient"></param>
        void Send(IEmailAccount emailAccount, IEmailTemplate emailTemplate, string recipient);

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount"></param>
        /// <param name="emailTemplate"></param>
        /// <param name="recipient"></param>
        /// <param name="logger"></param>
        void Send(IEmailAccount emailAccount, IEmailTemplate emailTemplate, string recipient, Action<string> logger);

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount"></param>
        /// <param name="emailTemplate"></param>
        /// <param name="recipient"></param>
        Task SendAsync(IEmailAccount emailAccount, IEmailTemplate emailTemplate, string recipient);

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount"></param>
        /// <param name="emailTemplate"></param>
        /// <param name="recipient"></param>
        /// <param name="logger"></param>
        Task SendAsync(IEmailAccount emailAccount, IEmailTemplate emailTemplate, string recipient, Action<string> logger);
    }
}