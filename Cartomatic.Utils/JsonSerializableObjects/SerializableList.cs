using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cartomatic.Utils.JsonSerializableObjects
{
    /// <summary>
    /// EF does not allow saving lits of primitive types even though dbs such as PostgreSQL do allow it.
    /// Therefore need a wrapper object that is used to store json serialised arrays.
    /// Note: if array based ops on the db side are required, this will not be a good type to use.
    /// 
    /// Also, one need to make the type non-generic in order to use it with EF mapping, so MyType : SerializableList of T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SerializableList<T> : List<T>, IJsonSerializable
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
