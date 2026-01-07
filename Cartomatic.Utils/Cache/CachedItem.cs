namespace Cartomatic.Utils.Cache
{
    /// <summary>
    /// a generic cached item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CachedItem<T>
    {
        /// <summary>
        /// Item retrieved from cache
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        /// Whether or not an item exists in cache
        /// </summary>
        public bool Exists { get; set; }

        /// <summary>
        /// Whether or not the cached item has timed out
        /// </summary>
        public bool TimedOut { get; set; }

        /// <summary>
        /// Whether or not the cached item is valid
        /// </summary>
        public bool Valid => Exists && !TimedOut;
    }
}
