using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cartomatic.Utils.Sorting
{
    /// <summary>
    /// read sorter extensions
    /// </summary>
    public static partial class ReadSorterExtensions
    {
        /// <summary>
        /// deserializes extjs filter into list of ReadFilter
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<ReadSorter> ExtJsJsonSortersToReadSorters(this string json)
        {
            return string.IsNullOrEmpty(json) ?
                new List<ReadSorter>() :
                JsonConvert.DeserializeObject<List<ReadSorter>>(json);
        }

        /// <summary>
        /// Converts filters collection to json string
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static string ReadSortersToExtJsJson(this IEnumerable<ReadSorter> filters)
        {
            return JsonConvert.SerializeObject(filters);
        }
    }
}
