using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cartomatic.Utils.JsonSerializableObjects
{
    /// <summary>
    /// Wrapper object that can save dictionaries in a form of associative arrays
    /// 
    /// Need to make the type non-generic in order to use it with EF mapping, so MyType : SerializableList of T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SerializableDictionary<T> : Dictionary<string, T>, IJsonSerializable
    {
#pragma warning disable 1591
        [JsonIgnore]
        public string Serialized
        {
            get => this.Serialize();
            set => this.Deserialize<T>(value);
        }
#pragma warning restore 1591
    }
}
