using System.Data;
using Npgsql;



namespace Cartomatic.Utils.Data
{
    /// <summary>
    /// DataSource credentials extensions
    /// </summary>
    public static class DataSourceCredentialsExtensions
    {
        //TODO - make it dynamically resolve types! So the util is not so tightly coupled with different drivers as it sucks!

        /// <summary>
        /// Returns a db engine specific Connection object
        /// </summary>
        /// <param name="dbc"></param>
        /// <returns>Db engine specific Connection</returns>
        public static IDbConnection GetDbConnectionObject(this IDataSourceCredentials dbc)
        {
            IDbConnection output = null;

            switch (dbc.DataSourceProvider)
            {
                case DataSourceProvider.Npgsql:

                    //TODO - make it dynamically resolve types! So the util is not so tightly coupled with different drivers as it sucks!
                    //Assembly assembly = Assembly.Load("Npgsql");
                    //Type t = assembly.GetType("Npgsql.NpgsqlConnection");
                    //ConstructorInfo ctor = t.GetConstructor(new Type[] {typeof (string)});

                    //output = (IDbConnection)ctor.Invoke(new object[] { dbc.GetConnectionString() });
                    
                    output = new NpgsqlConnection(dbc.GetConnectionString());
                    break;

                case DataSourceProvider.SqlServer:
                    output = new System.Data.SqlClient.SqlConnection(dbc.GetConnectionString());
                    break;

                //case DataSourceProvider.Oracle:
                //    //TODO - when implementing oracle make it dynamically resolve assemblies as pgsql code does
                //    output = null; //new System.Data.OracleClient.OracleConnection(dbc.connectionString);
                //    break;
            }

            return output;
        }

        /// <summary>
        /// Returns a db engine specific Command object
        /// </summary>
        /// <param name="dbc"></param>
        /// <returns>Db engine specific Command</returns>
        public static IDbCommand GetDbCommandObject(this IDataSourceCredentials dbc)
        {
            IDbCommand output = null;

            switch (dbc.DataSourceProvider)
            {
                case DataSourceProvider.Npgsql:
                    output = new NpgsqlCommand();
                    break;

                case DataSourceProvider.SqlServer:
                    output = new System.Data.SqlClient.SqlCommand();
                    break;

                //case DataSourceProvider.Oracle:
                //    //TODO - when implementing oracle make it dynamically resolve assemblies as pgsql code does
                //    output = null; //new System.Data.OracleClient.OracleCommand();
                //    break;
            }

            return output;
        }

        /// <summary>
        /// Returns a db engine specific DataAdapter object
        /// </summary>
        /// <param name="dbc"></param>
        /// <returns>Db engine specific DataAdapter</returns>
        public static IDbDataAdapter GetDbDataAdapterObject(this IDataSourceCredentials dbc)
        {
            IDbDataAdapter output = null;

            switch (dbc.DataSourceProvider)
            {
                case DataSourceProvider.Npgsql:
                    output = new NpgsqlDataAdapter();
                    break;

                case DataSourceProvider.SqlServer:
                    output = new System.Data.SqlClient.SqlDataAdapter();
                    break;

                //case DataSourceProvider.Oracle:
                //    //TODO - when implementing oracle make it dynamically resolve assemblies as pgsql code does
                //    output = null; //new System.Data.OracleClient.OracleDataAdapter();
                //    break;
            }

            return output;
        }
    }
}
