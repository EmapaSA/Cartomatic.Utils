using System;
using System.Reflection;

namespace Cartomatic.Utils.Reflection
{
    /// <summary>
    /// Type extensions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks whether or not a specified type has a property with given name. When binding flags are not customized, search is for case invariant public static or instance properties
        /// </summary>
        /// <param name="t"></param>
        /// <param name="pName"></param>
        /// <param name="bindingFlags">defaults to BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase</param>
        /// <returns></returns>
        public static bool HasProperty(
            this Type t,
            string pName,
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase
        )
        {
            return t.GetProperty(pName, bindingFlags) != null;
        }
    }
}
