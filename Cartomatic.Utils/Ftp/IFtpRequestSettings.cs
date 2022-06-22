using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Cartomatic.Utils.Ftp
{
    public interface IFtpRequestSettings
    {
        /// <summary>
        /// ftp method to execute; see WebRequestMethods.Ftp for details
        /// </summary>
        string Method { get; set; }

        /// <summary>
        /// Uri of ftp server
        /// </summary>
        string Uri { get; set; }

        /// <summary>
        /// Sub path
        /// </summary>
        string SubPath { get; set; }

        /// <summary>
        /// FTP user name
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// FTP user pass
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Specifies data type for file transfer; defaults to true
        /// </summary>
        bool? UseBinary { get; set; }

        /// <summary>
        /// behavior of client app data transfer process; defaults to true
        /// </summary>
        bool? UsePassive { get; set; }

        /// <summary>
        /// Whether or not a connection to the server should be closed after a request completes; defaults to true
        /// </summary>
        bool? KeepAlive { get; set; }

        /// <summary>
        /// Whether or not enable ssl explicitly (instead of implicitly via uri schema - ftps, fteps, etc)
        /// </summary>
        bool? EnableSsl { get; set; }

        /// <summary>
        /// Whether or not should ignore invalid ssl certs; this is handy when a server uses a self signed cert or another cert considered to be weak / untrusted
        /// </summary>
        bool? IgnoreInvalidSslCertificate { get; set; }
    }

    public class FtpRequestSettings : IFtpRequestSettings
    {
        /// <inheritdoc />
        public string Method { get; set; }

        /// <inheritdoc />
        public string Uri { get; set; }

        /// <inheritdoc />
        public string SubPath { get; set; }

        /// <inheritdoc />
        public string UserName { get; set; }

        /// <inheritdoc />
        public string Password { get; set; }

        /// <inheritdoc />
        public bool? UseBinary { get; set; }

        /// <inheritdoc />
        public bool? UsePassive { get; set; }

        /// <inheritdoc />
        public bool? KeepAlive { get; set; }

        /// <inheritdoc />
        public bool? EnableSsl { get; set; }

        /// <inheritdoc />
        public bool? IgnoreInvalidSslCertificate { get; set; }
    }
}
