using System;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace Cartomatic.Utils.Cache
{
    /// <summary>
    /// in memory cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InMemoryCache<T> : AbstractCache<T>
    {
        private readonly ConcurrentDictionary<string, (T obj, long timeSet)> _cache;

        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="cacheTimeout">miliseconds</param>
        public InMemoryCache(int cacheTimeout)
            : base (CacheType.InMemory, cacheTimeout)
        {
            _cache = new ConcurrentDictionary<string, (T, long)>();
        }

        /// <summary>gets an object from cache</summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public override CachedItem<T> Get(string cacheKey)
        {
            var success = _cache.TryGetValue(cacheKey, out var cached);

            return new CachedItem<T>
            {
                Item = success ? cached.obj : default(T),
                TimedOut = success && DateTime.Now.Ticks - cached.timeSet >= CacheTimeoutTicks,
                Exists = success
            };
        }

        /// <summary>sets an object in cache</summary>
        /// <param name="cacheKey"></param>
        /// <param name="obj"></param>
        public override void Set(string cacheKey, T obj)
        {
            CleanByKey(cacheKey);
            _cache.TryAdd(cacheKey, (obj, DateTime.Now.Ticks));
        }

        /// <summary>Cleans the entire cache</summary>
        public override void Clean()
        {
            _cache.Clear();
        }

        /// <summary>Cleans a cache item by key</summary>
        /// <param name="key"></param>
        public override void CleanByKey(string key)
        {
            _cache.TryRemove(key, out _);
        }

        /// <inheritdoc />
        public override IEnumerable<string> GetKeys(string filter =  null)
        {
            var outKeys = new List<string>();
            var searchableFilter = string.IsNullOrEmpty(filter)
                ? null
                : filter.ToLowerInvariant();

            foreach (var cacheKey in _cache.Keys)
            {
                if (string.IsNullOrEmpty(searchableFilter) || cacheKey.ToLowerInvariant().Contains(searchableFilter))
                {
                    outKeys.Add(cacheKey);
                }
            }

            return outKeys;
        }
    }
}
