using System;
using System.Security.Cryptography;

namespace Cartomatic.Utils.ApiClient
{
    /// <summary>
    /// Api client configurarion
    /// </summary>
    public abstract class ApiClientConfiguration : IApiClientConfiguration
    {
        /// <summary>
        /// Protocol
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// Host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Pass
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Request timeout
        /// </summary>
        public int? Timeout { get; set; }

        /// <summary>
        /// API version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Returns the configured client endpoint url 
        /// </summary>
        /// <returns></returns>
        public virtual string GetUrl()
        {
            return $"{(string.IsNullOrEmpty(Protocol) ? "http" : Protocol)}://{Host}{(Port.HasValue ? $":{Port}" : "")}";
        }

        private string _endPointId;

        /// <summary>
        /// Gets an identifier of an endpoint. This is pretty much a server identifier as it is based on the combination of protocol, host and port and this is unique per server.
        /// <para />
        /// Two configurations with the same settings actually point to the very same endpoint
        /// </summary>
        public virtual string EndPointId
        {
            get
            {
                if (string.IsNullOrEmpty(_endPointId))
                {
                    //get an array of bytes off the string
                    byte[] sb = System.Text.Encoding.UTF8.GetBytes(GetUrl());

                    //container for hashed string
                    byte[] ob;

                    using (SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider())
                    {
                        ob = sha.ComputeHash(sb);
                    }

                    //remove the dashes and make string lowercase
                    _endPointId = BitConverter.ToString(ob).Replace("-", "").ToLower();
                }

                return _endPointId;
            }
        }
    }
}
