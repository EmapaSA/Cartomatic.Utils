using System;
using Newtonsoft.Json;

namespace Cartomatic.Utils.SafeLink
{
#pragma warning disable 1591
    public class Link
    {
        public Link()
        {
            ResourceType = ResourceType.File;
        }
#pragma warning restore 1591

        /// <summary>
        /// Creates a new instance from encoded link data
        /// </summary>
        /// <param name="encoded"></param>
        /// <param name="key"></param>
        public Link(string encoded, string key)
        {
            Decrypt(encoded, key);
        }

        /// <summary>
        /// serializer settings 
        /// </summary>
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            //ensure dates are handled as ISO 8601
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };


        /// <summary>
        /// a type of resource represented by a link; defaults to File
        /// </summary>
        public ResourceType ResourceType { get; set; }

        /// <summary>
        /// a resource a link represents
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// The expiration time after which a link will no longer be valid; this is serialized as UTC, so a link can be properly validated
        /// against its expiration date on another machine too.
        /// </summary>
        public DateTime ExpirationTime { get; set; }


        /// <summary>
        /// Decodes link data and sets the instance properties
        /// </summary>
        /// <param name="encoded"></param>
        /// <param name="key"></param>
        protected void Decrypt(string encoded, string key)
        {
            var json = Crypto.SymmetricEncryption.Decrypt(encoded, key);
            try
            {
                var linkData = JsonConvert.DeserializeObject<Link>(json, _serializerSettings);
                ExpirationTime = linkData.ExpirationTime.ToLocalTime(); //serialised as utc!
                Resource = linkData.Resource;
                ResourceType = linkData.ResourceType;
            }
            catch
            {
                //ignore
            }
        }

        /// <summary>
        /// Encodes link data to string
        /// </summary>
        /// <returns></returns>
        public string Encrypt(string key)
        {
            //basically serialize to json an encrypt
            var json = JsonConvert.SerializeObject(this, _serializerSettings);
            return Crypto.SymmetricEncryption.Encrypt(json, key);
        }

        /// <summary>
        /// Whether or not a link is expired
        /// </summary>
        public bool IsExpired => DateTime.Now > ExpirationTime;
    }
}
