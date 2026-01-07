using System;
using System.Collections.Generic;


namespace Cartomatic.Utils.Cache
{
    /// <summary>
    /// Provides facilities for caching user configuration
    /// </summary>
    public abstract class AbstractCache<T> : ICache<T>
    {
#pragma warning disable 1591
        protected AbstractCache()
            : this (CacheType.InMemory, 5000)
        { }
#pragma warning restore 1591

        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="cacheTimeout">Miliseconds</param>
        protected AbstractCache(CacheType cacheType, int cacheTimeout)
        {
            CacheType = cacheType;
            CacheTimeout = cacheTimeout;
        }
        
        /// <summary>
        /// cache timeout in miliseconds
        /// </summary>
        public int CacheTimeout { get; }

        /// <summary>
        /// cache timeout expressed in ticks
        /// </summary>
        protected virtual long CacheTimeoutTicks => TimeSpan.TicksPerMillisecond * CacheTimeout;

        /// <summary>
        /// Type of cache
        /// </summary>
        public CacheType CacheType { get; }


        /// <inheritdoc />
        public abstract CachedItem<T> Get(string cacheKey);

        /// <inheritdoc />
        public abstract void Set(string cacheKey, T obj);

        /// <inheritdoc />
        public abstract void Clean();

        /// <inheritdoc />
        public abstract void CleanByKey(string key);

        /// <inheritdoc />
        public abstract IEnumerable<string> GetKeys(string filter = null);
    }
}
