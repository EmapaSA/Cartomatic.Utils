using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cartomatic.Utils.Filtering
{
    /// <summary>
    /// read filter extensions
    /// </summary>
    public static partial class ReadFilterExtensions
    {
        /// <summary>
        /// Deserializes ExtJs read filter json to a list of ReadFilter
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<ReadFilter> ExtJsJsonFiltersToReadFilters(this string json)
        {
            return string.IsNullOrEmpty(json) ?
                new List<ReadFilter>() :
                JsonConvert.DeserializeObject<List<ReadFilter>>(json);
        }

        /// <summary>
        /// Converts filters collection to json string
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static string ReadFiltersToExtJsJson(this IEnumerable<ReadFilter> filters)
        {
            return JsonConvert.SerializeObject(filters);
        }
    }
}
