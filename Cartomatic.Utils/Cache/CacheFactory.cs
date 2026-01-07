namespace Cartomatic.Utils.Cache
{
    /// <summary>
    /// simple cache factory
    /// </summary>
    public class CacheFactory
    {
        /// <summary>
        /// creates a new instance o cache object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheType"></param>
        /// <param name="cacheTimeout">miliseconds</param>
        /// <returns></returns>
        public static ICache<T> CreateCache<T>(CacheType cacheType, int cacheTimeout)
        {
            switch (cacheType)
            {
                default:
                    return new InMemoryCache<T>(cacheTimeout);

                case CacheType.Redis:
                    return new RedisCache<T>(cacheTimeout);
            }
        }
    }
}
