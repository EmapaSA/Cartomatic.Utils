namespace Cartomatic.Utils.Sorting
{
    /// <summary>
    /// abstracts sort properties applied when reading data
    /// </summary>
    public partial class ReadSorter
    {
        /// <summary>
        /// Object property to sort on
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// ummm.... well... sort direction ;)
        /// </summary>
        public string Direction { get; set; }
    }
}
