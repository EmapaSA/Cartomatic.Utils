using System;
using System.Collections.Generic;


namespace Cartomatic.Utils.Cache
{
    /// <summary>
    /// Redis cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RedisCache<T> : AbstractCache<T>
    {
#pragma warning disable 1591
        public RedisCache(int cacheTimeout)
            : base (CacheType.Redis, cacheTimeout)
        {
        }
#pragma warning restore 1591

        /// <summary>gets an object from cache</summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public override CachedItem<T> Get(string cacheKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>sets an object in cache</summary>
        /// <param name="cacheKey"></param>
        /// <param name="obj"></param>
        public override void Set(string cacheKey, T obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>Cleans the entire cache</summary>
        public override void Clean()
        {
            throw new NotImplementedException();
        }

        /// <summary>Cleans a cache item by key</summary>
        /// <param name="key"></param>
        public override void CleanByKey(string key)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetKeys(string filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
