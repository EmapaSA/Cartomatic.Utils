using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Cartomatic.Utils.RestSharpSerializers
{
    /// <summary>
    /// Custom RestSharp serializer utilising NewtonSoft.JSON
    /// <para />
    /// partially based on https://raw.githubusercontent.com/restsharp/RestSharp/86b31f9adf049d7fb821de8279154f41a17b36f7/RestSharp/Serializers/JsonSerializer.cs
    /// </summary>
    public class NewtonSoftJsonSerializer : ISerializer, IDeserializer
    {
        private Formatting Formatting { get; set; }

        private JsonSerializerSettings JsonSerializerSettings { get; set; }

        /// <summary>
        /// Creates a new instance with Formatting.None and null value removal when serializing
        /// </summary>
        public NewtonSoftJsonSerializer()
            : this(Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            })
        {
        }

        /// <summary>
        /// Creates a new instance of the serializer with the supplied settings
        /// </summary>
        /// <param name="formatting"></param>
        /// <param name="settings"></param>
        public NewtonSoftJsonSerializer(Newtonsoft.Json.Formatting formatting, JsonSerializerSettings settings)
        {
            ContentType = "application/json";
            Formatting = formatting;
            JsonSerializerSettings = settings;
        }


        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string DateFormat { get; set; }
        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string RootElement { get; set; }
        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string Namespace { get; set; }
        /// <summary>
        /// Content type for serialized content
        /// </summary>
        public string ContentType { get; set; }


        /// <inheritdoc />
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting, JsonSerializerSettings);
        }

        /// <inheritdoc />
        public T Deserialize<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}