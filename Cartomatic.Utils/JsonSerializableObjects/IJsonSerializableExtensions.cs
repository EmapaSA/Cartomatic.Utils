using System.Collections.Generic;
using Cartomatic.Utils.Dto;
using Newtonsoft.Json;

namespace Cartomatic.Utils.JsonSerializableObjects
{
    /// <summary>
    /// extensions for IJsonSerializable
    /// </summary>
    public static class IJsonSerializableExtensions
    {
        /// <summary>
        /// serializaes a IJsonSerializable to json string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(this IJsonSerializable obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Deserializes a IJsonSerializable from json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="json"></param>
        public static void Deserialize<T>(this IJsonSerializable obj, string json)
        {
            if (obj is SerializableList<T>)
                DeserializeSerializableList((SerializableList<T>) obj, json);
            if (obj is SerializableDictionary<T>)
                DeserializeSerializableDict((SerializableDictionary<T>)obj, json);
            else
                DeserializeObject(obj, json);
        }

        /// <summary>
        /// Desrializes an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="json"></param>
        private static void DeserializeObject<T>(T obj, string json)
        {
            var data = default(T);
            try
            {
                data = JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                //ignore
            }

            //copy deserialised to self
            obj.CopyPublicPropertiesFrom(data);
        }

        /// <summary>
        /// Deserializes a serializable list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="json"></param>
        private static void DeserializeSerializableList<T>(SerializableList<T> obj, string json)
        {
            //deserialise
            var data = new List<T>();
            try
            {
                data = JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch
            {
                //ignore
            }

            //first make sure to clear self
            obj.Clear();

            obj.AddRange(data ?? new List<T>());
        }

        /// <summary>
        /// Deserializes a serializable dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="json"></param>
        private static void DeserializeSerializableDict<T>(SerializableDictionary<T> obj, string json)
        {
            //deserialise
            var data = new Dictionary<string, T>();
            try
            {
                data = JsonConvert.DeserializeObject<Dictionary<string, T>>(json);
            }
            catch
            {
                //ignore
            }

            //first make sure to clear self
            obj.Clear();

            foreach (var key in data.Keys)
            {
                obj.Add(key, data[key]);
            }
        }
    }
}
