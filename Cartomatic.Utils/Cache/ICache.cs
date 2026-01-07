using System.Collections.Generic;


namespace Cartomatic.Utils.Cache
{
    /// <summary>
    /// Generic cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICache<T>
    {
        /// <summary>
        /// Cache timeout in millis
        /// </summary>
        int CacheTimeout { get; }

        /// <summary>
        /// Cache type
        /// </summary>
        CacheType CacheType { get; }

        /// <summary>
        /// gets an object from cache
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        CachedItem<T> Get(string cacheKey);

        /// <summary>
        /// sets an object in cache
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="obj"></param>
        void Set(string cacheKey, T obj);

        /// <summary>
        /// Cleans the entire cache
        /// </summary>
        void Clean();

        /// <summary>
        /// Cleans a cache item by key
        /// </summary>
        /// <param name="key"></param>
        void CleanByKey(string key);

        /// <summary>
        /// Gets cached keys
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<string> GetKeys(string filter = null);
    }
}
