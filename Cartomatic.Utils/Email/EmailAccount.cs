using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cartomatic.Utils.Email
{
    /// <summary>
    /// Email account details needed to connect to the smtp server
    /// </summary>
    public class EmailAccount : IEmailAccount
    {
        /// <inheritdoc />
        public string Sender { get; set; }

        /// <inheritdoc />
        public string SenderName { get; set; }

        /// <inheritdoc />
        public string SmtpHost { get; set; }

        /// <inheritdoc />
        public int? SmtpPort { get; set; }

        /// <inheritdoc />
        public string User { get; set; }

        /// <inheritdoc />
        public string Pass { get; set; }

        /// <inheritdoc />
        public bool? Ssl { get; set; }

        /// <summary>
        /// Whether or not email account seems complete - can send out emails
        /// </summary>
        /// <returns></returns>
        public bool SeemsComplete()
        {
            return !string.IsNullOrWhiteSpace(Sender) &&
                   !string.IsNullOrWhiteSpace(SmtpHost) &&
                   !string.IsNullOrWhiteSpace(User) &&
                   !string.IsNullOrWhiteSpace(Pass);
        }

        /// <summary>
        /// Creates instance from a json string; throws if json is not valid
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static EmailAccount FromJson(string json)
        {
            return JsonConvert.DeserializeObject<EmailAccount>(json);
        }

        /// <summary>
        /// serializes object to url query string
        /// </summary>
        /// <returns></returns>
        public string ToQuery()
        {
            return string.Join("&", new[]
            {
                $"{nameof(Sender)}={Sender}",
                $"{nameof(SmtpHost)}={SmtpHost}",
                $"{nameof(SmtpPort)}={SmtpPort}",
                $"{nameof(User)}={User}",
                $"{nameof(Pass)}={Pass}",
                $"{nameof(Ssl)}={Ssl}"
            });
        }

        /// <summary>
        /// converts object to a dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { nameof(Sender), Sender },
                { nameof(SmtpHost), SmtpHost },
                { nameof(SmtpPort), SmtpPort },
                { nameof(User), User },
                { nameof(Pass), Pass },
                { nameof(Ssl), Ssl }
            };
        }
    }
}
