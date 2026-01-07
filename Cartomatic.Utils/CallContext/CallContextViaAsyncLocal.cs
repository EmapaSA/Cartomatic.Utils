using System.Collections.Concurrent;
using System.Threading;

namespace Cartomatic.Utils
{
#if NETSTANDARD2_0 || NETCOREAPP3_1 || NET5_0_OR_GREATER
    /// <summary>
    /// Provides a way to set contextual data that flows with the call and 
    /// async context of a test or invocation.
    /// borrowed from http://www.cazzulino.com/callcontext-netstandard-netcore.html
    /// </summary>
    public static class CallContext
    {
        private static readonly ConcurrentDictionary<string, AsyncLocal<object>> State = new ConcurrentDictionary<string, AsyncLocal<object>>();

        /// <summary>
        /// Stores a given object and associates it with the specified name.
        /// </summary>
        /// <param name="name">The name with which to associate the new item in the call context.</param>
        /// <param name="data">The object to store in the call context.</param>
        public static void SetData(string name, object data) =>
            State.GetOrAdd(name, _ => new AsyncLocal<object>()).Value = data;

        /// <summary>
        /// Retrieves an object with the specified name from the <see cref="CallContext"/>.
        /// </summary>
        /// <param name="name">The name of the item in the call context.</param>
        /// <returns>The object in the call context associated with the specified name, or <see langword="null"/> if not found.</returns>
        public static object GetData(string name) =>
            State.TryGetValue(name, out var data) ? data.Value : null;
    }

    /// <summary>
    /// Provides a way to set contextual data that flows with the call and 
    /// async context of a test or invocation.
    /// borrowed from http://www.cazzulino.com/callcontext-netstandard-netcore.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class CallContext<T>
    {
        private static readonly ConcurrentDictionary<string, AsyncLocal<T>> State = new ConcurrentDictionary<string, AsyncLocal<T>>();

        /// <summary>
        /// Stores a given object and associates it with the specified name.
        /// </summary>
        /// <param name="name">The name with which to associate the new item in the call context.</param>
        /// <param name="data">The object to store in the call context.</param>
        public static void SetData(string name, T data) =>
            State.GetOrAdd(name, _ => new AsyncLocal<T>()).Value = data;

        /// <summary>
        /// Retrieves an object with the specified name from the <see cref="CallContext"/>.
        /// </summary>
        /// <param name="name">The name of the item in the call context.</param>
        /// <returns>The object in the call context associated with the specified name, or a default value for <typeparamref name="T"/> if none is found.</returns>
        public static T GetData(string name) =>
            State.TryGetValue(name, out AsyncLocal<T> data) ? data.Value : default(T);
    }
#endif
}