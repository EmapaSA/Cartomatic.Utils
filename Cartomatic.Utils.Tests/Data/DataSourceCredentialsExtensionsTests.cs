using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cartomatic.Utils.Data;
using NUnit.Framework.Internal;
using NUnit.Framework;
using FluentAssertions;

namespace Cartomatic.Utils.Data.Tests
{
    [TestFixture]
    public class DataSourceCredentialsExtensionsTests
    {
        [Test]
        public void GetDbConnection_WhenDataSourceProviderIsPgSql_ReturnsNpgsqlConnection()
        {
            var dbc = new DataSourceCredentials()
            {
                DataSourceProvider = DataSourceProvider.Npgsql,
                ServerHost = "localhost"
            };

            var conn = dbc.GetDbConnectionObject();

            conn.GetType().FullName.Should().Be("Npgsql.NpgsqlConnection");
        }

        [Test]
        public void GetDbCommandObject_WhenDataSourceProviderIsPgSql_ReturnsNpgsqlCommand()
        {
            var dbc = new DataSourceCredentials()
            {
                DataSourceProvider =DataSourceProvider.Npgsql
            };

            var conn = dbc.GetDbCommandObject();

            conn.GetType().FullName.Should().Be("Npgsql.NpgsqlCommand");
        }

        [Test]
        public void GetDbDataAdapterObject_WhenDataSourceProviderIsPgSql_ReturnsNpgsqlDataAdapter()
        {
            var dbc = new DataSourceCredentials()
            {
                DataSourceProvider =DataSourceProvider.Npgsql
            };

            var conn = dbc.GetDbDataAdapterObject();

            conn.GetType().FullName.Should().Be("Npgsql.NpgsqlDataAdapter");
        }

        [Test]
        public void GetDbConnection_WhenDataSourceProviderIsSqlServer_ReturnsSqlClientSqlConnectionn()
        {
            var dbc = new DataSourceCredentials()
            {
                DataSourceProvider = DataSourceProvider.SqlServer
            };

            var conn = dbc.GetDbConnectionObject();

            conn.GetType().FullName.Should().Be("System.Data.SqlClient.SqlConnection");
        }

        [Test]
        public void GetDbCommandObject_WhenDataSourceProviderIsSqlServer_ReturnsSqlClientSqlCommandn()
        {
            var dbc = new DataSourceCredentials()
            {
                DataSourceProvider = DataSourceProvider.SqlServer
            };

            var conn = dbc.GetDbCommandObject();

            conn.GetType().FullName.Should().Be("System.Data.SqlClient.SqlCommand");
        }

        [Test]
        public void GetDbDataAdapterObject_WhenDataSourceProviderIsSqlServer_ReturnsSqlClientSqlDataAdapter()
        {
            var dbc = new DataSourceCredentials()
            {
                DataSourceProvider = DataSourceProvider.SqlServer
            };

            var conn = dbc.GetDbDataAdapterObject();

            conn.GetType().FullName.Should().Be("System.Data.SqlClient.SqlDataAdapter");
        }

    }
}
