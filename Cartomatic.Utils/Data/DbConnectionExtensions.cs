using System.Data;



namespace Cartomatic.Utils.Data
{
    /// <summary>
    /// DbConnection extensions
    /// </summary>
    public static class DbConnectionExtensions
    {
        /// <summary>
        /// Closes db connection 
        /// </summary>
        /// <param name="conn">Database connection object</param>
        /// <param name="dispose"></param>
        public static void CloseConnection(this IDbConnection conn, bool dispose = true)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();

                if(dispose)
                    conn.Dispose();
            }
        }
    }
}
