using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace Cartomatic.Utils.Data
{

    /// <summary>
    /// Holds information allowing for connecting to a datasource
    /// </summary>
    public class DataSourceCredentials : IDataSourceCredentials
    {

        /// <summary>
        /// Type of the data source the credentials apply to
        /// </summary>
        public DataSourceProvider DataSourceProvider { get; set; }

        /// <inheritdoc />
        public string ServerHost { get; set; }

        /// <inheritdoc />
        public string ServerName { get; set; }

        /// <inheritdoc />
        public int? ServerPort { get; set; }

        /// <inheritdoc />
        public string DbName { get; set; }

        /// <inheritdoc />
        public string ServiceDb { get; set; }

        /// <inheritdoc />
        public string UserName { get; set; }

        /// <inheritdoc />
        public string Pass { get; set; }

        /// <inheritdoc />
        public string ServiceUserName { get; set; }

        /// <inheritdoc />
        public string ServiceUserPass { get; set; }



        /// <summary>
        /// Whether or not default service Db name should be used when db name not provided - applicable to Npgsql only
        /// </summary>
        public bool UseDefaultServiceDb { get; set; }

        /// <summary>
        /// other connection settings as read from a connection string
        /// </summary>
        private List<string> OtherConnSettings { get; set; }

#pragma warning disable 1591
        public DataSourceCredentials()
        {
            UseDefaultServiceDb = true;
        }

        public DataSourceCredentials(string connStr, DataSourceProvider? provider = null)
        {
            var connStrParams = ExtractConnStrParams(connStr);
            DataSourceProvider = provider ?? GetDataSourceProvider(connStrParams);

            if (connStrParams.ContainsKey("server"))
                ServerHost = connStrParams["server"];

            if (connStrParams.ContainsKey("data source"))
                ServerHost = connStrParams["data source"];

            if (connStrParams.ContainsKey("port") && int.TryParse(connStrParams["port"], out var port))
                ServerPort = port;

            if (connStrParams.ContainsKey("database"))
                DbName = connStrParams["database"];

            if (connStrParams.ContainsKey("user id"))
                UserName = connStrParams["user id"];

            if (connStrParams.ContainsKey("password"))
                Pass = connStrParams["password"];

            //oracle should use uid / pwd

            //Post process...
            if (DataSourceProvider == DataSourceProvider.SqlServer)
            {
                var hostAndPort = ServerHost.Split(',');
                ServerHost = hostAndPort.First();
                if (hostAndPort.Length > 1 && int.TryParse(hostAndPort[1], out var port1))
                    ServerPort = port1;
            }
        }
#pragma warning restore 1591

        private Dictionary<string, string> ExtractConnStrParams(string connStr)
        {
            return connStr
                .Split(';')
                .Select(x => x.Split('='))
                .ToDictionary(
                    x => x[0].Trim().ToLower(),
                    x => x.Length > 1 ? x[1] : null
                );
        }

        private DataSourceProvider GetDataSourceProvider(Dictionary<string, string> connStrParams)
        {
            //decide on the provider based on the connection string params
            //note: this may a bit naive but works for current needs; will be extended as required

            //by default use sql server as it has no specific keys really
            var provider = DataSourceProvider.SqlServer;

            if (connStrParams.ContainsKey("port"))
                provider = DataSourceProvider.Npgsql;


            if (connStrParams.ContainsKey("uid") || connStrParams.ContainsKey("pwd"))
                provider = DataSourceProvider.Oracle;


            return provider;
        }

        /// <summary>
        /// Returns a default Db name
        /// </summary>
        /// <returns></returns>
        protected internal string GetDefaultDbName()
        {
            string dbName = string.Empty;

            switch (DataSourceProvider)
            {
                case DataSourceProvider.Npgsql:
                    dbName = "postgres";
                    break;
            }

            return dbName;
        }

        /// <summary>
        /// Returns a connection string for the configured data source type
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString(bool serviceDatabase = false, bool superUser = false, bool pooling = true)
        {
            var conn = "INVALID CONNECTION STRING";

            switch (DataSourceProvider)
            {
                case DataSourceProvider.Npgsql:
                    conn =
                        "Server=" + ServerHost + ";" +
                        "Port=" + ServerPort + ";" +
                        "Database=" + (serviceDatabase
                                ? (string.IsNullOrEmpty(ServiceDb) ? "postgres" : ServiceDb)
                                : (string.IsNullOrEmpty(DbName) ? (UseDefaultServiceDb ? "postgres" : "") : DbName)) + ";" +
                        "user id=" + (serviceDatabase || superUser
                            ? (string.IsNullOrEmpty(ServiceUserName) ? "postgres" : ServiceUserName)
                            : UserName) + ";" +
                        "password=" + (serviceDatabase || superUser
                            ? (string.IsNullOrEmpty(ServiceUserPass) ? "postgres" : ServiceUserPass)
                            : Pass) + ";" +
                        (pooling ? string.Empty : "Pooling=false;");
                    break;

                case DataSourceProvider.SqlServer:
                    conn =
                        "server=" + ServerHost + (ServerPort.HasValue ? "," + ServerPort : "") + ";" +
                        "user id=" + (serviceDatabase || superUser ? ServiceUserName : UserName) + ";" +
                        "password=" + (serviceDatabase || superUser ? ServiceUserPass : Pass) + ";" +
                        "database=" + (serviceDatabase ? ServiceDb : DbName) + ";";
                    break;

                case DataSourceProvider.Oracle:
                    throw new NotImplementedException();
                    //break;
            }

            if (OtherConnSettings?.Any() == true)
                conn += string.Join(";", OtherConnSettings);

            return conn;
        }

        /// <summary>
        /// Creates a copy of the object
        /// </summary>
        /// <returns></returns>
        public DataSourceCredentials Clone()
        {
            return (DataSourceCredentials)MemberwiseClone();
        }
    }

}